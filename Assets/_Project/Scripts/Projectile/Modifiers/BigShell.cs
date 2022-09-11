using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShell : IProjectileModifier
{

    float damageMod = 1.7f;
    float speedMod = 0.8f;
    float scaleMod = 2f;

    public void OnAttach(TurretController turret)
    {
        turret.projDamage = turret.projDamage * damageMod;
        turret.projSpeed = turret.projSpeed * speedMod;
    }

    public void OnDetach(TurretController turret)
    {
        turret.projDamage = turret.projDamage / damageMod;
        turret.projSpeed = turret.projSpeed / speedMod;
    }

    public void OnProjEnable(Projectile proj)
    {
        proj.transform.localScale *= scaleMod;
    }

    public void OnHit(Projectile proj)
    {
        
    }

}
