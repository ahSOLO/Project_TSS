using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : IState
{
    PlayerController pC;

    public PlayerIdle(PlayerController pC)
    {
        this.pC = pC;
    }
    
    public void FixedTick()
    {
        pC.rB.velocity *= (1 - pC.friction);

        if (Mathf.Abs(pC.rB.velocity.x) <= 0.01f && Mathf.Abs(pC.rB.velocity.y) <= 0.01f)
        {
            pC.rB.velocity = Vector2.zero;
        }
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
