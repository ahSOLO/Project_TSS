using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputActionAsset playerInput;
    private InputAction aimAction;
    private InputAction moveAction;
    private InputAction mousePos;

    public enum InputMode { KBM, Controller };
    public InputMode inputMode;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput.Enable();

        aimAction = GameManager.Instance.playerInput.FindAction("Aim");
        moveAction = GameManager.Instance.playerInput.FindAction("Move");
        mousePos = GameManager.Instance.playerInput.FindAction("MousePos");

        aimAction.performed += ctx => Debug.Log(ctx.control.device.layout);
        moveAction.performed += ctx => Debug.Log(ctx.control.device.layout);
        mousePos.performed += ctx => Debug.Log(ctx.control.device.layout);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
