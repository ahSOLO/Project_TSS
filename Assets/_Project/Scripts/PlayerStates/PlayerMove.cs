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
        Vector2 dir = pC.moveAction.ReadValue<Vector2>();
        pC.rB.SetRotation(Quaternion.RotateTowards(pC.transform.rotation, Quaternion.LookRotation(Vector3.forward, dir), pC.rotationSpeed * Time.deltaTime));
        pC.rB.velocity = pC.transform.up * dir.magnitude * pC.moveSpeed * Time.fixedDeltaTime;
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
