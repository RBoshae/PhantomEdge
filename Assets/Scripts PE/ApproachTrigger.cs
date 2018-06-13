using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachTrigger : MonoBehaviour {

    private Minion MyMinion;

    private void Start()
    {
        MyMinion = transform.parent.GetComponent<Minion>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    MainPlayer player = other.GetComponent<MainPlayer>();
    //    Minion minion = other.GetComponent<Minion>();
    //    if (player)
    //    {
    //        MyMinion.agent.destination = other.transform.position;
    //    }
    //    else if (minion)
    //    {
    //        if (minion.team)
    //        meleeminion m = other.getcomponentinchildren<meleeminion>();
    //        if (team != m.team)
    //        {
    //            agent.destination = m.transform.localposition;
    //        }
    //    }
    //    else
    //    {
    //        goal = gameobject.find(goalname).transform;
    //        agent.destination = goal.localposition;
    //    }
    //}
}
