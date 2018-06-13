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


    void Start () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        IsOnNavMesh(agent);

        goal = GameObject.Find(goalname).transform;
        agent.destination = goal.position;
    }
    //Constructor for melee minion
    public MeleeMinion(float hp, float maxhp)
    {
        CurrentHP = hp;
        MaxHP = maxhp;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Attack function goes here.
        }
        else if(collision.gameObject.tag == "Attack")
        {
            ApplyDamage();
        }
        
    }
    
    
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
