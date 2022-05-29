using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class NullState : State<BounceGame>
{
    private static NullState instance;
    public static NullState Instance()
    {
        if (instance == null)
            instance = new NullState();
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

