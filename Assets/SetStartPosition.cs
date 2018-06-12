using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartPosition : MonoBehaviour {

    public Transform StartPosition;
	// Use this for initialization
	void Start () {
        transform.position = StartPosition.position;
	}
	
}
