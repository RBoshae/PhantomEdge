using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMinion : Minion {

	// Use this for initialization
    void Start () {
		
	}
    //Constructor for melee minion
    public MeleeMinion(float hp, float maxhp)
    {
        CurrentHP = hp;
        MaxHP = maxhp;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Attack function goes here.
        }
        else if(collision.gameObject.tag == "Attack")
        {
            ApplyDamage();
        }
        
    }
    public override void ApplyDamage(float damage = 0)
    {

        CurrentHP -= damage;

        if(CurrentHP <= 0)
        {
            StartCoroutine(DeathAnimation());
        }
    }
    IEnumerator DeathAnimation()
    {
        //Play Death Animation if any
        //while(animation.IsPlaying)
        //{
        //    yield return null;
        //}
        Destroy(this.gameObject);

        return null;
    }
}
