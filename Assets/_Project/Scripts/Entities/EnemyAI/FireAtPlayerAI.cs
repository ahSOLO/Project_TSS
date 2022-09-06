using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAtPlayerAI : IEnemyAI
{
    public void Init(Enemy enemy)
    {

    }

    public void Tick(Enemy enemy)
    {
        enemy.turret.rotation = Quaternion.Lerp(enemy.turret.rotation, Quaternion.LookRotation(Vector3.forward, PlayerController.instance.transform.position - enemy.transform.position), enemy.turretRotSpeed);
        
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
