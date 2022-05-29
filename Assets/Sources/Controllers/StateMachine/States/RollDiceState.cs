using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class RollDiceState : State<BounceGame>
{
    private static RollDiceState instance;
    public static RollDiceState Instance()
    {
        if (instance == null)
            instance = new RollDiceState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter RollDiceState");
        EventManager.Instance().fire(InGameEventType.POPUP, "Roll The Dice");
        StartRollDice(t);
        
        
        ////Can change state to out bound, arrcording to the dice. Now only change to recovery state
        //t.changeState(RecoverState.Instance());
    }

    public override void Execute(BounceGame t)
    {
        var time = t.GetProperty("RollDiceStateTimeCounter");
        if ( time != null )
            if( Time.time - (float)time > 1f )
            {
                EventManager.Instance().fire(InGameEventType.ROLL_DICE, null);
                t.RemoveProperty("RollDiceStateTimeCounter");
            }
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit RollDiceState");
        t.RemoveProperty("RollDiceStateTimeCounter");
        t.RemoveProperty("RollDiceBouncableTiles");
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        switch(aEventType)
        {
            case InGameEventType.ROLL_DONE:
                Dictionary<int, Vector2> map = (Dictionary<int, Vector2>)t.GetProperty("RollDiceBouncableTiles");
                EventManager.Instance().fire(InGameEventType.DEACTIVE_ROLLABLE_TO_TILES, map);
                int rolledToTile = (int)aEventData;
                var diceBonus = t.currentPlayer.GetTotalBuffValue(BUFF_TYPE.DICE);
                if( diceBonus != 0 )
                {
                    rolledToTile += diceBonus;
                    if (rolledToTile > 9)
                        rolledToTile = 0;
                    if (rolledToTile < 0)
                        rolledToTile = 0;
                    EventManager.Instance().fire(InGameEventType.POPUP, "Increase your dice by " + diceBonus + " to " + rolledToTile);
                }
                //Debug.Log("ROLL DONE!!!: " + rolledToTile);
                if( rolledToTile == 0 )
                {
                    if (t.currentPlayer.GetTotalBuffValue(BUFF_TYPE.NO_ZERO_NET_HITTING) != 0)
                    {
                        EventManager.Instance().fire(InGameEventType.POPUP, "PLEASE ROLL AGAIN");
                        StartRollDice(t);
                    }
                    else
                        //Hit the Net
                        t.changeState(HitTheNetState.Instance());
                    return;
                }
                Vector2 tilePos = map[rolledToTile];
                t.firstBounce.SetPosition(tilePos);
                EventManager.Instance().fire(InGameEventType.SET_BALL, t);
               // Debug.Log("Check Tile: " + tilePos.ToString() +" in field!!:" + CheckInField(tilePos, t));


                var strikeTime = GetStrikeTime(t);
                //First Time Strike = Serve Strike
                if( strikeTime == 0 )
                {
                    if (CheckInServableTile(tilePos, t))
                    {
                        t.changeState(RecoverState.Instance());
                    }
                    else
                    {
                        t.changeState(ServerOutOfBound.Instance());
                    }
                }
                else
                {
                    if (CheckInField(tilePos, t))
                    {
                        t.changeState(RecoverState.Instance());
                    }
                    else
                    {
                        t.changeState(OutOfBoundState.Instance());
                    }
                }


                break;
        }
    }

    private Dictionary<int, Vector2> GetBouncableTiles(Vector2 center)
    {
        Dictionary<int, Vector2> map = new Dictionary<int, Vector2>();

        map.Add(1, new Vector2(center.x - 1, center.y + 1));
        map.Add(2, new Vector2(center.x, center.y + 1));
        map.Add(3, new Vector2(center.x + 1, center.y + 1));
        map.Add(4, new Vector2(center.x - 1, center.y ));
        map.Add(5, new Vector2(center.x, center.y));
        map.Add(6, new Vector2(center.x + 1, center.y));
        map.Add(7, new Vector2(center.x - 1, center.y - 1));
        map.Add(8, new Vector2(center.x, center.y - 1));
        map.Add(9, new Vector2(center.x + 1, center.y - 1));
        return map;
    }

    private int GetStrikeTime(BounceGame t)
    {
        var obj = t.GetProperty("strikeTimes");
        int strikeTime = 0;
        if (obj != null)
            strikeTime = (int)obj;
        else
            t.SetProperty("strikeTimes", 0);
        return strikeTime;
    }

    private bool CheckInField( Vector2 tile, BounceGame t )
    {
        BaseGround ground = t.GetGround();
        return ( Utils.CheckVector2ExistInList( tile, ground.player1StrikableTile )
            || Utils.CheckVector2ExistInList(tile, ground.player2StrikableTile));
    }

    private bool CheckInServableTile(Vector2 tile, BounceGame t )
    {
        var obj = t.GetProperty("serveTimes");
        int serveTime = 0;
        if (obj != null)
            serveTime = (int)obj;
        else
            t.SetProperty("serveTimes", 0);
        if (serveTime % 2 == 0)
        {
            if (t.currentServer == t.firstPlayer)
            {
                return Utils.CheckVector2ExistInList(tile, t.GetGround().player1UpServeStrikableTiles);
            }
            else
            {
                return Utils.CheckVector2ExistInList(tile, t.GetGround().player2DownServeStrikableTiles);
            }
        }
        else
        {
            if (t.currentServer == t.firstPlayer)
            {
                return Utils.CheckVector2ExistInList(tile, t.GetGround().player1DownServeStrikableTiles);
            }
            else
            {
                return Utils.CheckVector2ExistInList(tile, t.GetGround().player2UpServeStrikableTiles);
            }

        }
    }


    private void StartRollDice(BounceGame t)
    {
        EventManager.Instance().fire(InGameEventType.RESET_COLLISION, null);
        var bouncableTiles = GetBouncableTiles(t.firstBounce.GetPosition());
        t.SetProperty("RollDiceBouncableTiles", bouncableTiles);
        EventManager.Instance().fire(InGameEventType.ACTIVE_ROLLABLE_TO_TILES, bouncableTiles);
        t.SetProperty("RollDiceStateTimeCounter", Time.time);
    }


}

