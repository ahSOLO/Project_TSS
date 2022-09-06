using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWhenReadyAI : IEnemyAI
{
    public void Init(Enemy enemy)
    {

    }

    public void Tick(Enemy enemy)
    {
        if (enemy.cooldownTimer <= 0)
        {
            enemy.FireProjectile();
        }
    }

    public void FixedTick(Enemy enemy)
    {

    }

    public void OnDeath(Enemy enemy)
    {

    }
}
