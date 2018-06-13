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
    
    
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Minion" && collision.gameObject.GetComponent<Minion>() && collision.gameObject.GetComponent<Minion>().team != GlobalRefs.Player.team && Wielder == null)
        {
            collision.gameObject.GetComponent<Minion>().ApplyDamage(Damage);
        }
        else if(collision.gameObject.tag == "Minion" && collision.gameObject.GetComponent<Minion>() && Wielder != null && collision.gameObject.GetComponent<Minion>().team != Wielder.team)
        {
            collision.gameObject.GetComponent<Minion>().ApplyDamage(Damage / 10);
        }
        else if(collision.gameObject.tag == "Tower")
        {
            //Add Later
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
