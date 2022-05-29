using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StateMachine<T>: IEventListener
{
    private T m_owner;
    private State<T> m_CurrentState;
    private State<T> m_PreviousState;
    private State<T> m_GlobalState;

    public StateMachine(T owner)
    {
        m_owner = owner;
    }

    public void SetCurrentState(State<T> state)
    {
        m_CurrentState = state;
    }

    public void SetGlobalState(State<T> state)
    {
        m_GlobalState = state;

    }

    public void SetPreviousState(State<T> state)
    {
        m_PreviousState = state;
    }

    public void Update()
    {
        if (m_GlobalState != null)
            m_GlobalState.Execute(m_owner);
        if (m_CurrentState != null)
            m_CurrentState.Execute(m_owner);
    }

    public void ChangeState(State<T> newState)
    {
        m_PreviousState = m_CurrentState;
        m_CurrentState.Exit(m_owner);
        m_CurrentState = newState;
        m_CurrentState.Enter(m_owner);
    }

    public void RevertToPreviousState()
    {
        ChangeState(m_PreviousState);
    }

    public State<T> CurrentState()
    {
        return m_CurrentState;
    }

    public State<T> GlobalState()
    {
        return m_GlobalState;
    }

    public State<T> PreviousState()
    {
        return m_PreviousState;
    }

    public Boolean isInState(State<T> state)
    {
        if (state.Equals(m_CurrentState))
            return true;
        else return false;
    }

    public void handle(InGameEventType aEventType, object aEventData)
    {
        m_CurrentState.HandleEvent(aEventType, aEventData, m_owner);
    }
}


