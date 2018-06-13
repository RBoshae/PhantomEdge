using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MeleeMinion : Minion {



    [SerializeField]
    public Transform goal;
    [SerializeField]
    string goalname;
    // Use this for initialization
    public string team;
    public Animator anim;


    void IsOnNavMesh(NavMeshAgent agent)
    {
        NavMeshHit hit;
        
        if(NavMesh.SamplePosition(this.GetComponentInParent<Transform>().position,  out hit, 2, agent.areaMask))
        {
            Debug.Log("Closest navmesh: " + hit.position);
            agent.Warp(hit.position);
        }
        else
        {
            Debug.Log("Navmesh Not found");
        }
    }

    NavMeshAgent agent;
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        IsOnNavMesh(agent);

        goal = GameObject.Find(goalname).transform;
        agent.destination = goal.localPosition;
    }
    //Constructor for melee minion
    public MeleeMinion(float hp, float maxhp)
    {
        CurrentHP = hp;
        MaxHP = maxhp;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        //Attack function goes here.
    //    }
    //    else if(collision.gameObject.tag == "Attack")
    //    {
    //        ApplyDamage();
    //    }
        
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.name);
    //    if(other.gameObject.tag == "Player")
    //    {
    //        agent.destination = other.transform.position;
    //    }
    //    else if(other.gameObject.tag == "Minion")
    //    {
    //        MeleeMinion m = other.GetComponentInChildren<MeleeMinion>();
    //        if(team != m.team)
    //        {
    //            agent.destination = m.transform.localPosition;
    //        }
    //    }
    //    else
    //    {
    //        goal = GameObject.Find(goalname).transform;
    //        agent.destination = goal.localPosition;
    //    }
    //}
    public override void ApplyDamage(float damage = 0)
    {

        CurrentHP -= damage;

        if(CurrentHP <= 0)
        {
            StartCoroutine(DeathAnimation());
        }
    }
    IEnumerator DeathAnimation()
    {
        //Play Death Animation if any
        //while(animation.IsPlaying)
        //{
        //    yield return null;
        //}
        Destroy(this.gameObject);

        return null;
    }
}
