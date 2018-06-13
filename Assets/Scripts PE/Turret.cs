using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum turretState { idle, attacking, dead };

public class Turret : BaseObject {
    [SerializeField]
    protected float currentHP;
    [SerializeField]
    protected float maxHP = 100f;
    [SerializeField]
    private float attackRadius = 5f;
    [SerializeField]
    private float ApproachRadius = 10f;
    [SerializeField]
    private Transform bulletSpawnPoint;
    //public Teams team;
    //public Animator anim;
    public turretState state;

    public Transform target;
    //private float InitialAnimatorSpeed;
    public string enemyTag = "Enemy";

    public Transform partToRotate;

    // Use this for initialization
    void Start ()
    {
        
        // Update Target is called 2 times a second
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity; // When we haven't found an enemy then we have an infinite distance to the enemy.
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // returns distance in Unity units from tower to enemy
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            } 
        }

        // Check if nearest enemy is within range.
        if (nearestEnemy != null && shortestDistance <= attackRadius)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }
    // Update is called once per fram
    private void Update()
    {
        // if we don't have a target don't do anything 
        if (target == null)
        {
            return;
        }

        // Rotate object to aim at target.
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        Debug.Log(partToRotate);
    }

    // Makes sure that the range is drawn if the turret is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        
    }



    //private void checkNearbyEnemies()
    //{
    //    // Get all gameobjects within ApproachRadius distance
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, ApproachRadius);

    //    float minDist = Mathf.Infinity;
    //    foreach (Collider hit in hitColliders)
    //    {
    //        // if you find a player, immediately shoot at him (high priority over minions)
    //        MainPlayer player = hit.GetComponent<MainPlayer>();
    //        if (player)
    //        {
    //            goal = player.transform;
    //            return;
    //        }
    //        // Check the closest minion out of all minions closeby
    //        Minion minion = hit.GetComponent<Minion>();
    //        if (minion && minion.team != team)
    //        {
    //            float curDist = Vector3.Distance(minion.transform.position, transform.position);
    //            if (curDist < minDist)
    //            {
    //                minDist = curDist;
    //                goal = minion.transform;
    //            }
    //        }
    //    }
    //}

}
