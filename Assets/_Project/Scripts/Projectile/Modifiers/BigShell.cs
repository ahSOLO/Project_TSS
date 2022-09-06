using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShell : IProjectileModifier
{

    public void OnAttach(TurretController turret)
    {
        turret.SetDamage(turret.GetDamage() * 1.7f);
        turret.SetSpeed(turret.GetSpeed() * 0.8f);
    }

    public void OnDetach(TurretController turret)
    {
        turret.SetDamage(turret.GetDamage() / 1.7f);
        turret.SetSpeed(turret.GetSpeed() / 0.8f);
    }

    public void OnProjEnable(Projectile proj)
    {
        proj.transform.localScale *= 2f;
    }

    public void OnHit(Projectile proj)
    {
        
    }

}
