using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShot : IProjectileModifier
{
    float speedMod = 2f;
    float damageMod = 1.5f;

    public void OnAttach(TurretController turret)
    {
        turret.projSpeed = turret.projSpeed * speedMod;
        turret.projDamage = turret.projDamage * damageMod;
    }

    public void OnDetach(TurretController turret)
    {
        turret.projSpeed = turret.projSpeed / speedMod;
        turret.projDamage = turret.projDamage / damageMod;
    }

    public void OnProjEnable(Projectile proj)
    {
        
    }

    public void OnHit(Projectile proj)
    {
        
    }
}
