using System;
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
    public EventHandler<InputModeEventArgs> InputModeChanged = delegate { };

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

        aimAction.performed += CheckControlSwitch;
        moveAction.performed += CheckControlSwitch;
        mousePos.performed += CheckControlSwitch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckControlSwitch(InputAction.CallbackContext ctx)
    {
        if (inputMode == InputMode.KBM && ctx.control.device.displayName.Contains("Controller"))
        {
            inputMode = InputMode.Controller;
            InputModeChanged.Invoke(this, new InputModeEventArgs(inputMode));
        }
        else if (inputMode == InputMode.Controller && !ctx.control.device.displayName.Contains("Controller"))
        {
            inputMode = InputMode.KBM;
            InputModeChanged.Invoke(this, new InputModeEventArgs(inputMode));
        }
    }

    public class InputModeEventArgs : EventArgs
    {
        public InputMode inputMode;

        public InputModeEventArgs(InputMode inputMode)
        {
            this.inputMode = inputMode;
        }
    }
}
