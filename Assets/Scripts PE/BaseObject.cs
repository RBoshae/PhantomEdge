﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{

    public Teams team = Teams.red;
    public int CurrentHP;
    [SerializeField]
    protected int MaxHP = 10;
    [SerializeField]
    protected ParticleSystem normalParticles;
    [SerializeField]
    protected ParticleSystem DestroyParticles;
    protected bool dead = false;

    public virtual void ApplyDamage(int damage = 0)
    {
        CurrentHP -= damage;

        if (CurrentHP <= 0 && !dead)
        {
            dead = true;

            StartCoroutine(DestroyAnimation());
        }
    }

    protected virtual void Start()
    {
        CurrentHP = MaxHP;
    }

    protected IEnumerator DestroyAnimation()
    {
        float startTime = Time.time;
        while((Time.time - startTime) < DestroyParticles.main.duration)
        {
            yield return null;
        }
        Destroy(this.gameObject);
    }
}