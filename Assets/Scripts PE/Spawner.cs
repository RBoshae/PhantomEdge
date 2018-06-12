using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject[] Minions;
    [SerializeField]
    Transform SpawnPoint;
    [SerializeField]
    float spawnTimer;
	void Start () {
		if(Minions.Length == 0)
        {
            Application.Quit();
        }
	}
	
	// Update is called once per frame
    //Spawns a minion every spawnTimer seconds. at the referenced spawnpoint
	void Update () {
        float timer = spawnTimer;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        GameObject m = Instantiate(Minions[0], SpawnPoint);
	}
}
