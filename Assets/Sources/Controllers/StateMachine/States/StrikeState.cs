using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class StrikeState : State<BounceGame>
{
    private static StrikeState instance;
    public static StrikeState Instance()
    {
        if (instance == null)
            instance = new StrikeState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter StrikeState");
        EventManager.Instance().fire(InGameEventType.POPUP, "Choose where to strike");

        List<Vector2> strikableTile;
        if (t.currentPlayer == t.firstPlayer)
            strikableTile = t.GetGround().player1SelectableTile;
        else
            strikableTile = t.GetGround().player2SelectableTile;
        EventManager.Instance().fire(InGameEventType.ACTIVE_SERVABLE_FIELD, strikableTile);
    }

    public override void Execute(BounceGame t)
    {
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit StrikeState");
        List<Vector2> strikableTile;
        if (t.currentPlayer == t.firstPlayer)
            strikableTile = t.GetGround().player1StrikableTile;
        else
            strikableTile = t.GetGround().player2StrikableTile;
        EventManager.Instance().fire(InGameEventType.DEACTIVE_SERVABLE_FIELD, strikableTile);
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        switch (aEventType)
        {
            case InGameEventType.INPUT:
                BaseObject input = (BaseObject)aEventData;
                Debug.Log("Type: " + input.GetProperty("inputType").ToString());
                Debug.Log("Data: " + input.GetProperty("inputData").ToString());
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

