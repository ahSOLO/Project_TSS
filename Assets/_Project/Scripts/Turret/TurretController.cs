using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
    public static TurretController instance;
    
    public Transform projectileSpawnPoint;
    public Transform projectileContainer;
    public float turnSpeed = 450f;
    public float cooldown;

    private InputAction aimAction;
    private InputAction mousePos;
    private InputAction fireAction;

    public GameObject projectilePrefab;
    private Projectile loadedProjectile;

    public float projCooldown;
    public float projDamage;
    public float projSpeed;
    public float projLifetime;

    List<ProjectileModifierEntry> modifiers;

    private void Awake()
    {
        instance = this;

        modifiers = new List<ProjectileModifierEntry>();
    }

    private void Start()
    {
        aimAction = GameManager.instance.playerInput.FindAction("Aim");
        mousePos = GameManager.instance.playerInput.FindAction("MousePos");
        fireAction = GameManager.instance.playerInput.FindAction("Fire");
    }

    private void Update()
    {
        Vector2 aimVector;
        if (GameManager.instance.inputMode == GameManager.InputMode.Controller)
        {
            aimVector = aimAction.ReadValue<Vector2>();
            if (aimVector != Vector2.zero)
            {
                RotateTowardsDest(aimVector);
            }
        }
        else if (GameManager.instance.inputMode == GameManager.InputMode.KBM)
        {
            aimVector = Camera.main.ScreenToWorldPoint(mousePos.ReadValue<Vector2>()) - PlayerController.instance.transform.position;
            if (aimVector != Vector2.zero)
            {
                RotateTowardsDest(aimVector);                
            }
        }

        if (fireAction.IsPressed() && cooldown <= 0f)
        {
            FireProjectile();
        }

        for (int i = modifiers.Count - 1; i >= 0; i--)
        {
            if (modifiers[i].lifetime != -1)
            {
                modifiers[i].lifetime -= Time.deltaTime;
                if (modifiers[i].lifetime <= 0)
                {
                    modifiers[i].modifier.OnDetach(this);
                    modifiers.Remove(modifiers[i]);
                }
            }
        }

        cooldown -= Time.deltaTime;
    }

    private void RotateTowardsDest(Vector2 destination)
    {
        Quaternion rotationDest = Quaternion.LookRotation(Vector3.forward, destination);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationDest, turnSpeed * Time.deltaTime);
    }

    private void FireProjectile()
    {
        var proj = Instantiate(projectilePrefab, projectileContainer, true);
        proj.GetComponent<Projectile>().Init(projDamage, projSpeed, projLifetime, modifiers);
        proj.transform.position = projectileSpawnPoint.position;
        proj.transform.rotation = projectileSpawnPoint.rotation;
        proj.gameObject.SetActive(true);
        cooldown = GetCooldownBounded();
    }

    public void AttachProjectileModifier(IProjectileModifier modifier, float duration)
    {
        modifiers.Add(new ProjectileModifierEntry(modifier, duration));

        modifier.OnAttach(this);
    }

    public float GetCooldown() => projCooldown;
    public float GetCooldownBounded() => Mathf.Max(0.15f, projCooldown);
    public void SetCooldown(float cd) => projCooldown = cd;

    public float GetSpeed() => projSpeed;
    public float GetSpeedBounded() => Mathf.Min(25f, Mathf.Max(1f, projSpeed));
    public void SetSpeed(float speed) => projSpeed = speed;

    public float GetDamage() => projDamage;
    public float GetDamageBounded() => Mathf.Min(9999f, Mathf.Max(1f, projDamage));
    public void SetDamage(float damage) => projDamage = damage;
}
