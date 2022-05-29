using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IPlayer
{
    int GetTotalBuffValue(BUFF_TYPE buffType);
    int GetBuffValue(BUFF_DURATION duration, BUFF_TYPE buffType);
    void ResetBuffAndDebuff(BUFF_DURATION durationType, BUFF_TYPE buffType);
    void ResetBuffAndDebuff(BUFF_DURATION durationType);

    void ResetBuffAndDebuff();

    void SetBuff(BUFF_DURATION durationType, BUFF_TYPE buffType, int buffValue);
    void SetDebuff(BUFF_DURATION durationType, BUFF_TYPE debuffType, int debuffValue);

    Vector2 GetPosition();

    void SetPosition(int x, int y);

    Vector2 GetLastStrikePosition();

    void SetLastStrikePosition(int x, int y);

    int GetStrokeRemain();

    int GetStrokeUsed();
    void ResetStrokeRemain();

    void DecreaseStrokeRemain(int number);

    void SetTacticCardType(TacticCardType type);

    void IncreaseCurrentTacticCard(int number);
    void IncreaseTacticCard(TacticCardType type, int number);

    void ResetTacticCard();

    TacticCardType GetCurrentTacticCard();

    Dictionary<TacticCardType, int> GetTacticCardsData();
}

