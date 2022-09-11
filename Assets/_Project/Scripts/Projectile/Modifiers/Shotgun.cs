using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : IProjectileModifier
{
    int numberMod = 4;
    float rotSpreadMod = 90f;
    float lifetimeMod = -0.75f;

    public void OnAttach(TurretController turret)
    {
        turret.projNumber += numberMod;
        turret.projRotSpread += rotSpreadMod;
        turret.projLifetime += lifetimeMod;
    }

    public void OnDetach(TurretController turret)
    {
        turret.projNumber -= numberMod;
        turret.projRotSpread -= rotSpreadMod;
        turret.projLifetime -= lifetimeMod;
    }

    public void OnProjEnable(Projectile proj)
    {
        
    }

    public void OnHit(Projectile proj)
    {
        
    }

}
