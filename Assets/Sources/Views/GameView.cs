using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameView : MonoBehaviour, IEventListener
{
    public FieldView fieldView;
    public DiceRollView rollDiceView;
    BounceGame bounceGame;
	// Use this for initialization
	void Start ()
    {
        //string firstTimeSaved = PlayerPrefs.GetString("FirstTime");
        //if (string.IsNullOrEmpty(firstTimeSaved))
        //    PlayerPrefs.SetString("FirstTime", DateTime.Now.ToString());
        //Debug.Log(firstTimeSaved);
        //DateTime firstTime = DateTime.Parse(firstTimeSaved);
        //Debug.Log(firstTime);
        //DateTime maxTime = firstTime.AddMinutes(15);
        //DateTime maxTime = DateTime.Parse("2016-10-31");
        //Debug.Log(maxTime);
        //Debug.Log(DateTime.Now);
        //Debug.Log("Compare: " + maxTime.CompareTo(DateTime.Now));
        //if (maxTime.CompareTo(DateTime.Now) < 1)
        //{
        //    Application.Quit();
        //}

        RegisterEvent();
        bounceGame = new BounceGame();
        StartCoroutine(StartGame());
	}
    private void RegisterEvent()
    {
        EventManager.Instance().attach(InGameEventType.INPUT, this);
        EventManager.Instance().attach(InGameEventType.ROLL_DONE, this);
        EventManager.Instance().attach(InGameEventType.CHECK_STRIKABLE_DONE, this);
        EventManager.Instance().attach(InGameEventType.CHOOSE_STROKE_CARD, this);
        EventManager.Instance().attach(InGameEventType.DONT_CHOOSE_STROKE_CARD, this);
        EventManager.Instance().attach(InGameEventType.BONUS_CARD_GETTED, this);
        EventManager.Instance().attach(InGameEventType.CHOOSE_HOME_SUPPORT, this);
        EventManager.Instance().attach(InGameEventType.CHOOSE_TACTIC_CARD, this);
    }

    IEnumerator StartGame()
    {
        yield return 0;
        fieldView.Init(bounceGame.GetGround());
        yield return 0;
        bounceGame.StartGame();

    }
	
	// Update is called once per frame
	void Update () 
    {
        //NGUIDebug.Log("CCCCC");
        bounceGame.Update();
	}

    public void TestScore()
    {
        bounceGame.SetGameScore(bounceGame.firstPlayer, 3);
        bounceGame.SetGameScore(bounceGame.secondPlayer, 1);
        bounceGame.SetGamePoint(bounceGame.firstPlayer, 2);
        bounceGame.SetGamePoint(bounceGame.secondPlayer, 3);

        Dictionary<string, int> scoreEventData = new Dictionary<string, int>();
        scoreEventData.Add("player1Score", bounceGame.GetGameScore(bounceGame.firstPlayer));
        scoreEventData.Add("player2Score", bounceGame.GetGameScore(bounceGame.secondPlayer));
        scoreEventData.Add("player1Point", bounceGame.GetGamePoint(bounceGame.firstPlayer));
        scoreEventData.Add("player2Point", bounceGame.GetGamePoint(bounceGame.secondPlayer));
        EventManager.Instance().fire(InGameEventType.UPDATE_GAME_SCORE, scoreEventData);
    }

    public void handle(InGameEventType aEventType, object aEventData)
    {
        bounceGame.handle(aEventType, aEventData);
    }
}
