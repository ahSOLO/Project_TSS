using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public float turnSpeed = 450f;

    private InputAction aimAction;
    private InputAction mousePos;

    private void Start()
    {
        aimAction = GameManager.Instance.playerInput.FindAction("Aim");
        mousePos = GameManager.Instance.playerInput.FindAction("MousePos");
    }

    private void Update()
    {
        Vector2 aimVector;
        if (GameManager.Instance.inputMode == GameManager.InputMode.Controller)
        {
            aimVector = aimAction.ReadValue<Vector2>();
            if (aimVector != Vector2.zero)
            {
                RotateTowardsDest(aimVector);
            }
        }
        else if (GameManager.Instance.inputMode == GameManager.InputMode.KBM)
        {
            aimVector = mousePos.ReadValue<Vector2>() - (new Vector2(Screen.width / 2, Screen.height / 2));
            if (aimVector != Vector2.zero)
            {
                RotateTowardsDest(aimVector);                
            }
        }
    }

    private void RotateTowardsDest(Vector2 destination)
    {
        Quaternion rotationDest = Quaternion.LookRotation(Vector3.forward, destination);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationDest, turnSpeed * Time.deltaTime);
    }
}
