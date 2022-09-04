using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public Transform projectileContainer;
    public float turnSpeed = 450f;
    public float cooldown;

    private InputAction aimAction;
    private InputAction mousePos;
    private InputAction fireAction;

    public GameObject projectilePrefab;

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

        if (fireAction.IsPressed())
        {
            FireProjectile();
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
        if (cooldown <= 0f)
        {
            var proj = Instantiate(projectilePrefab, projectileContainer, true);
            proj.transform.position = projectileSpawnPoint.position;
            proj.transform.rotation = projectileSpawnPoint.rotation;
            proj.gameObject.SetActive(true);
            cooldown = proj.GetComponent<Projectile>().cooldown;
        }
    }
}
