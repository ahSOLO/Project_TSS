using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private IProjectileModifier projectilePU;
    public enum ProjectilePU { RapidFire, StraightShot, BigShell, MultiShot, Shotgun };
    public ProjectilePU projectilePUSelection;
    public float duration;

    private void Awake()
    {
        switch (projectilePUSelection)
        {
            case ProjectilePU.RapidFire:
                projectilePU = new RapidFire();
                break;
            case ProjectilePU.StraightShot:
                projectilePU = new StraightShot();
                break;
            case ProjectilePU.BigShell:
                projectilePU = new BigShell();
                break;
            case ProjectilePU.MultiShot:
                projectilePU = new MultiShot();
                break;
            case ProjectilePU.Shotgun:
                projectilePU = new Shotgun();
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            TurretController.instance.AttachProjectileModifier(projectilePU, duration);
            Destroy(gameObject);
        }
    }
}
