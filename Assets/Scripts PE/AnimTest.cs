using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour {

    // Use this for initialization
    public Animator anim;
	void Start () {
		
	}

    // Update is called once per frame
    private void Update()
    {
        if (anim)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetBool("attack", true);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetBool("attack", false);
            }
        }
    }
}
