using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class BaseScore
{
    protected Dictionary<IPlayer, int> gameScores = new Dictionary<IPlayer,int>();
    protected Dictionary<IPlayer, int> gamePoints = new Dictionary<IPlayer, int>();

    public void ResetScore()
    {
        foreach( var player in gameScores.Keys )
        {
            gameScores[player] = 0;
        }
    }

    public void ResetPoint()
    {
        foreach (var player in gamePoints.Keys)
        {
            gamePoints[player] = 0;
        }
    }

    public abstract void IncreasePoint(IPlayer player);
    public abstract void IncreaseScore(IPlayer player);
    public abstract void IncreaseScoreForWinner();

    public int GetScore(IPlayer player)
    {
        if (gameScores.ContainsKey(player))
            return gameScores[player];
        else return 0;
    }

    public int GetPoint(IPlayer player)
    {
        if (gamePoints.ContainsKey(player))
            return gamePoints[player];
        else return 0;
    }

    public void SetScore(IPlayer player, int score)
    {
            gameScores[player] = score;
    }

    public void SetPoint(IPlayer player, int point)
    {
            gamePoints[player] = point;
    }

    public void AddPlayer(IPlayer player)
    {
        if (!gameScores.ContainsKey(player))
            gameScores.Add(player, 0);
        if (!gamePoints.ContainsKey(player))
            gamePoints.Add(player, 0);
    }
    public void RemovePlayer(IPlayer player)
    {
        if (!gameScores.ContainsKey(player))
            gameScores.Remove(player);
        if (!gamePoints.ContainsKey(player))
            gamePoints.Remove(player);
    }

    public void StartNewMatch()
    {
        ResetScore();
        ResetPoint();
    }
}

