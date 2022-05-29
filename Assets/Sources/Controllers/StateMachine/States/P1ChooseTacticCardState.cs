using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class P1ChooseTacticCardState : State<BounceGame>
{
    private static P1ChooseTacticCardState instance;
    public static P1ChooseTacticCardState Instance()
    {
        if (instance == null)
            instance = new P1ChooseTacticCardState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {
        Debug.Log("OK Enter P1ChooseTacticCard");
        EventManager.Instance().fire(InGameEventType.POPUP, "Player 1 Please Choose a tactic card");
        EventManager.Instance().fire(InGameEventType.OPEN_TACTIC_CARD, null);
        EventManager.Instance().fire(InGameEventType.REMOVE_ACTIVE_TACTIC_CARD, null);
        EventManager.Instance().fire(InGameEventType.SET_DATA_TACTIC_CARD, t.firstPlayer.GetTacticCardsData());

    }

    public override void Execute(BounceGame t)
    {
    }

    public override void Exit(BounceGame t)
    {
        Debug.Log("OK Exit P1ChooseTacticCard");

    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        switch (aEventType)
        {
            case InGameEventType.CHOOSE_TACTIC_CARD:
                TacticCardType tacticType = (TacticCardType)aEventData;
                Debug.Log("Choose tactic Card: " + tacticType);
                t.firstPlayer.SetTacticCardType(tacticType);
                t.changeState(P2ChooseTacticCardState.Instance());
                break;
        }
    }
}

