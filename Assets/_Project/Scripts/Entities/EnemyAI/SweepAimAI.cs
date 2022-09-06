using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepAimAI : IEnemyAI
{
    bool isSweepingClockwise = false;

    public void Init(Enemy enemy)
    {
        
    }

    public void Tick(Enemy enemy)
    {
        enemy.turret.localRotation = Quaternion.RotateTowards(enemy.turret.localRotation, Quaternion.Euler(0f, 0f, isSweepingClockwise ? enemy.sweepStartAngle : enemy.sweepEndAngle ), enemy.turretRotSpeed * Time.deltaTime);

        if ((!isSweepingClockwise && (Mathf.Abs(Utility.NormalizeAngle(enemy.turret.localRotation.eulerAngles.z) - enemy.sweepEndAngle) < 1f) 
            ||isSweepingClockwise && (Mathf.Abs(Utility.NormalizeAngle(enemy.turret.localRotation.eulerAngles.z) - enemy.sweepStartAngle) < 1f)))
        {
            isSweepingClockwise = !isSweepingClockwise;
        }
    }

    public void FixedTick(Enemy enemy)
    {

    }

    public void OnDeath(Enemy enemy)
    {

    }
}
