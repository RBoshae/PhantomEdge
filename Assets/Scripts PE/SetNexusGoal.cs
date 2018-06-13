using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNexusGoal : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Minion minion = other.GetComponent<Minion>();
        if (minion)
        {
            minion.FollowNexus();
        }
    }
}
