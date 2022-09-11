using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : IProjectileModifier
{
    float cdMod = 0.5f;
    float damageMod = 1.15f;

    public void OnAttach(TurretController turret)
    {
        turret.projCooldown = turret.projCooldown * cdMod;
        turret.projDamage = turret.projDamage * damageMod;
    }

    public void OnDetach(TurretController turret)
    {
        turret.projCooldown = turret.projCooldown / cdMod;
        turret.projDamage = turret.projDamage / damageMod;
    }

    public void OnProjEnable(Projectile proj)
    {
        
    }

    public void OnHit(Projectile proj)
    {
        
    }
}
