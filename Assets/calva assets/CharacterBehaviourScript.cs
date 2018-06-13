using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The role that the player chooses to be (warrior, mage, )
    public enum Weapon
{
    sword = 0,
}

public enum PlayerStates
{
    idle = 0,
    walking = 1,
    attacking = 2,
    dead = 3
}

public class Player : CharacterBase
{

    public Weapon currentRole;
    [SerializeField]
    public PlayerStates state = PlayerStates.idle;
    public Character_Istics playerStats;
    //Player's Health
    public int maxHealth;
    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = Mathf.Clamp(value, 0, maxHealth);
            if (_health == 0)
            {
                //Destroy(gameObject);
                Debug.Log("Dead");
            }
        }
    }

    private int _health = 0;

    private void Start()
    {
        maxHealth = _health = playerStats.Health;
    }
   
}