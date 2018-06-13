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
