using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class OutOfBoundState : State<BounceGame>
{
    private static OutOfBoundState instance;
    public static OutOfBoundState Instance()
    {
        if (instance == null)
            instance = new OutOfBoundState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter OutOfBoundState");
        EventManager.Instance().fire(InGameEventType.POPUP, "Out Of Bound");
        var obj2 = t.GetProperty("strikeTimes");
        int strikeTime = 0;
        if (obj2 != null)
            strikeTime = (int)obj2;
        else
            t.SetProperty("strikeTimes", 0);

        Debug.Log("OutOfBoundState: strikeTime: " + strikeTime);
        IncreasePoint(t);
        //if( strikeTime != 0 )
        //{
        //    IncreasePoint(t);
        //}
        //else
        //{
        //    var obj = t.GetProperty("serveFailTimes");
        //    int serveFailTime = 0;
        //    if (obj != null)
        //        serveFailTime = (int)obj;
        //    else
        //        t.SetProperty("serveFailTimes", 0);

        //    Debug.Log("OutOfBoundState: serveFailTime: " + serveFailTime);
        //    if( serveFailTime != 0)
        //    {
        //        //t.SetProperty("serveFailTimes", 0);
        //        IncreasePoint(t);
        //    }
        //}


        t.SetProperty("OOBTimeCounter", Time.time);
    }

    private void IncreasePoint(BounceGame t)
    {
        if (t.currentPlayer == t.firstPlayer)
        {
            Debug.Log("OutOfBoundState: current Player 1");
            t.gameScore.IncreasePoint(t.secondPlayer);
            t.pointWinner = t.secondPlayer;

        }
        else
        {
            Debug.Log("OutOfBoundState: current Player 2");
            t.gameScore.IncreasePoint(t.firstPlayer);
            t.pointWinner = t.firstPlayer;

        }

    }

    public override void Execute(BounceGame t)
    {
        var time = t.GetProperty("OOBTimeCounter");
        if (time != null)
            if (Time.time - (float)time > 1f)
            {
                t.changeState(CalculateScore.Instance());
                //var obj2 = t.GetProperty("strikeTimes");
                //int strikeTime = 0;
                //if (obj2 != null)
                //    strikeTime = (int)obj2;
                //else
                //    t.SetProperty("strikeTimes", 0);

                //Debug.Log("MissTheBallState: strikeTime: " + strikeTime);

                //if (strikeTime > 0)
                //{
                //    t.changeState(CalculateScore.Instance());
                //}
                //else
                //{
                //    var obj = t.GetProperty("serveFailTimes");
                //    int serveFailTime = 0;
                //    if (obj != null)
                //        serveFailTime = (int)obj;
                //    else
                //        t.SetProperty("serveFailTimes", 0);
                //    if (serveFailTime == 0 && t.currentPlayer == t.currentServer)
                //    {
                //        t.SetProperty("serveFailTimes", 1);
                //        t.changeState(ServeState.Instance());
                //    }
                //    else
                //    {
                //        t.changeState(CalculateScore.Instance());
                //    }

                //}
                t.RemoveProperty("OOBTimeCounter");
            }
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit OutOfBoundState");
        t.RemoveProperty("OOBTimeCounter");
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        throw new NotImplementedException();
    }
}

