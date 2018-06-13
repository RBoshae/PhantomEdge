using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    int Damage = 100;
    [SerializeField]
    Minion Wielder = null;
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.GetComponent<Minion>() && collision.gameObject.GetComponent<Minion>().team != GlobalRefs.Player.team && Wielder == null)
        {
            collision.gameObject.GetComponent<Minion>().ApplyDamage(Damage / 2);
        }
        else if(collision.gameObject.GetComponent<Minion>() && Wielder != null && collision.gameObject.GetComponent<Minion>().team != Wielder.team)
        {
            collision.gameObject.GetComponent<Minion>().ApplyDamage(Damage / 10);
        }
        else if(collision.gameObject.GetComponent<Turret>() && (collision.gameObject.GetComponent<Turret>().team != Wielder.team || collision.gameObject.GetComponent<Turret>().team != GlobalRefs.Player.team))
        {
            //Add Later
            collision.gameObject.GetComponent<Turret>().ApplyDamage(Damage * 0);
        }
        else if(collision.gameObject.GetComponent<Nexus>() && (collision.gameObject.GetComponent<Nexus>().team != Wielder.team || collision.gameObject.GetComponent<Nexus>().team != GlobalRefs.Player.team))
        {
            collision.gameObject.GetComponent<Nexus>().ApplyDamage(Damage / 2);
        }

    }
}
