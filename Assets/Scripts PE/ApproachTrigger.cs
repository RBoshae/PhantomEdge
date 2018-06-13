using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachTrigger : MonoBehaviour {

    private Minion MyMinion;
    private HashSet<MainPlayer> players = new HashSet<MainPlayer>();
    private HashSet<Minion> minions = new HashSet<Minion>();

    private void Start()
    {
        MyMinion = transform.parent.GetComponent<Minion>();
    }

    private void OnTriggerEnter(Collider other)
    {
        MainPlayer player = other.GetComponent<MainPlayer>();
        Minion minion = other.GetComponent<Minion>();
        if (player && player.team != MyMinion.team)
        {
            players.Add(player);
            if (MyMinion.state == MinionState.walking)
                MyMinion.agent.destination = other.transform.position;
        }
        else if (minion && minion.team != MyMinion.team)
        {
            minions.Add(minion);
            if (MyMinion.state == MinionState.walking)
                MyMinion.agent.destination = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MainPlayer player = other.GetComponent<MainPlayer>();
        Minion minion = other.GetComponent<Minion>();
        bool someoneLeft = false;
        if (player && player.team != MyMinion.team)
        {
            players.Remove(player);
            someoneLeft = true;
        }
        else if (minion && minion.team != MyMinion.team)
        {
            minions.Remove(minion);
            someoneLeft = true;
        }
        if (someoneLeft && MyMinion.state == MinionState.approaching)
        {
            if (players.Count > 0)
            {
                // get next player
                // else get next minion
            }
        }
    }

    private void FollowNexus()
    {
        switch (MyMinion.team)
        {
            case Teams.red:
                MyMinion.goal = GlobalRefs.blueNexus.transform;
                break;
            case Teams.blue:
                MyMinion.goal = GlobalRefs.redNexus.transform;
                break;
            default:
                break;
        }
    }
}
