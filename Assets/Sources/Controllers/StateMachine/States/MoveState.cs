using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class MoveState : State<BounceGame>
{
    private static MoveState instance;
    public static MoveState Instance()
    {
        if (instance == null)
            instance = new MoveState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter MoveState");
        EventManager.Instance().fire(InGameEventType.POPUP, "Move to strike");
        var currentPlayerPos = t.currentPlayer.GetPosition();
        var tileSpd = t.GetGround().GetTile((int)currentPlayerPos.x, (int)currentPlayerPos.y).SPD;

        //TODO calculate spd include bonus and debuff later, note that if spd < 0 => no move
        var playerSpd = tileSpd + t.currentPlayer.GetTotalBuffValue(BUFF_TYPE.MOVEMENT_SPD);
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
                    } else
                    {
                        if (movableTile.x < t.GetGround().GetGroundLength() / 2)
                            continue;
                    }
                    movableField.Add(movableTile);
                }
            }
        }
        EventManager.Instance().fire(InGameEventType.ACTIVE_MOVABLE_FIELD, movableField);
        var dataMap = new Dictionary<string, object>();
        dataMap["player"] = t.currentPlayer;
        dataMap["movableField"] = movableField;
        EventManager.Instance().fire(InGameEventType.ACTIVE_STRIKABLE_TILES, dataMap);
        EventManager.Instance().fire(InGameEventType.DEACTIVE_COLLISION_BOX, null);
        t.SetProperty("MoveState_MovableField", movableField);
        Utils.SetTacticCardData(t);
    }

    public override void Execute(BounceGame t)
    {
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit MoveState");
        List<Vector2> movableField = (List<Vector2>)t.GetProperty("MoveState_MovableField");
        Debug.Log("movableField: " + movableField.Count);
        EventManager.Instance().fire(InGameEventType.DEACTIVE_MOVABLE_FIELD, movableField);
        EventManager.Instance().fire(InGameEventType.DEACTIVE_STRIKABLE_TILES, movableField);
        EventManager.Instance().fire(InGameEventType.ACTIVE_COLLISION_BOX, null);
        t.RemoveProperty("MoveState_MovableField");
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        switch( aEventType )
        {
            case InGameEventType.INPUT:
                BaseObject input = (BaseObject)aEventData;
                Debug.Log("HandleInput MoveState");
                InputType type = (InputType)input.GetProperty("inputType");
                switch (type)
                {
                    case InputType.SELECT_TILE:
                        Vector2 movePos = (Vector2)input.GetProperty("inputData");
                        t.currentPlayer.SetPosition((int)movePos.x, (int)movePos.y);
                        Dictionary<string, Vector2> playerPositionEventData = new Dictionary<string, Vector2>();
                        if( t.currentPlayer == t.firstPlayer )
                            playerPositionEventData.Add("player1Pos", movePos);
                        else
                            playerPositionEventData.Add("player2Pos", movePos);
                        EventManager.Instance().fire(InGameEventType.SET_PLAYERS_POSITION, playerPositionEventData);


                        t.changeState(CheckStrikableState.Instance());

                        break;
                }
                break;
        }
        

    }
}

