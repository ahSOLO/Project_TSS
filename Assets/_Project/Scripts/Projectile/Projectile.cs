using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public float speed = 10f;
    public float lifetime = 2f;
    public float cooldown = 0.5f;
    public List<IProjectileModifier> modifiers;
    protected Rigidbody2D rB;

    // Lifetime
    private void Awake()
    {
        rB = GetComponent<Rigidbody2D>();
        modifiers = new List<IProjectileModifier>();
    }

    private void OnEnable()
    {
        foreach (var modifier in modifiers)
        {
            modifier.OnAttach();
        }

        SetVelocity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyCollision(collision);
    }

    private void Update()
    {        
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject); // TODO: Object pool
        }
    }

    // Logic
    private void SetVelocity()
    {
        rB.velocity = transform.up * speed;
    }

    private void EnemyCollision(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // Get enemy HP and subtract damage.
            // Activate projectile effects on enemy.
            foreach (var modifier in modifiers)
            {
                modifier.OnHit();
            }
            Destroy(gameObject); // TODO: Object pool
        }
    }
}
