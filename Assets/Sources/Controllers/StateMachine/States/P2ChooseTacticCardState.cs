using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class P2ChooseTacticCardState : State<BounceGame>
{
    private static P2ChooseTacticCardState instance;
    public static P2ChooseTacticCardState Instance()
    {
        if (instance == null)
            instance = new P2ChooseTacticCardState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter P2ChooseTacticCardState");
        EventManager.Instance().fire(InGameEventType.POPUP, "Player 2 Please Choose a tactic card");
        EventManager.Instance().fire(InGameEventType.OPEN_TACTIC_CARD, null);
        EventManager.Instance().fire(InGameEventType.SET_DATA_TACTIC_CARD, t.secondPlayer.GetTacticCardsData());

    }

    public override void Execute(BounceGame t)
    {
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit P2ChooseTacticCardState");
        EventManager.Instance().fire(InGameEventType.CLOSE_TACTIC_CARD, null);
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        switch (aEventType)
        {
            case InGameEventType.CHOOSE_TACTIC_CARD:
                TacticCardType tacticType = (TacticCardType)aEventData;
                Debug.Log("Choose tactic Card: " + tacticType);
                t.secondPlayer.SetTacticCardType(tacticType);
                //t.changeState(ServeState.Instance());
                t.changeState(ChooseServePositionState.Instance());
                break;
        }
    }
}

