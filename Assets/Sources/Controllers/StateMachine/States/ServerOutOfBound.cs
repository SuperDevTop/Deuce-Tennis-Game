using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class ServerOutOfBound : State<BounceGame>
{
    private static ServerOutOfBound instance;
    public static ServerOutOfBound Instance()
    {
        if (instance == null)
            instance = new ServerOutOfBound();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter ServerOutOfBound");
        EventManager.Instance().fire(InGameEventType.POPUP, "FOUL");


            var obj = t.GetProperty("serveFailTimes");
            int serveFailTime = 0;
            if (obj != null)
                serveFailTime = (int)obj;
            else
                t.SetProperty("serveFailTimes", 0);

            Debug.Log("ServerOutOfBound: serveFailTime: " + serveFailTime);
            if (serveFailTime != 0)
            {
                IncreasePoint(t);
            }


        t.SetProperty("SOUBTimeCounter", Time.time);
        
    }

    private void IncreasePoint(BounceGame t)
    {
        if (t.currentPlayer == t.firstPlayer)
        {
            Debug.Log("ServerOutOfBound: current Player 1");
            t.gameScore.IncreasePoint(t.secondPlayer);
            t.pointWinner = t.secondPlayer;

        }
        else
        {
            Debug.Log("ServerOutOfBound: current Player 2");
            t.gameScore.IncreasePoint(t.firstPlayer);
            t.pointWinner = t.firstPlayer;
        }

    }

    public override void Execute(BounceGame t)
    {
        var time = t.GetProperty("SOUBTimeCounter");
        if (time != null)
            if (Time.time - (float)time > 1f)
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
                }
                else
                {
                    t.changeState(CalculateScore.Instance());
                }

                t.RemoveProperty("SOUBTimeCounter");
            }
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit ServerOutOfBound");
        t.RemoveProperty("SOUBTimeCounter");

        EventManager.Instance().fire(InGameEventType.HIDE_BALL, null);
        EventManager.Instance().fire(InGameEventType.HIDE_PLAYERS, null);
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        throw new NotImplementedException();
    }
}

