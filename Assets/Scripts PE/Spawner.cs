using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Teams { red, blue };

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
    float groupTimer;
    [SerializeField]
    int groupSize = 5;
    public int redTeamCount = 0;
    public int blueTeamCount = 0;
    public int maxTeamSize = 30;

    void Start () {
        timer = spawnTimer;
        GlobalRefs.Spawner = this;
		if(Minions.Length == 0)
        {
            Debug.LogError("No minions set to spawn");
        }
	}
    
    //Spawns a minion every spawnTimer seconds. at the referenced spawnpoint
	void Update () {
        
        if(timer <= 0)
        {
            for (int i = 0; i < groupSize; i++)
            {
                if (redTeamCount < (maxTeamSize - 3))
                {
                    Minion redMinion = Instantiate(Minions[0], null).transform.GetChild(0).GetComponent<Minion>();
                    redMinion.transform.position = SpawnPoints[0].position;
                    redMinion.MySpawnPoint = SpawnPoints[0];
                    redMinion.CustomAwake();
                    redMinion = Instantiate(Minions[1], null).transform.GetChild(0).GetComponent<Minion>();
                    redMinion.transform.position = SpawnPoints[1].position;
                    redMinion.MySpawnPoint = SpawnPoints[1];
                    redMinion.CustomAwake();
                    redMinion = Instantiate(Minions[2], null).transform.GetChild(0).GetComponent<Minion>();
                    redMinion.transform.position = SpawnPoints[2].position;
                    redMinion.MySpawnPoint = SpawnPoints[2];
                    redMinion.CustomAwake();
                    redTeamCount += 3;
                }
                if (blueTeamCount < (maxTeamSize - 3))
                {
                    Minion blueMinion = Instantiate(Minions[3], null).transform.GetChild(0).GetComponent<Minion>();
                    blueMinion.transform.position = SpawnPoints[3].position;
                    blueMinion.MySpawnPoint = SpawnPoints[3];
                    blueMinion.CustomAwake();
                    blueMinion = Instantiate(Minions[4], null).transform.GetChild(0).GetComponent<Minion>();
                    blueMinion.transform.position = SpawnPoints[4].position;
                    blueMinion.MySpawnPoint = SpawnPoints[4];
                    blueMinion.CustomAwake();
                    blueMinion = Instantiate(Minions[5], null).transform.GetChild(0).GetComponent<Minion>();
                    blueMinion.transform.position = SpawnPoints[5].position;
                    blueMinion.MySpawnPoint = SpawnPoints[5];
                    blueMinion.CustomAwake();
                    blueTeamCount += 3;
                }
            }
            timer = spawnTimer;
        }
        timer -= Time.deltaTime;
	}
}
