using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum InGameEventType
{
    //Start State
    UPDATE_GAME_SCORE
    //Roll dice, check collision
    , RESET_COLLISION
    //Choose Serve Position
    , ACTIVE_START_POSITION, DEACTIVE_START_POSITION
    //Serve State
    , SET_PLAYERS_POSITION, HIDE_PLAYERS, ACTIVE_SERVABLE_FIELD, DEACTIVE_SERVABLE_FIELD, HIDE_BALL, DEACTIVE_STRIKABLE_TILES
    , ACTIVE_STRIKABLE_TILES
    //Input
    , INPUT
    //View Update
    , SET_BALL
    //Recovery State
    , ACTIVE_MOVABLE_FIELD, DEACTIVE_MOVABLE_FIELD
    , ROLL_DICE, ACTIVE_ROLLABLE_TO_TILES, ROLL_DONE, DEACTIVE_ROLLABLE_TO_TILES
    //Check Strikable
    , CHECK_STRIKABLE, CHECK_STRIKABLE_DONE
    //Pop up
    , POPUP
    //Stroke Card
    , CHOOSE_STROKE_CARD, DONT_CHOOSE_STROKE_CARD, TOGGLE_OPEN_STROKE_CARD
    //Bonus Card
    , OPEN_BONUS_CARD, BONUS_CARD_GETTED
    , OPEN_HOME_SUPPORT, CHOOSE_HOME_SUPPORT
    //Tactic Card
    , OPEN_TACTIC_CARD, CLOSE_TACTIC_CARD, CHOOSE_TACTIC_CARD, SET_ACTIVE_TACTIC_CARD, SET_DATA_TACTIC_CARD, REMOVE_ACTIVE_TACTIC_CARD
    //Move
    , DEACTIVE_COLLISION_BOX, ACTIVE_COLLISION_BOX
}
public interface IEventListener
{
    void handle(InGameEventType aEventType, object aEventData);
}
public class EventManager 
{
	private static EventManager mInstance;


    private Dictionary<InGameEventType, List<IEventListener>> mListeners;
	
	private EventManager()
	{
        mListeners = new Dictionary<InGameEventType, List<IEventListener>>();
	}
	
	public static EventManager Instance()
	{
		if(mInstance == null) mInstance = new EventManager();
		return mInstance;
	}

    public void attach(InGameEventType aEventType, IEventListener aListener)
	{
        if (!mListeners.ContainsKey(aEventType))
        {
            List<IEventListener> listeners = new List<IEventListener>();
            mListeners.Add(aEventType, listeners);
            listeners.Add(aListener);
        }
        else
        {
            var listeners = mListeners[aEventType];
            if(!listeners.Contains(aListener))
            {
                listeners.Add(aListener);
            }
        }	
	}

    public void detach(InGameEventType aEventType, IEventListener aListener)
	{
        if (mListeners.ContainsKey(aEventType))
        {
            List<IEventListener> listeners = mListeners[aEventType];
            listeners.Remove(aListener);
            if (listeners.Count == 0) mListeners.Remove(aEventType);
        }
	}

    public void fire(InGameEventType aEventType, object aEventData)
	{
        //Debug.Log("Fire Event: " + aEventType.ToString());
        if(mListeners.ContainsKey(aEventType))
        {
            List<IEventListener> listeners = mListeners[aEventType];
		    foreach(IEventListener listener in listeners)
		    {
			    listener.handle(aEventType, aEventData);
		    }
        }
	}
	
	public void clear(){
		mListeners.Clear();
	}

}
