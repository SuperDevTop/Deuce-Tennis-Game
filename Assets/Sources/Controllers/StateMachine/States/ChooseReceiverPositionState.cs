using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class ChooseReceiverPositionState : State<BounceGame>
{
    private static ChooseReceiverPositionState instance;
    public static ChooseReceiverPositionState Instance()
    {
        if (instance == null)
            instance = new ChooseReceiverPositionState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter ChooseReceiverPositionState");
        EventManager.Instance().fire(InGameEventType.POPUP, "Choose the Receiver's Position");

        var obj = t.GetProperty("serveTimes");
        int serveTime = 0;
        if (obj != null)
            serveTime = (int)obj;
        else
            t.SetProperty("serveTimes", 0);

        Dictionary<string, Vector2> playerPositionEventData = new Dictionary<string, Vector2>();
        List<Vector2> receiverStartTiles;
        if( serveTime % 2 == 0 )
        {
            if( t.ReceivePlayer == t.firstPlayer )
            {
                receiverStartTiles = t.GetGround().leftStartDownTiles;
            }
            else
            {
                receiverStartTiles = t.GetGround().rightStartUpTiles;
            }
        }
        else
        {
            if (t.ReceivePlayer == t.firstPlayer)
            {
                receiverStartTiles = t.GetGround().leftStartUpTiles;
            }
            else
            {
                receiverStartTiles = t.GetGround().rightStartDownTiles;
            }

        }

        t.SetProperty("ChooseReceiverPositionState_receiverStartTiles", receiverStartTiles);
        EventManager.Instance().fire(InGameEventType.ACTIVE_START_POSITION, receiverStartTiles);


    }

    public override void Execute(BounceGame t)
    {
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit ChooseReceiverPositionState");
        List<Vector2> receiverStartTiles = (List<Vector2>)t.GetProperty("ChooseReceiverPositionState_receiverStartTiles");
        Debug.Log("serverStartTiles: " + receiverStartTiles.Count);
        EventManager.Instance().fire(InGameEventType.DEACTIVE_START_POSITION, receiverStartTiles);
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        switch (aEventType)
        {
            case InGameEventType.INPUT:
                BaseObject input = (BaseObject)aEventData;
                InputType type = (InputType)input.GetProperty("inputType");
                switch (type)
                {
                    case InputType.SELECT_TILE:
                        Vector2 pos = (Vector2)input.GetProperty("inputData");
                        Dictionary<string, Vector2> playerPositionEventData = new Dictionary<string, Vector2>();
                        if (t.ReceivePlayer == t.firstPlayer)
                        {
                            playerPositionEventData.Add("player1Pos", pos);
                        }
                        else
                        {
                            playerPositionEventData.Add("player2Pos", pos);
                        }
                        EventManager.Instance().fire(InGameEventType.SET_PLAYERS_POSITION, playerPositionEventData);
                        t.ReceivePlayer.SetPosition((int)pos.x, (int)pos.y);
                        t.changeState(ServeState.Instance());
                        break;
                }
                break;
        }
    }
}

