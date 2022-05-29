using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class BonusCardState : State<BounceGame>
{
    private static BonusCardState instance;
    public static BonusCardState Instance()
    {
        if (instance == null)
            instance = new BonusCardState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter BonusCardState");
        EventManager.Instance().fire(InGameEventType.POPUP, "You got a bonus Card!!!");

        EventManager.Instance().fire(InGameEventType.OPEN_BONUS_CARD, null);


    }

    public override void Execute(BounceGame t)
    {
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit BonusCardState");
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        var winner = t.gameWinner;
        Dictionary<string, int> scoreEventData = new Dictionary<string, int>();
        bool gonnaChangeState = true;
        switch( aEventType )
        {
            case InGameEventType.BONUS_CARD_GETTED:
                var type = (BonusCardType)aEventData;
                Debug.Log("Bonus Card State: " + type);
                switch(type)
                {
                    case BonusCardType.ATHLETIC:
                        winner.SetBuff(BUFF_DURATION.ONE_POINT, BUFF_TYPE.MOVEMENT_SPD, 1);
                        break;
                    case BonusCardType.CHANGE_SIDE:
                        var p1Score = t.gameScore.GetScore(t.firstPlayer);
                        var p2Score = t.gameScore.GetScore(t.secondPlayer);
                        if( p1Score == p2Score )
                        {
                            t.gameScore.IncreaseScore(winner);
                            EventManager.Instance().fire(InGameEventType.POPUP, "Winner wins an extra game!");
                            scoreEventData["player1Score"] =  t.GetGameScore(t.firstPlayer);
                            scoreEventData["player2Score"]= t.GetGameScore(t.secondPlayer);
                            scoreEventData["player1Point"]= t.GetGamePoint(t.firstPlayer);
                            scoreEventData["player2Point"]= t.GetGamePoint(t.secondPlayer);
                            EventManager.Instance().fire(InGameEventType.UPDATE_GAME_SCORE, scoreEventData);
                        }
                        else
                        {
                            EventManager.Instance().fire(InGameEventType.POPUP, "ChangeSide!");
                        }
                        break;
                    case BonusCardType.COACH_ADVICE:
                        gonnaChangeState = false;
                        EventManager.Instance().fire(InGameEventType.POPUP, "Choose a tactic card for Coach Advice!!");
                        EventManager.Instance().fire(InGameEventType.OPEN_TACTIC_CARD, null);
                        EventManager.Instance().fire(InGameEventType.REMOVE_ACTIVE_TACTIC_CARD, null);
                        //TODO later
                        break;
                    case BonusCardType.ENTERTAINMENT:
                        winner.SetBuff(BUFF_DURATION.WHOLE_GAME, BUFF_TYPE.POINT, 1);
                        break;
                    case BonusCardType.FAKE_SHOT:
                        if (winner == t.firstPlayer)
                        {
                            t.secondPlayer.SetDebuff(BUFF_DURATION.ONE_POINT, BUFF_TYPE.MOVEMENT_SPD, 1);
                        }
                        else
                        {
                            t.firstPlayer.SetDebuff(BUFF_DURATION.ONE_POINT, BUFF_TYPE.MOVEMENT_SPD, 1);
                        }
                        break;
                    case BonusCardType.GRAND_SLAM:
                        winner.SetBuff(BUFF_DURATION.WHOLE_GAME, BUFF_TYPE.SCORE, 1);
                        break;
                    case BonusCardType.HAVE_FUN:
                        winner.SetBuff(BUFF_DURATION.WHOLE_GAME, BUFF_TYPE.UNLIMIT_STROKE, 1);
                        break;
                    case BonusCardType.HOME_SUPPORT:
                        //TODO later
                        gonnaChangeState = false;
                        EventManager.Instance().fire(InGameEventType.OPEN_HOME_SUPPORT, null);
                        break;
                    case BonusCardType.JUMP_SHOT:
                        winner.SetDebuff(BUFF_DURATION.WHOLE_GAME, BUFF_TYPE.MOVEMENT_SPD, 1);
                        t.gameScore.SetPoint(winner, 2);
                        scoreEventData["player1Score"] = t.GetGameScore(t.firstPlayer);
                        scoreEventData["player2Score"] = t.GetGameScore(t.secondPlayer);
                        scoreEventData["player1Point"] = t.GetGamePoint(t.firstPlayer);
                        scoreEventData["player2Point"] = t.GetGamePoint(t.secondPlayer);
                        EventManager.Instance().fire(InGameEventType.UPDATE_GAME_SCORE, scoreEventData);
                        break;
                    case BonusCardType.KEEP_GOING:
                        EventManager.Instance().fire(InGameEventType.POPUP, "KEEP GOING!");
                        break;
                    case BonusCardType.NEW_RACKET:
                        winner.SetBuff(BUFF_DURATION.WHOLE_GAME, BUFF_TYPE.STROKE_CARD, 1);
                        break;
                    case BonusCardType.PRECISION:
                        winner.SetBuff(BUFF_DURATION.WHOLE_GAME, BUFF_TYPE.DICE, 1);
                        break;
                    case BonusCardType.TWEENER:
                        t.gameScore.SetPoint(winner, 1);
                        scoreEventData["player1Score"] = t.GetGameScore(t.firstPlayer);
                        scoreEventData["player2Score"] = t.GetGameScore(t.secondPlayer);
                        scoreEventData["player1Point"] = t.GetGamePoint(t.firstPlayer);
                        scoreEventData["player2Point"] = t.GetGamePoint(t.secondPlayer);
                        EventManager.Instance().fire(InGameEventType.UPDATE_GAME_SCORE, scoreEventData);
                        break;
                }
                break;
            case InGameEventType.CHOOSE_HOME_SUPPORT:
                var point = (int)aEventData;
                t.gameScore.SetPoint(winner, point);
                scoreEventData["player1Score"] = t.GetGameScore(t.firstPlayer);
                scoreEventData["player2Score"] = t.GetGameScore(t.secondPlayer);
                scoreEventData["player1Point"] = t.GetGamePoint(t.firstPlayer);
                scoreEventData["player2Point"] = t.GetGamePoint(t.secondPlayer);
                EventManager.Instance().fire(InGameEventType.UPDATE_GAME_SCORE, scoreEventData);
                break;
            case InGameEventType.CHOOSE_TACTIC_CARD:
                
                TacticCardType tacticType = (TacticCardType)aEventData;
                winner.IncreaseTacticCard(tacticType, 2);
                break;
        }
        if (gonnaChangeState)
            t.changeState(P1ChooseTacticCardState.Instance());

    }
}

