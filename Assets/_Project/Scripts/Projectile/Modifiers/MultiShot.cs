using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShot : IProjectileModifier
{
    int numberMod = 2;

    public void OnAttach(TurretController turret)
    {
        turret.projNumber += numberMod;
    }

    public void OnDetach(TurretController turret)
    {
        turret.projNumber -= numberMod;
    }

    public void OnProjEnable(Projectile proj)
    {
        
    }

    public void OnHit(Projectile proj)
    {
        
    }

}
