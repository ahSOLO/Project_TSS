using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    [RangeAttribute(0, 1)] public float friction = 0.233f;
    public float moveSpeed = 3f;
    public float rotationSpeed = 720f;
    public float idleMinSpeed = 1f;
    public Rigidbody2D rB;

    // Player SM
    private StateMachine playerSM;
    private PlayerIdle playerIdleState;
    private PlayerMove playerMoveState;

    // Input
    [HideInInspector] public InputAction moveAction;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rB = GetComponentInChildren<Rigidbody2D>();

        SetupPlayerSM();

        // Input
        moveAction = GameManager.instance.playerInput.FindAction("Move");
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

    public Vector2 GetPlayerPosition()
    {
        return transform.position;
    }
}
