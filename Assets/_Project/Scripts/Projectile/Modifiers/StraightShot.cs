using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShot : IProjectileModifier
{

    public void OnAttach(TurretController turret)
    {
        turret.SetSpeed(turret.GetSpeed() * 2f);
    }

    public void OnDetach(TurretController turret)
    {
        turret.SetSpeed(turret.GetSpeed() / 2f);
    }

    public void OnProjEnable(Projectile proj)
    {

    }

    public void OnHit(Projectile proj)
    {
        
    }
}
