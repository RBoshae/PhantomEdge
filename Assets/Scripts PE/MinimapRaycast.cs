using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRaycast : MonoBehaviour
{
    [SerializeField]
    private Camera MinimapCam;
    [SerializeField]
    private Transform player;
    
    void Update () {
		if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick))
        {
            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("Minimap");
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Ray ray = MinimapCam.ViewportPointToRay(new Vector3(hit.textureCoord.x, hit.textureCoord.y, 0));
                RaycastHit mapHit;
                if (Physics.Raycast(ray, out mapHit))
                {
                    player.position = mapHit.point;
                }
            }
        }
    }
}
