using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class ChooseStrokeCardState : State<BounceGame>
{
    private static ChooseStrokeCardState instance;
    public static ChooseStrokeCardState Instance()
    {
        if (instance == null)
            instance = new ChooseStrokeCardState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter ChooseStrokeCardState");
        EventManager.Instance().fire(InGameEventType.POPUP, "Choose a stroke card to play");
        //TODO Check number of stroke card left
        if( t.currentPlayer.GetStrokeRemain() > 0 )
        {
            //if enough
            EventManager.Instance().fire(InGameEventType.TOGGLE_OPEN_STROKE_CARD, true);

        }
        else
        {
            //if not enough
            t.changeState(StrikeState.Instance());
        }

    }

    public override void Execute(BounceGame t)
    {
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit ChooseStrokeCardState");
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        switch( aEventType )
        {
            case InGameEventType.CHOOSE_STROKE_CARD:
                t.currentPlayer.DecreaseStrokeRemain(1);
                EventManager.Instance().fire(InGameEventType.TOGGLE_OPEN_STROKE_CARD, false);
                StrokeCardType strokeType = (StrokeCardType)aEventData;
                Debug.Log("ChooseStrokeCardState: " + strokeType);
                switch( strokeType )
                {
                    case StrokeCardType.DROP_SHOT:
                        Debug.Log("ChooseStrokeCardState: AAAA");
                        t.currentPlayer.SetBuff(BUFF_DURATION.STROKE, BUFF_TYPE.FIXED_BOUNCE_DISTANCE, 1);
                        t.OtherPlayer.SetBuff(BUFF_DURATION.STROKE, BUFF_TYPE.MOVEMENT_SPD, 3);
                        Debug.Log("ChooseStrokeCardState: BBBB");
                        t.changeState(StrikeState.Instance());
                        break;
                    case StrokeCardType.LOB_SHOT:
                        t.OtherPlayer.SetBuff(BUFF_DURATION.STROKE, BUFF_TYPE.MOVEMENT_SPD, 3);
                        t.OtherPlayer.SetDebuff(BUFF_DURATION.STROKE, BUFF_TYPE.NO_HIT_BEFORE_FIRST_BOUNCE, 1);
                        t.changeState(StrikeState.Instance());
                        break;
                    case StrokeCardType.POWER:
                        t.currentPlayer.SetDebuff(BUFF_DURATION.STROKE, BUFF_TYPE.RECOVER_SPD, 1);
                        t.OtherPlayer.SetDebuff(BUFF_DURATION.STROKE, BUFF_TYPE.MOVEMENT_SPD, 1);
                        t.changeState(StrikeState.Instance());
                        break;
                    case StrokeCardType.SLICE:
                        t.currentPlayer.SetBuff(BUFF_DURATION.STROKE, BUFF_TYPE.RECOVER_SPD, 1);
                        t.currentPlayer.SetDebuff(BUFF_DURATION.STROKE, BUFF_TYPE.ADDITION_SECOND_BOUNCE, 1);
                        t.changeState(StrikeState.Instance());
                        break;
                    case StrokeCardType.TOP_SPIN:
                        t.currentPlayer.SetBuff(BUFF_DURATION.STROKE, BUFF_TYPE.NO_ZERO_NET_HITTING, 1);
                        t.currentPlayer.SetBuff(BUFF_DURATION.STROKE, BUFF_TYPE.ADDITION_SECOND_BOUNCE, 1);
                        t.changeState(StrikeState.Instance());
                        break;
                }
                break;

            case InGameEventType.DONT_CHOOSE_STROKE_CARD:
                EventManager.Instance().fire(InGameEventType.TOGGLE_OPEN_STROKE_CARD, false);
                t.changeState(StrikeState.Instance());
                break;
        }
    }
}

