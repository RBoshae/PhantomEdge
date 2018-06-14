using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    int Damage = 100;
    [SerializeField]
    Minion Wielder = null;

    void Start () {
        if(Wielder == null)
        {
            GlobalRefs.Sword = this.gameObject;
        }
        
	}
    
    
    private void OnTriggerEnter(Collision collision)
    {

        if(collision.gameObject.GetComponent<Minion>() && collision.gameObject.GetComponent<Minion>().team != GlobalRefs.Player.team && Wielder == null)
        {
            collision.gameObject.GetComponent<Minion>().ApplyDamage(Damage);
        }
        else if(collision.gameObject.GetComponent<Minion>() && Wielder != null && collision.gameObject.GetComponent<Minion>().team != Wielder.team)
        {
            collision.gameObject.GetComponent<Minion>().ApplyDamage(Damage / 10);
        }
        else if(collision.gameObject.GetComponent<Turret>() && (collision.gameObject.GetComponent<Turret>().team != Wielder.team || collision.gameObject.GetComponent<Turret>().team != GlobalRefs.Player.team))
        {
            //Add Later
            collision.gameObject.GetComponent<Turret>().ApplyDamage(Damage/2);
        }
        else if(collision.gameObject.GetComponent<Nexus>() && (collision.gameObject.GetComponent<Nexus>().team != Wielder.team || collision.gameObject.GetComponent<Nexus>().team != GlobalRefs.Player.team))
        {
            collision.gameObject.GetComponent<Nexus>().ApplyDamage(Damage);
        }

    }
    
    // Update is called once per frame
    void Update () {
		if(Wielder == null && OVRInput.Get(OVRInput.Button.One))
        {
            GlobalRefs.Sword.GetComponent<Rigidbody>().useGravity = false;
            GlobalRefs.Sword.transform.position = Vector3.Lerp(GlobalRefs.Sword.transform.position, GameObject.Find("RightHandAnchor").transform.position, Time.deltaTime * 5);
            
        }
        else
        {
            GlobalRefs.Sword.GetComponent<Rigidbody>().useGravity = true;
        }
	}
}
