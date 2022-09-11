using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAI : IEnemyAI
{
    private Rigidbody2D rB;

    public void Init(Enemy enemy)
    {
        rB = enemy.GetComponent<Rigidbody2D>();
        rB.velocity = Vector3.zero;
    }

    public void Tick(Enemy enemy)
    {
    }

    public void FixedTick(Enemy enemy)
    {
        if (PlayerController.instance != null)
        {
            enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, Quaternion.LookRotation(Vector3.forward, (PlayerController.instance.transform.position - enemy.transform.position)), enemy.rotSpeed * Time.fixedDeltaTime);
        }
        rB.velocity = enemy.transform.up * enemy.moveSpeed * Time.fixedDeltaTime;
    }

    public void OnDeath(Enemy enemy)
    {

    }
}
