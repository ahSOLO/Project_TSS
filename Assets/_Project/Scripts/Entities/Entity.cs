using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float hp = 100f;
    
    private Faction faction;

    protected virtual void Awake()
    {
        faction = CompareTag("Player") ? Faction.Player
            : CompareTag("Enemy") ? Faction.Enemy
            : Faction.Neutral ;
    }

    protected void ReceiveDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Death();
        }
    }

    protected abstract void Death();
}
