using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private float speed;
    private float lifetime;
    private List<ProjectileModifierEntry> modifiers;
    private Rigidbody2D rB;
    private SpriteRenderer rend;

    // Lifetime
    private void Awake()
    {
        rB = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    public void Init(float damage, float speed, float lifetime, List<ProjectileModifierEntry> modifiers)
    {
        this.damage = damage;
        this.speed = speed;
        this.lifetime = lifetime;
        this.modifiers = modifiers;
    }

    private void OnEnable()
    {
        foreach (var modifierEntry in modifiers)
        {
            modifierEntry.modifier.OnProjEnable(this);
        }

        rend.color = CalcProjectileColor();
        rB.velocity = CalcVelocity();
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
    private Vector2 CalcVelocity() => transform.up * speed;

    private Color CalcProjectileColor() => rend.color * Mathf.Sqrt((damage / 10f));

    private void EnemyCollision(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // Get enemy HP and subtract damage.
            // Activate projectile effects on enemy.
            foreach (var modifierEntry in modifiers)
            {
                modifierEntry.modifier.OnHit(this);
            }
            Destroy(gameObject); // TODO: Object pool
        }
    }
}
