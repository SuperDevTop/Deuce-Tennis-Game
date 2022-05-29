using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class ServeState : State<BounceGame>
{
    private static ServeState instance;
    public static ServeState Instance()
    {
        if (instance == null)
            instance = new ServeState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter ServeState");

        var obj = t.GetProperty("serveTimes");
        int serveTime = 0;
        if (obj != null)
            serveTime = (int)obj;
        else
            t.SetProperty("serveTimes", 0);

        //Dictionary<string, Vector2> playerPositionEventData = new Dictionary<string, Vector2>();
        //Vector2 player1Pos;
        //Vector2 player2Pos;
        List<Vector2> p1ServableTiles;
        List<Vector2> p2ServableTiles;
        if( serveTime % 2 == 0 )
        {
        //    player1Pos = t.GetGround().player1ServeDownPos;
        //    player2Pos = t.GetGround().player2ServeLeftPos;
            p1ServableTiles = t.GetGround().player1UpServableTiles;
            p2ServableTiles = t.GetGround().player2DownServableTiles;
        }
        else
        {
        //    player1Pos = t.GetGround().player1ServeUpPos;
        //    player2Pos = t.GetGround().player2ServeDownPos;
            p1ServableTiles = t.GetGround().player1DownServableTiles;
            p2ServableTiles = t.GetGround().player2UpServableTiles;

        }
        //playerPositionEventData.Add("player1Pos", player1Pos);
        //playerPositionEventData.Add("player2Pos", player2Pos);

        //EventManager.Instance().fire(InGameEventType.SET_PLAYERS_POSITION, playerPositionEventData);

        //t.firstPlayer.SetPosition((int)player1Pos.x, (int)player1Pos.y);
        //t.secondPlayer.SetPosition((int)player2Pos.x, (int)player2Pos.y);

        if( t.currentServer == t.firstPlayer )
        {
            EventManager.Instance().fire(InGameEventType.ACTIVE_SERVABLE_FIELD, p1ServableTiles);
            EventManager.Instance().fire(InGameEventType.POPUP, "P1 Choose where to serve");
        }
        else
        {
            EventManager.Instance().fire(InGameEventType.ACTIVE_SERVABLE_FIELD, p2ServableTiles);
            EventManager.Instance().fire(InGameEventType.POPUP, "P2 Choose where to serve");
        }

        EventManager.Instance().fire(InGameEventType.HIDE_BALL, null);

        Utils.SetTacticCardData(t);
    }

    public override void Execute(BounceGame t)
    {
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit ServeState");
        EventManager.Instance().fire(InGameEventType.DEACTIVE_SERVABLE_FIELD, t.GetGround().player1UpServableTiles);
        EventManager.Instance().fire(InGameEventType.DEACTIVE_SERVABLE_FIELD, t.GetGround().player2UpServableTiles);
        EventManager.Instance().fire(InGameEventType.DEACTIVE_SERVABLE_FIELD, t.GetGround().player1DownServableTiles);
        EventManager.Instance().fire(InGameEventType.DEACTIVE_SERVABLE_FIELD, t.GetGround().player2DownServableTiles);
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        switch (aEventType)
        {
            case InGameEventType.INPUT:
                BaseObject input = (BaseObject)aEventData;
                //Debug.Log("Type: " + input.GetProperty("inputType").ToString());
                //Debug.Log("Data: " + input.GetProperty("inputData").ToString());
                InputType type = (InputType)input.GetProperty("inputType");
                switch (type)
                {
                    case InputType.SELECT_TILE:
                        Vector2 ballPos = (Vector2)input.GetProperty("inputData");
                        t.firstBounce.SetPosition(ballPos);
                        t.changeState(RollDiceState.Instance());
                        //EventManager.Instance().fire(InGameEventType.SET_BALL, ballPos);

                        break;
                }
                break;
        }
    }
}

