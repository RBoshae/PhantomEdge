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
    public Transform goal;
    [SerializeField]
    string goalname;
    [SerializeField]
    private ParticleSystem DestroyParticles;
    public Teams team;
    public Animator anim;
    public NavMeshAgent agent;
    public MinionState state;
    private Rigidbody rb;
    private float InitialAnimatorSpeed;

    public void ApplyDamage(int damage = 0)
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
        }
    }

    public void KnockBack(float dist)
    {
        state = MinionState.knockback;
        agent.enabled = false;
        anim.speed = 0;

    }

    protected void IsOnNavMesh(NavMeshAgent agent)
    {
        NavMeshHit hit;

        if (NavMesh.SamplePosition(this.GetComponentInParent<Transform>().position, out hit, 2, agent.areaMask))
        {
            Debug.Log("Closest navmesh: " + hit.position);
            agent.Warp(hit.position);
        }
        else
        {
            Debug.Log("Navmesh Not found");
        }
    }

    protected void Start()
    {
        CurrentHP = MaxHP;
        agent = GetComponent<NavMeshAgent>();
        IsOnNavMesh(agent);

        agent.destination = goal.localPosition;
        anim = transform.parent.GetComponent<Animator>();
        state = MinionState.attacking;
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
