using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class StartGameState : State<BounceGame>
{
    private static StartGameState instance;
    public static StartGameState Instance()
    {
        if (instance == null)
            instance = new StartGameState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter Start Game");
        Dictionary<string, int> scoreEventData = new Dictionary<string, int>();
        scoreEventData.Add("player1Score", t.GetGameScore(t.firstPlayer));
        scoreEventData.Add("player2Score", t.GetGameScore(t.secondPlayer));
        scoreEventData.Add("player1Point", t.GetGamePoint(t.firstPlayer));
        scoreEventData.Add("player2Point", t.GetGamePoint(t.secondPlayer));
        EventManager.Instance().fire(InGameEventType.UPDATE_GAME_SCORE, scoreEventData);
        
    }

    public override void Execute(BounceGame t)
    {
        object timer = t.GetProperty("timer");
        if( timer != null )
        {
            float timerValue = Convert.ToSingle(timer) + Time.deltaTime;
            if (timerValue > 1)
            {
                t.changeState(P1ChooseTacticCardState.Instance());
            }
            else
                t.SetProperty("timer", timerValue);            
        }
        else
        {
            t.SetProperty("timer", 0);  
        }
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit Start Game");
        t.RemoveProperty("timer");
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        throw new NotImplementedException();
    }
}

