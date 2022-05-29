using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class EndGameState : State<BounceGame>
{
    private static EndGameState instance;
    public static EndGameState Instance()
    {
        if (instance == null)
            instance = new EndGameState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter EndGameState");
        EventManager.Instance().fire(InGameEventType.POPUP, "GAME END!!!!!!");
        
    }

    public override void Execute(BounceGame t)
    {
        
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit EndGameState");
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        throw new NotImplementedException();
    }
}

