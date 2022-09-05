using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : IProjectileModifier
{

    public void OnAttach(TurretController turret)
    {
        turret.SetCooldown(turret.GetCooldown() * 0.50f);
        turret.SetDamage(turret.GetDamage() * 1.15f);
    }

    public void OnDetach(TurretController turret)
    {
        turret.SetCooldown(turret.GetCooldown() / 0.50f);
        turret.SetDamage(turret.GetDamage() / 1.15f);
    }

    public void OnProjEnable(Projectile proj)
    {
        
    }

    public void OnHit(Projectile proj)
    {
        
    }
}
