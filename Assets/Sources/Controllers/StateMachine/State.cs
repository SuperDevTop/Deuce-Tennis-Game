using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class State<T>
{
    public abstract void Enter(T t);
    public abstract void Execute(T t);
    public abstract void Exit(T t);
    public abstract void HandleEvent(InGameEventType aEventType, object aEventData, T t);

}
