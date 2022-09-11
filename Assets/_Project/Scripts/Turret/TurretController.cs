using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
    public static TurretController instance;
    
    private InputAction aimAction;
    private InputAction mousePos;
    private InputAction fireAction;

    [Header("Turret")]
    public float turnSpeed = 450f;
    private float cooldownTimer;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public Transform projectileContainer;

    public int projNumber = 1;
    public float projSpread = 0.5f;
    public float projRotSpread = 0f;
    public float projCooldown = 0.5f;
    public float projDamage = 10f;
    public float projSpeed = 10f;
    public float projLifetime = 3f;

    private float minSpread = 0.5f;
    private float maxSpread = 2f;
    private float minRotSpread = 0f;
    private float maxRotSpread = 120f;
    private float minCooldown = 0.2f;
    private float maxDamage = 9999f;
    private float minSpeed = 4f;
    private float maxSpeed = 24f;

    private List<ProjectileModifierEntry> modifiers;

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

        if (fireAction.IsPressed() && cooldownTimer <= 0f)
        {
            FireVolley();
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

        cooldownTimer -= Time.deltaTime;
    }

    private void RotateTowardsDest(Vector2 destination)
    {
        Quaternion rotationDest = Quaternion.LookRotation(Vector3.forward, destination);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationDest, turnSpeed * Time.deltaTime);
    }

    private void FireVolley()
    {
        if (projNumber == 1)
        {
            FireProjectile(projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            return;
        }

        Vector3 spreadLeftBound = projectileSpawnPoint.position - (projectileSpawnPoint.right * GetSpreadBounded() / 2f);
        Quaternion rotationSpreadLeftBound = Quaternion.Euler(projectileSpawnPoint.rotation.eulerAngles.x, projectileSpawnPoint.rotation.eulerAngles.y, projectileSpawnPoint.rotation.eulerAngles.z + GetRotSpreadBounded() / 2f);
        
        for (float i = 0.5f; i < projNumber; i++)
        {
            FireProjectile(
                spawnPosition: spreadLeftBound + projectileSpawnPoint.right * (i * GetSpreadBounded() / projNumber),
                spawnRotation: Quaternion.Euler(rotationSpreadLeftBound.eulerAngles.x, rotationSpreadLeftBound.eulerAngles.y, rotationSpreadLeftBound.eulerAngles.z - (i * GetRotSpreadBounded() / projNumber))
                );
        }
    }

    private void FireProjectile(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        var proj = Instantiate(projectilePrefab, projectileContainer, true);
        proj.GetComponent<Projectile>().Init(GetDamageBounded(), GetSpeedBounded(), projLifetime, modifiers, Faction.Player);
        proj.transform.position = spawnPosition;
        proj.transform.rotation = spawnRotation;
        proj.gameObject.SetActive(true);
        cooldownTimer = GetCooldownBounded();
    }

    public void AttachProjectileModifier(IProjectileModifier modifier, float duration)
    {
        modifiers.Add(new ProjectileModifierEntry(modifier, duration));

        modifier.OnAttach(this);
    }
    
    private float GetCooldownBounded() => Mathf.Max(minCooldown, projCooldown);
    private float GetSpeedBounded() => Mathf.Min(maxSpeed, Mathf.Max(minSpeed, projSpeed));
    private float GetDamageBounded() => Mathf.Min(maxDamage, projDamage);
    private float GetSpreadBounded() => Mathf.Min(maxSpread, Mathf.Max(minSpread, projSpread));
    private float GetRotSpreadBounded() => Mathf.Min(maxRotSpread, Mathf.Max(minRotSpread, projRotSpread));
}
