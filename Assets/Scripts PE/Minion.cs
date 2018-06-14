using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MinionState { walking, approaching, attacking, knockback, dead };

public abstract class Minion : MonoBehaviour {

    [SerializeField]
    protected float CurrentHP;
    [SerializeField]
    protected float MaxHP = 2;
    [SerializeField]
    protected Transform _goal;
    [SerializeField]
    protected Transform CenterGoal;
    [SerializeField]
    protected Transform GoalNexus;
    [SerializeField]
    protected Transform MyNexus;
    [SerializeField]
    protected ParticleSystem DestroyParticles;
    [SerializeField]
    protected float ApproachRadius = 8;
    [SerializeField]
    protected float AttackRadius = 1;
    public Transform MySpawnPoint;
    public Teams team;
    public Animator anim;
    public NavMeshAgent agent;
    public MinionState state;
    public bool locked = false;
    protected Rigidbody rb;
    protected float InitialAnimatorSpeed;
    [SerializeField]
    Transform currentDestination;
    public Transform goal
    {
        get {
            return _goal;
        }
        set {
            _goal = value;
            agent.destination = value.localPosition;
            //currentDestination.position = value.localPosition;
        }
    }

    public void CustomAwake()
    {
        CurrentHP = MaxHP;
        agent = GetComponent<NavMeshAgent>();
        IsOnNavMesh(agent);
        agent.destination = _goal.position;

        anim = transform.parent.GetComponent<Animator>();
        state = MinionState.walking;
        rb = GetComponent<Rigidbody>();
        InitialAnimatorSpeed = anim.speed;
        CenterGoal = _goal;
        GetGoalNexus();
        DestroyParticles = GlobalRefs.Explosion;
    }

    public bool ApplyDamage(int damage = 0)
    {
        CurrentHP -= damage;

        if (CurrentHP <= 0 && state != MinionState.dead)
        {
            state = MinionState.dead;
            switch (team) {
                case Teams.red:
                    GlobalRefs.Spawner.blueTeamCount--;
                    break;
                case Teams.blue:
                    GlobalRefs.Spawner.redTeamCount--;
                    break;
                default:
                    break;
            }
            StartCoroutine(DeathAnimation());
            return true;
        }
        return false;
    }

    public void KnockBack(float dist)
    {
        state = MinionState.knockback;
        agent.enabled = false;
        anim.speed = 0;
    }

    public void KilledEnemy()
    {
        
    }

    protected void GetGoalNexus()
    {
        switch (team)
        {
            case Teams.red:
                MyNexus = GlobalRefs.redNexus.transform;
                GoalNexus = GlobalRefs.blueNexus.transform;
                break;
            case Teams.blue:
                MyNexus = GlobalRefs.blueNexus.transform;
                GoalNexus = GlobalRefs.redNexus.transform;
                break;
            default:
                break;
        }
    }

    public void FollowNexus()
    {
        float myDistToMyNexus = Vector3.Distance(transform.position, MyNexus.position);
        float CenterDistToMyNexus = Vector3.Distance(CenterGoal.position, MyNexus.position);
        if (myDistToMyNexus >= CenterDistToMyNexus || Mathf.Abs(myDistToMyNexus - CenterDistToMyNexus) < 10)
        {
            goal = GoalNexus;
        }
        else
        {
            goal = CenterGoal;
        }
        state = MinionState.walking;
    }

    protected void IsOnNavMesh(NavMeshAgent agent)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(MySpawnPoint.position, out hit, 4, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
        else
        {
            Debug.Log("Navmesh Not found");
        }
    }

    protected void Update()
    {
        if (!locked)
        {
            // Start walking again once knockback has stopped
            if (state == MinionState.knockback && rb.velocity == Vector3.zero)
            {
                state = MinionState.walking;
                agent.enabled = true;
                anim.speed = InitialAnimatorSpeed;
            }
            CheckNearbyEnemies();
        }
        transform.GetComponentInChildren<SphereCollider>().transform.position = goal.position;
    }

    protected void CheckNearbyEnemies()
    {
        if  (state == MinionState.walking || state == MinionState.approaching)
        {
            // Get all gameobjects within ApproachRadius distance
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, ApproachRadius);
            float minDist = Mathf.Infinity;
            foreach (Collider hit in hitColliders)
            {
                // if you find a player, immediately shoot at him (high priority over minions)
                MainPlayer player = hit.GetComponent<MainPlayer>();
                if (player)
                {
                    if (state == MinionState.approaching && Vector3.Distance(player.transform.position, transform.position) <= AttackRadius)
                    {
                        goal = player.transform;
                        agent.speed = 0;
                        state = MinionState.attacking;
                    }
                    else
                    {
                        goal = player.transform;
                        state = MinionState.approaching;
                    }
                    return;
                }
                // Check the closest minion out of all minions closeby
                Minion minion = hit.GetComponent<Minion>();
                if (minion && !minion.locked && minion.state == MinionState.walking && minion.team != team)
                {
                    minion.locked = true;
                    float curDist = Vector3.Distance(minion.transform.position, transform.position);
                    if (minion.state == MinionState.walking && curDist <= AttackRadius)
                    {
                        goal = minion.transform;
                        agent.speed = 0;
                        state = MinionState.attacking;
                        minion.AttackMe(this);
                        return;
                    }
                    else if (minion.state == MinionState.walking && curDist < minDist)
                    {
                        minDist = curDist;
                        goal = minion.transform;
                        minion.ApproachMe(this);
                        state = MinionState.approaching;
                    }
                    minion.locked = false;
                }
            }
            if (minDist != Mathf.Infinity)
            {
                return;
            }
            else
            {
                FollowNexus();
            }
        }
    }

    protected void ApproachMe(Minion minion)
    {
        state = MinionState.approaching;
        goal = minion.transform;
    }

    protected void AttackMe(Minion minion)
    {
        state = MinionState.attacking;
        goal = minion.transform;
        agent.speed = 0;
    }

    protected IEnumerator DeathAnimation()
    {
        float startTime = Time.time;
        DestroyParticles.transform.position = transform.position;
        DestroyParticles.Play();
        while ((Time.time - startTime) < DestroyParticles.main.duration)
        {
            yield return null;
        }
        DestroyParticles.Stop();
        Destroy(this.gameObject);
    }
}
