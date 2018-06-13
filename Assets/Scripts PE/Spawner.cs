using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject[] Minions;
    [SerializeField]
    Transform[] SpawnPoints;
    [SerializeField]
    float spawnTimer = 3;
    [SerializeField]
    float timer;
    [SerializeField]
    int groupSize = 5;
    public int redTeamCount;
    public int blueTeamCount;

    void Start () {
        timer = spawnTimer;
		if(Minions.Length == 0)
        {
            Application.Quit();
        }
	}
	
	// Update is called once per frame
    //Spawns a minion every spawnTimer seconds. at the referenced spawnpoint
	void Update () {
        
        if(timer <= 0)
        {
            //for(int i = 0; i < groupSize; i++)
            //{
            //    GameObject redMinion = Instantiate(Minions[0], SpawnPoints[0]);
            //    redMinion = Instantiate(Minions[1], SpawnPoints[1]);
            //    redMinion = Instantiate(Minions[2], SpawnPoints[2]);
            //    redTeamCount++;
            //    
            //    //GameObject blueMinion = Instantiate(Minions[1], SpawnPoints[1]);
            //    //blueTeamCount++;
            //}
            GameObject redMinion = Instantiate(Minions[0], SpawnPoints[0]);
            //Debug.Log(SpawnPoints[0].transform.position);

            redMinion.transform.position = SpawnPoints[0].position;
            timer = spawnTimer;
        }

        timer -= Time.deltaTime;
	}
}
