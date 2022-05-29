using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class RecoverState : State<BounceGame>
{
    private static RecoverState instance;
    public static RecoverState Instance()
    {
        if (instance == null)
            instance = new RecoverState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter RecoverState");
        EventManager.Instance().fire(InGameEventType.POPUP, "Recovery Move");
        var currentPlayerPos = t.currentPlayer.GetPosition();
        var tileSpd = t.GetGround().GetTile((int)currentPlayerPos.x, (int)currentPlayerPos.y).SPD;

        //TODO calculate spd include bonus and debuff later, note that if spd < 0 => no move
        var playerSpd = tileSpd + t.currentPlayer.GetTotalBuffValue(BUFF_TYPE.RECOVER_SPD);
        List<Vector2> movableField = new List<Vector2>();
        for( int x = -playerSpd; x <= playerSpd; x++ )
        {
            for( int y = -playerSpd; y <= playerSpd; y++ )
            {
                if( Math.Abs(x) + Math.Abs(y) <= playerSpd )
                {
                    Vector2 movableTile = new Vector2(currentPlayerPos.x + x, currentPlayerPos.y + y);
                    if (movableTile.x < 0 || movableTile.x >= t.GetGround().GetGroundLength()
                        || movableTile.y < 0 || movableTile.y >= t.GetGround().GetGroundWidth())
                        continue;
                    if (t.currentPlayer == t.firstPlayer)
                    {
                        if (movableTile.x >= t.GetGround().GetGroundLength() / 2)
                            continue;
                    }
                    else
                    {
                        if (movableTile.x < t.GetGround().GetGroundLength() / 2)
                            continue;
                    }
                    movableField.Add(movableTile);
                }
            }
        }
        EventManager.Instance().fire(InGameEventType.ACTIVE_SERVABLE_FIELD, movableField);
        t.SetProperty("RecoveryState_MovableField", movableField);

        var obj2 = t.GetProperty("strikeTimes");
        int strikeTime = 0;
        if (obj2 != null)
            strikeTime = (int)obj2;
        else
            t.SetProperty("strikeTimes", 0);
        t.SetProperty("strikeTimes", strikeTime + 1);
    }

    public override void Execute(BounceGame t)
    {
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit RecoverState");
        List<Vector2> movableField = (List<Vector2>)t.GetProperty("RecoveryState_MovableField");
        //Debug.Log("movableField: " + movableField.Count);
        EventManager.Instance().fire(InGameEventType.DEACTIVE_MOVABLE_FIELD, movableField);
        t.RemoveProperty("RecoveryState_MovableField");
        t.currentPlayer.ResetBuffAndDebuff(BUFF_DURATION.STROKE);
        if (t.currentPlayer == t.firstPlayer)
            t.currentPlayer = t.secondPlayer;
        else
            t.currentPlayer = t.firstPlayer;
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        switch (aEventType)
        {
            case InGameEventType.INPUT:
                BaseObject input = (BaseObject)aEventData;
                Debug.Log("HandleInput RecoverState");
                InputType type = (InputType)input.GetProperty("inputType");
                switch (type)
                {
                    case InputType.SELECT_TILE:
                        Vector2 movePos = (Vector2)input.GetProperty("inputData");
                        t.currentPlayer.SetPosition((int)movePos.x, (int)movePos.y);
                        Dictionary<string, Vector2> playerPositionEventData = new Dictionary<string, Vector2>();
                        if (t.currentPlayer == t.firstPlayer)
                            playerPositionEventData.Add("player1Pos", movePos);
                        else
                            playerPositionEventData.Add("player2Pos", movePos);
                        EventManager.Instance().fire(InGameEventType.SET_PLAYERS_POSITION, playerPositionEventData);
                        t.changeState(MoveState.Instance());

                        break;
                }
                break;
        }

    }
}

