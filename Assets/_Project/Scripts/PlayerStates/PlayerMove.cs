using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : IState
{
    PlayerController pC;
    Vector2 inputDir;

    public PlayerMove(PlayerController pC)
    {
        this.pC = pC;
    }

    public void FixedTick()
    {
        pC.rB.velocity = pC.moveAction.ReadValue<Vector2>() * pC.moveSpeed * Time.fixedDeltaTime;
    }

    public void LateTick()
    {

    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void Tick()
    {

    }
}
