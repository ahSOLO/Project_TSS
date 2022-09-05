using UnityEngine;

public interface IProjectileModifier
{
    public void OnAttach(TurretController turret);

    public void OnDetach(TurretController turret);

    public void OnProjEnable(Projectile proj);

    public void OnHit(Projectile proj);
}