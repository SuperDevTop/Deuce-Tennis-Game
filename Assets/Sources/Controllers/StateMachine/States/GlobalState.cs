using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class GlobalState: State<BounceGame>
{
    private static GlobalState instance;
    public static GlobalState Instance()
    {
        if (instance == null)
            instance = new GlobalState();
        return instance;
    }
    public override void Enter(BounceGame t)
    {

    }

    public override void Execute(BounceGame t)
    {
    }

    public override void Exit(BounceGame t)
    {
    }

    public override void HandleEvent(InGameEventType aEventType, object aEventData, BounceGame t)
    {
        throw new NotImplementedException();
    }
}

