﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class HitTheNetState : State<BounceGame>
{
    private static HitTheNetState instance;
    public static HitTheNetState Instance()
    {
        if (instance == null)
            instance = new HitTheNetState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter HitTheNetState");
        EventManager.Instance().fire(InGameEventType.POPUP, "Hit The Net");
        EventManager.Instance().fire(InGameEventType.HIDE_BALL, null);


        var obj2 = t.GetProperty("strikeTimes");
        int strikeTime = 0;
        if (obj2 != null)
            strikeTime = (int)obj2;
        else
            t.SetProperty("strikeTimes", 0);

        Debug.Log("HitTheNetState: strikeTime: " + strikeTime);
        if (strikeTime != 0)
        {
            IncreasePoint(t);
        }
        else
        {
            var obj = t.GetProperty("serveFailTimes");
            int serveFailTime = 0;
            if (obj != null)
                serveFailTime = (int)obj;
            else
                t.SetProperty("serveFailTimes", 0);

            Debug.Log("HitTheNetState: serveFailTime: " + serveFailTime);
            if (serveFailTime != 0)
            {
                IncreasePoint(t);
            }
        }

        t.SetProperty("HTBTimeCounter", Time.time);
        
    }

    private void IncreasePoint(BounceGame t)
    {
        if (t.currentPlayer == t.firstPlayer)
        {
            Debug.Log("HitTheNetState: current Player 1");
            t.gameScore.IncreasePoint(t.secondPlayer);
            t.pointWinner = t.secondPlayer;

        }
        else
        {
            Debug.Log("HitTheNetState: current Player 2");
            t.gameScore.IncreasePoint(t.firstPlayer);
            t.pointWinner = t.firstPlayer;
        }

    }

    public override void Execute(BounceGame t)
    {
        var time = t.GetProperty("HTBTimeCounter");
        if (time != null)
            if (Time.time - (float)time > 1f)
            {

                var obj2 = t.GetProperty("strikeTimes");
                int strikeTime = 0;
                if (obj2 != null)
                    strikeTime = (int)obj2;
                else
                    t.SetProperty("strikeTimes", 0);

                Debug.Log("MissTheBallState: strikeTime: " + strikeTime);

                if (strikeTime > 0)
                {
                    t.changeState(CalculateScore.Instance());
                }
                else
                {
                    var obj = t.GetProperty("serveFailTimes");
                    int serveFailTime = 0;
                    if (obj != null)
                        serveFailTime = (int)obj;
                    else
                        t.SetProperty("serveFailTimes", 0);
                    if (serveFailTime == 0 && t.currentPlayer == t.currentServer)
                    {
                        t.SetProperty("serveFailTimes", 1);
                        t.changeState(ChooseServePositionState.Instance());
                        //t.changeState(ServeState.Instance());
                    }
                    else
                    {
                        t.changeState(CalculateScore.Instance());
                    }

                }
                t.RemoveProperty("HTBTimeCounter");
            }
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit HitTheNetState");
        t.RemoveProperty("HTBTimeCounter");

        EventManager.Instance().fire(InGameEventType.HIDE_BALL, null);
        EventManager.Instance().fire(InGameEventType.HIDE_PLAYERS, null);
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        throw new NotImplementedException();
    }
}

