using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minion : MonoBehaviour {

    [SerializeField]
    protected float CurrentHP;
    [SerializeField]
    protected float MaxHP;
    public virtual void ApplyDamage(float damage = 0)
    {

    }
}
