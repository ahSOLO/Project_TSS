using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [RangeAttribute(0, 1)] public float friction = 0.233f;
    public float moveSpeed = 3f;
    public Rigidbody2D rB;

    // Player SM
    private StateMachine playerSM;
    private PlayerIdle playerIdleState;
    private PlayerMove playerMoveState;

    // Input
    public InputActionAsset playerInput;
    [HideInInspector] public InputAction moveAction;

    void Start()
    {
        rB = GetComponentInChildren<Rigidbody2D>();

        SetupPlayerSM();

        // Input
        playerInput.Enable();
        moveAction = playerInput.FindAction("Move");
    }

    private void SetupPlayerSM()
    {
        // Player State Machine
        playerSM = gameObject.AddComponent<StateMachine>();
        playerIdleState = new PlayerIdle(this);
        playerMoveState = new PlayerMove(this);

        playerSM.AddTransition(playerIdleState, () => moveAction.ReadValue<Vector2>() != Vector2.zero, playerMoveState);
        playerSM.AddTransition(playerMoveState, () => moveAction.ReadValue<Vector2>() == Vector2.zero, playerIdleState);

        playerSM.SetState(playerIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
