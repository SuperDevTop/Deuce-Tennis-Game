using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class CalculateScore : State<BounceGame>
{
    private static CalculateScore instance;
    public static CalculateScore Instance()
    {
        if (instance == null)
            instance = new CalculateScore();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter CalculateScore");


        //var obj = t.GetProperty("serveTimes");
        //int serveTime = 0;
        //if (obj != null)
        //    serveTime = (int)obj;
        //else
        //    t.SetProperty("serveTimes", 0);
        //t.SetProperty("serveTimes", serveTime + 1);
        IncreaseServeTime(t);
        HandleTacticCard(t);
        if (t.IsMatchOver())
        {
            if (t.currentServer == t.firstPlayer)
                t.currentServer = t.secondPlayer;
            else
                t.currentServer = t.firstPlayer;
            t.gameScore.IncreaseScoreForWinner();
            t.gameScore.SetPoint(t.firstPlayer, 0);
            t.gameScore.SetPoint(t.secondPlayer, 0);
            t.SetProperty("serveTimes", 0);//reset for new game
            //reset buff must be after score calculate, because some of the calculation require whole game buff
            t.firstPlayer.ResetBuffAndDebuff(BUFF_DURATION.WHOLE_GAME);
            t.secondPlayer.ResetBuffAndDebuff(BUFF_DURATION.WHOLE_GAME);
        }
        else
        {
            t.firstPlayer.ResetStrokeRemain();
            t.secondPlayer.ResetStrokeRemain();
            t.firstPlayer.ResetBuffAndDebuff(BUFF_DURATION.ONE_POINT);
            t.secondPlayer.ResetBuffAndDebuff(BUFF_DURATION.ONE_POINT);
        }

        t.currentPlayer = t.currentServer;
        Dictionary<string, int> scoreEventData = new Dictionary<string, int>();
        scoreEventData.Add("player1Score", t.GetGameScore(t.firstPlayer));
        scoreEventData.Add("player2Score", t.GetGameScore(t.secondPlayer));
        scoreEventData.Add("player1Point", t.GetGamePoint(t.firstPlayer));
        scoreEventData.Add("player2Point", t.GetGamePoint(t.secondPlayer));
        EventManager.Instance().fire(InGameEventType.UPDATE_GAME_SCORE, scoreEventData);

        t.SetProperty("CCTimeCounter", Time.time);
        
        
    }
    private void IncreaseServeTime(BounceGame t)
    {
        var obj = t.GetProperty("serveTimes");
        int serveTime = 0;
        if (obj != null)
            serveTime = (int)obj;
        else
            t.SetProperty("serveTimes", 0);
        t.SetProperty("serveTimes", serveTime + 1);

    }
    private void HandleTacticCard(BounceGame t)
    {
        HandleTacticCard(t, t.firstPlayer);
        HandleTacticCard(t, t.secondPlayer);
    }

    private void HandleTacticCard( BounceGame t, IPlayer player)
    {
        var obj2 = t.GetProperty("strikeTimes");
        int strikeTime = 0;
        if (obj2 != null)
            strikeTime = (int)obj2;
        else
            t.SetProperty("strikeTimes", 0);
        switch(player.GetCurrentTacticCard())
        {
            case TacticCardType.DOWN_THE_LINE:
                if (player == t.pointWinner)
                    if( CheckInDownTheLine( t.firstBounce.GetPosition(), t ))
                    {
                        player.IncreaseCurrentTacticCard(1);
                    }
                break;
            case TacticCardType.ON_THE_DEFENSIVE:

                if( strikeTime >= GameConfig.TACTIC_DEFENSIVE_STROKE)
                {
                    player.IncreaseCurrentTacticCard(1);
                }
                break;
            case TacticCardType.ON_THE_OFFENSIVE:
                if (strikeTime >= GameConfig.TACTIC_OFFENSIVE_STROKE)
                {
                    player.IncreaseCurrentTacticCard(1);
                }
                break;
            case TacticCardType.SMART_GAME:
                if (player == t.pointWinner)
                    if( player.GetStrokeUsed() == 0)
                    {
                        player.IncreaseCurrentTacticCard(1);
                    }
                break;
            case TacticCardType.TO_THE_NET:
                if (player == t.pointWinner)
                    if (CheckAtTheNet(player.GetLastStrikePosition(), t))
                    {                    
                        player.IncreaseCurrentTacticCard(1);
                    }
                break;
        }
        var tactics = player.GetTacticCardsData().Keys.ToArray();
        foreach (var tactic in tactics)
        {
            if( player.GetTacticCardsData()[tactic] >= 5 )
            {
                t.gameScore.IncreaseScore(player);
                player.GetTacticCardsData()[tactic] = -1;
            }
        }
    }
    public override void Execute(BounceGame t)
    {
        var time = t.GetProperty("CCTimeCounter");
        if (time != null)
            if (Time.time - (float)time > 1f)
            {
                if( t.isGameEnd() )
                {
                    t.changeState(EndGameState.Instance());
                }
                else
                {
                    var obj = t.GetProperty("serveTimes");
                    int serveTime = 0;
                    if (obj != null)
                        serveTime = (int)obj;
                    else
                        t.SetProperty("serveTimes", 0);
                    if (serveTime == 0)//new games
                    {
                        t.changeState(BonusCardState.Instance());
                    }
                    else
                        t.changeState(P1ChooseTacticCardState.Instance());
                }
                
                t.RemoveProperty("CCTimeCounter");
            }
    }


    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit CalculateScore");
        t.RemoveProperty("CCTimeCounter");
        t.SetProperty("serveFailTimes", 0);
        t.SetProperty("strikeTimes", 0);
        t.firstPlayer.ResetBuffAndDebuff(BUFF_DURATION.STROKE);
        t.secondPlayer.ResetBuffAndDebuff(BUFF_DURATION.STROKE);
        EventManager.Instance().fire(InGameEventType.HIDE_BALL, null);
        EventManager.Instance().fire(InGameEventType.HIDE_PLAYERS, null);
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        throw new NotImplementedException();
    }

    private bool CheckInDownTheLine(Vector2 tile, BounceGame t)
    {
        BaseGround ground = t.GetGround();
        return (Utils.CheckVector2ExistInList(tile, ground.player1DownTheLineTile)
            || Utils.CheckVector2ExistInList(tile, ground.player2DownTheLineTile));
    }

    private bool CheckAtTheNet(Vector2 tile, BounceGame t)
    {
        BaseGround ground = t.GetGround();
        return (Utils.CheckVector2ExistInList(tile, ground.player1AtTheNetTile)
            || Utils.CheckVector2ExistInList(tile, ground.player2AtTheNetTile));
    }
}

