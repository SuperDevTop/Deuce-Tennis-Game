using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class ChooseServePositionState : State<BounceGame>
{
    private static ChooseServePositionState instance;
    public static ChooseServePositionState Instance()
    {
        if (instance == null)
            instance = new ChooseServePositionState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter ChooseServePositionState");
        EventManager.Instance().fire(InGameEventType.POPUP, "Choose the Server's Position");

        var obj = t.GetProperty("serveTimes");
        int serveTime = 0;
        if (obj != null)
            serveTime = (int)obj;
        else
            t.SetProperty("serveTimes", 0);
        Debug.Log("Server Times: " + serveTime);
        Dictionary<string, Vector2> playerPositionEventData = new Dictionary<string, Vector2>();
        List<Vector2> serverStartTiles;
        if( serveTime % 2 == 0 )
        {
            if( t.currentServer == t.firstPlayer )
            {
                serverStartTiles = t.GetGround().leftStartDownTiles;
            }
            else
            {
                serverStartTiles = t.GetGround().rightStartUpTiles;
            }
        }
        else
        {
            if (t.currentServer == t.firstPlayer)
            {
                serverStartTiles = t.GetGround().leftStartUpTiles;
            }
            else
            {
                serverStartTiles = t.GetGround().rightStartDownTiles;
            }

        }
        t.SetProperty("ChooseServePositionState_serverStartTiles", serverStartTiles);
        EventManager.Instance().fire(InGameEventType.ACTIVE_START_POSITION, serverStartTiles);
    }

    public override void Execute(BounceGame t)
    {
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit ChooseServePositionState");

        List<Vector2> serverStartTiles = (List<Vector2>)t.GetProperty("ChooseServePositionState_serverStartTiles");
        Debug.Log("serverStartTiles: " + serverStartTiles.Count);
        EventManager.Instance().fire(InGameEventType.DEACTIVE_START_POSITION, serverStartTiles);
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
                        if (t.currentServer == t.firstPlayer)
                        {
                            playerPositionEventData.Add("player1Pos", pos);
                        }
                        else
                        {
                            playerPositionEventData.Add("player2Pos", pos);
                        }
                        EventManager.Instance().fire(InGameEventType.SET_PLAYERS_POSITION, playerPositionEventData);
                        t.currentServer.SetPosition((int)pos.x, (int)pos.y);
                        t.changeState(ChooseReceiverPositionState.Instance());
                        break;
                }
                break;
        }
    }
}

