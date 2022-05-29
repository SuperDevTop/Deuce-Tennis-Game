using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class CheckStrikableState : State<BounceGame>
{
    private static CheckStrikableState instance;
    public static CheckStrikableState Instance()
    {
        if (instance == null)
            instance = new CheckStrikableState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter CheckStrikableState");
        //Implement the check strikable logic here
        //Now just consider it will alway strikable
        //t.changeState(ChooseStrokeCardState.Instance());
        EventManager.Instance().fire(InGameEventType.CHECK_STRIKABLE, t.currentPlayer);
    }

    public override void Execute(BounceGame t)
    {
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit CheckStrikableState");
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        switch( aEventType)
        {
            case InGameEventType.CHECK_STRIKABLE_DONE:
                bool strikable = (bool)aEventData;
                Debug.Log("CheckStrikableState: " + strikable);
                if( strikable )
                {
                    var pos = t.currentPlayer.GetPosition();
                    t.currentPlayer.SetLastStrikePosition((int)pos.x, (int)pos.y);
                    t.changeState(ChooseStrokeCardState.Instance());
                }
                else
                {
                    t.changeState(MissTheBallState.Instance());

                }
                break;
        }
    }
}

