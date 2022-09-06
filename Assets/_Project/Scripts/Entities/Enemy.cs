using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public float moveSpeed;
    public float rotSpeed;
    public float turretRotSpeed;
    public float cooldownTimer;

    public EnemyAICollection enemyAI;

    public GameObject projectilePrefab;
    public Transform projectileContainer;
    public Transform projectileSpawnPoint;
    public Transform turret;

    public float projCooldown;
    public float projDamage;
    public float projSpeed;
    public float projLifetime;

    [Tooltip("The starting angle. Rotations start counter-clockwise with right as starting point, normalized between -180 and 180 degrees.")]
    public float sweepStartAngle = -40;
    [Tooltip("The ending angle. Rotations start counter-clockwise with right as starting point, normalized between -180 and 180 degrees.")]
    public float sweepEndAngle = -40f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        foreach (IEnemyAI modifier in enemyAI.aIModifiers)
        {
            modifier.Init(this);
        }
    }

    private void Update()
    {
        foreach (IEnemyAI modifier in enemyAI.aIModifiers)
        {
            modifier.Tick(this);
        }

        cooldownTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        foreach (IEnemyAI modifier in enemyAI.aIModifiers)
        {
            modifier.FixedTick(this);
        }
    }

    protected override void Death()
    {
        foreach (IEnemyAI modifier in enemyAI.aIModifiers)
        {
            modifier.OnDeath(this);
        }
    }

    public void FireProjectile()
    {
        var proj = Instantiate(projectilePrefab, projectileContainer, true);
        proj.GetComponent<Projectile>().Init(projDamage, projSpeed, projLifetime, null, Faction.Enemy);
        proj.transform.position = projectileSpawnPoint.position;
        proj.transform.rotation = projectileSpawnPoint.rotation;
        proj.gameObject.SetActive(true);
        cooldownTimer = projCooldown;
    }
}
