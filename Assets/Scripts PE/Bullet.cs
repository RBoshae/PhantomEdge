using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    int Damage = 100;
    [SerializeField]
    BaseObject Wielder = null;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Minion>() && collision.gameObject.GetComponent<Minion>().team != GlobalRefs.Player.team && Wielder == null)
        {
            print("hit minion");
            collision.gameObject.GetComponent<Minion>().ApplyDamage(Damage / 2);
        }
        else if(collision.gameObject.GetComponent<Minion>() && Wielder != null && collision.gameObject.GetComponent<Minion>().team != Wielder.team)
        {
            print("hit range minion");
            collision.gameObject.GetComponent<Minion>().ApplyDamage(Damage / 10);
        }
        else if(collision.gameObject.GetComponent<Turret>() && (collision.gameObject.GetComponent<Turret>().team != GlobalRefs.Player.team))
        {
            //Add Later
            print("hit turret");
            collision.gameObject.GetComponent<Turret>().ApplyDamage(Damage * 0);
        }
        else if(collision.gameObject.GetComponent<Nexus>() && (collision.gameObject.GetComponent<Nexus>().team != GlobalRefs.Player.team))
        {
            print("hit nexus");
            collision.gameObject.GetComponent<Nexus>().ApplyDamage(Damage / 2);
        }

    }
}
