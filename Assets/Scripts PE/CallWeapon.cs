using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallWeapon : MonoBehaviour {


    // Update is called once per frame
    private void Start()
    {
        GlobalRefs.Gun = this.gameObject;
    }
    void Update () {
        if (OVRInput.Get(OVRInput.Button.Three) && GlobalRefs.Gun != null)
        {
            Debug.Log("Gun");
            GlobalRefs.Gun.GetComponent<Rigidbody>().useGravity = false;
            GlobalRefs.Gun.transform.position = Vector3.Lerp(GlobalRefs.Gun.transform.position, GameObject.Find("LeftHandAnchor").transform.position, Time.deltaTime * 5);

        }
        else
        {
            GlobalRefs.Gun.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
