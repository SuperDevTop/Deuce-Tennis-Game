using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public interface IGame
{
    void StartGame();
    void EndGame();

    int GetGameScore(IPlayer player);

    int GetGamePoint(IPlayer player);

    void SetGameScore(IPlayer player, int score);
    void SetGamePoint(IPlayer player, int point);

    BaseGround GetGround();


    void SetFirstBouncePosition(Vector2 pos);
    void SetSecondBouncePosition(Vector2 pos);
}

