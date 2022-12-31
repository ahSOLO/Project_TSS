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
        var targetVeloc = pC.rB.velocity * (1 - (pC.friction * Time.fixedDeltaTime));
        if (targetVeloc.sqrMagnitude >= Mathf.Pow(pC.idleMinSpeed, 2))
        {
            pC.rB.velocity = targetVeloc;
        }
        else
        {
            pC.rB.velocity = targetVeloc.normalized * pC.idleMinSpeed;
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
