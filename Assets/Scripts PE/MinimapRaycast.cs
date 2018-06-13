using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRaycast : MonoBehaviour
{
	// Update is called once per frame
	void Update () {
		if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick))
        {
            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("Minimap");
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {

            }
        }
	}
}
