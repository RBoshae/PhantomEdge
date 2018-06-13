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
    private Transform _goal;
    [SerializeField]
    private ParticleSystem DestroyParticles;
    [SerializeField]
    private float ApproachRadius = 4;
    [SerializeField]
    private float AtackRadius = 1;
    public Teams team;
    public Animator anim;
    public NavMeshAgent agent;
    public MinionState state;
    private Rigidbody rb;
    private float InitialAnimatorSpeed;

    public Transform goal
    {
        get {
            return _goal;
        }
        set {
            _goal = value;
            agent.destination = value.localPosition;
        }
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

    public void FollowNexus()
    {
        switch (team)
        {
            case Teams.red:
                goal = GlobalRefs.blueNexus.transform;
                break;
            case Teams.blue:
                goal = GlobalRefs.redNexus.transform;
                break;
            default:
                break;
        }
    }

    protected void IsOnNavMesh(NavMeshAgent agent)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.parent.parent.position, out hit, 4, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
        else
        {
            Debug.Log("Navmesh Not found");
        }
    }

    protected void Awake()
    {
        CurrentHP = MaxHP;
        agent = GetComponent<NavMeshAgent>();
        IsOnNavMesh(agent);
        agent.destination = _goal.position;

        anim = transform.parent.GetComponent<Animator>();
        state = MinionState.walking;
        rb = GetComponent<Rigidbody>();
        InitialAnimatorSpeed = anim.speed;
    }

    private void Update()
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

    private void CheckNearbyEnemies()
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
                goal = player.transform;
                return;
            }
            // Check the closest minion out of all minions closeby
            Minion minion = hit.GetComponent<Minion>();
            if (minion && minion.team != team)
            {
                float curDist = Vector3.Distance(minion.transform.position, transform.position);
                if (curDist < minDist)
                {
                    minDist = curDist;
                    goal = minion.transform;
                }
            }
        }
    }

    protected IEnumerator DeathAnimation()
    {
        float startTime = Time.time;
        while ((Time.time - startTime) < DestroyParticles.main.duration)
        {
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
