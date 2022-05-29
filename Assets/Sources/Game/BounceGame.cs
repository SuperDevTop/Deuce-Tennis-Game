using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BounceGame: BaseObject, IGame, IEventListener
{
    protected State<BounceGame> currentState;
    protected State<BounceGame> globalState;
    protected State<BounceGame> previousState;

    protected StateMachine<BounceGame> stateMachine;

    public IPlayer OtherPlayer
    {
        get
        {
            if (currentPlayer == firstPlayer)
                return secondPlayer;
            else return firstPlayer;
        }
    }

    public IPlayer ReceivePlayer
    {
        get
        {
            if (currentServer == firstPlayer)
                return secondPlayer;
            else return firstPlayer;
        }
    }

    public IPlayer firstPlayer;
    public IPlayer secondPlayer;
    public IPlayer currentPlayer;
    public IPlayer currentServer;
    public IPlayer pointWinner;
    public IPlayer gameWinner;
    public IPlayer endGameWinner;
    public IBall firstBounce;
    public IBall secondBounce;

    public BaseScore gameScore;

    private BaseGround ground;

    public BounceGame()
    {
        firstPlayer = new BasePlayer();
        secondPlayer = new BasePlayer();
        currentPlayer = firstPlayer;
        currentServer = firstPlayer;
        firstBounce = new BaseBall();
        secondBounce = new BaseBall();
        gameScore = new BounceGameScore();
        ground = GroundFactory.Instance.CreateStandardGround();
        FakeData();
        stateMachine = new StateMachine<BounceGame>(this);
        stateMachine.SetCurrentState(NullState.Instance());
    }
    private void FakeData()
    {
        //gameScore.SetScore(firstPlayer, 5);
        //gameScore.SetScore(secondPlayer, 1);
        //gameScore.SetPoint(firstPlayer, 0);
        //gameScore.SetPoint(secondPlayer, 3);
        //firstPlayer.IncreaseTacticCard(TacticCardType.TO_THE_NET, 4);
        //firstPlayer.IncreaseTacticCard(TacticCardType.ON_THE_OFFENSIVE, 4);
        //firstPlayer.IncreaseTacticCard(TacticCardType.SMART_GAME, 4);
    }
    public void Update()
    {
        stateMachine.Update();
    }

    public void changeState( State<BounceGame> state )
	{
		stateMachine.ChangeState(state);
	}

    public StateMachine<BounceGame> GetFSM()
	{
		return stateMachine;
	}


    public void StartGame()
    {
        changeState(StartGameState.Instance());
    }

    public void EndGame()
    {
        throw new NotImplementedException();
    }

    public int GetGameScore(IPlayer player)
    {
        return gameScore.GetScore(player);
    }

    public int GetGamePoint(IPlayer player)
    {
        return gameScore.GetPoint(player);
    }

    public void SetGameScore(IPlayer player, int score)
    {
        gameScore.SetScore(player, score);
    }

    public void SetGamePoint(IPlayer player, int point)
    {
        gameScore.SetPoint(player, point);
    }


    public BaseGround GetGround()
    {
        return ground;
    }


    //public void HandleInput(BaseObject input)
    //{
    //    stateMachine.HandleInput(input);
    //}


    public void SetFirstBouncePosition(Vector2 pos)
    {
        firstBounce.SetPosition(pos);
    }

    public void SetSecondBouncePosition(Vector2 pos)
    {
        secondBounce.SetPosition(pos);
    }

    public void handle(InGameEventType aEventType, object aEventData)
    {
        stateMachine.handle(aEventType, aEventData);
    }

    public bool IsGameOver()
    {
        return false;
    }

    public bool IsMatchOver()
    {
        var firstPoint = gameScore.GetPoint(firstPlayer);
        var secondPoint = gameScore.GetPoint(secondPlayer);
        if( Math.Abs(  firstPoint - secondPoint ) >= 2)
        {
            if (firstPoint >= 4 || secondPoint >= 4)
            {
                if (firstPoint > secondPoint)
                    gameWinner = firstPlayer;
                else
                    gameWinner = secondPlayer;
                return true;
            }
        }
        return false;
    }

    public bool isGameEnd()
    {
        var firstScore = gameScore.GetScore(firstPlayer);
        var secondScore = gameScore.GetScore(secondPlayer);
        if (Math.Abs(firstScore - secondScore) >= 2)
        {
            if (firstScore >= 6 || secondScore >= 6)
            {
                if (firstScore > secondScore)
                    endGameWinner = firstPlayer;
                else
                    endGameWinner = secondPlayer;
                return true;
            }
        }
        return false;
    }

}

