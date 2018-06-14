using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GlobalRefs.Explosion = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
