using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class BasePlayer: IPlayer 
{
    Dictionary<BUFF_DURATION, Dictionary<BUFF_TYPE, int>> buffMap;
    Dictionary<BUFF_DURATION, Dictionary<BUFF_TYPE, int>> debuffMap;
    TacticCardType currentTactic;
    Dictionary<TacticCardType, int> tacticMap;
    int StrokeRemain;
    int StrokeUse;
    Vector2 position;
    Vector2 lastStrikePosition;
    public BasePlayer()
    {
        position = new Vector2(0, 0);
        lastStrikePosition = new Vector2(0, 0);
        buffMap = new Dictionary<BUFF_DURATION, Dictionary<BUFF_TYPE, int>>();
        debuffMap = new Dictionary<BUFF_DURATION, Dictionary<BUFF_TYPE, int>>();
        initBuffMap(buffMap);
        initBuffMap(debuffMap);
        StrokeRemain = GameConfig.NUMBER_OF_STROKE_PER_POINT;
        initTacticMap();

    }

    private void initBuffMap(Dictionary<BUFF_DURATION, Dictionary<BUFF_TYPE, int>> buffMap)
    {
        foreach( BUFF_DURATION duration in Enum.GetValues(typeof(BUFF_DURATION)))
        {
            Dictionary<BUFF_TYPE, int> buffValueMap = new Dictionary<BUFF_TYPE, int>();
            foreach (BUFF_TYPE type in Enum.GetValues(typeof(BUFF_TYPE)))
            {
                buffValueMap.Add(type, 0);
            }
            buffMap.Add(duration, buffValueMap);
        }
    }

    private void initTacticMap()
    {
        tacticMap = new Dictionary<TacticCardType, int>();
        foreach (TacticCardType type in Enum.GetValues(typeof(TacticCardType)))
        {
            tacticMap.Add(type, 0);
        }
    }
    public int GetTotalBuffValue(BUFF_TYPE buffType)
    {
        var value = 0;
        foreach (BUFF_DURATION duration in Enum.GetValues(typeof(BUFF_DURATION)))
        {
            value += buffMap[duration][buffType];
            value -= debuffMap[duration][buffType];
        }
        return value;
    }

    public int GetBuffValue(BUFF_DURATION duration, BUFF_TYPE type)
    {
        var buffs = buffMap[duration];
        var value = buffs[type];
        return value;
    }

    public void ResetBuffAndDebuff(BUFF_DURATION durationType, BUFF_TYPE buffType)
    {
        buffMap[durationType][buffType] = 0;
        debuffMap[durationType][buffType] = 0;
    }

    public void ResetBuffAndDebuff(BUFF_DURATION durationType)
    {
        foreach (BUFF_TYPE type in Enum.GetValues(typeof(BUFF_TYPE)))
        {
            buffMap[durationType][type] = 0;
        }
        foreach (BUFF_TYPE type in Enum.GetValues(typeof(BUFF_TYPE)))
        {
            debuffMap[durationType][type] = 0;
        }
    }

    public void ResetBuffAndDebuff()
    {
        foreach (BUFF_DURATION duration in Enum.GetValues(typeof(BUFF_DURATION)))
        {
            foreach (BUFF_TYPE type in Enum.GetValues(typeof(BUFF_TYPE)))
            {
                buffMap[duration][type] = 0;
                debuffMap[duration][type] = 0;
            }
        }
    }

    public void SetBuff(BUFF_DURATION durationType, BUFF_TYPE buffType, int buffValue)
    {
        buffMap[durationType][buffType] = buffValue;
    }

    public void SetDebuff(BUFF_DURATION durationType, BUFF_TYPE debuffType, int debuffValue)
    {
        debuffMap[durationType][debuffType] = debuffValue;
    }


    public Vector2 GetPosition()
    {
        return position;
    }

    public void SetPosition(int x, int y)
    {
        position.x = x;
        position.y = y;
    }

    public Vector2 GetLastStrikePosition()
    {
        return lastStrikePosition;
    }

    public void SetLastStrikePosition(int x, int y)
    {
        lastStrikePosition.x = x;
        lastStrikePosition.y = y;
    }


    public int GetStrokeRemain()
    {
        if (GetTotalBuffValue(BUFF_TYPE.UNLIMIT_STROKE) > 0)
            StrokeRemain += 999;
        StrokeRemain += GetTotalBuffValue(BUFF_TYPE.STROKE_CARD);
        return StrokeRemain;
    }

    public int GetStrokeUsed()
    {
        return StrokeUse;
    }

    public void ResetStrokeRemain()
    {
        StrokeRemain = GameConfig.NUMBER_OF_STROKE_PER_POINT;
        StrokeUse = 0;
    }


    public void DecreaseStrokeRemain(int number)
    {
        StrokeRemain -= number;
        StrokeUse += number;
        if (StrokeRemain < 0)
            StrokeRemain = 0;
    }


    public void SetTacticCardType(TacticCardType type)
    {
        currentTactic = type;
    }

    public void IncreaseCurrentTacticCard(int number)
    {
        tacticMap[currentTactic] += number;
    }

    public void IncreaseTacticCard(TacticCardType type, int number)
    {
        tacticMap[type] += number;
    }

    public void ResetTacticCard()
    {
        foreach (TacticCardType type in Enum.GetValues(typeof(TacticCardType)))
        {
            tacticMap[type] = 0;
        }
    }

    public TacticCardType GetCurrentTacticCard()
    {
        return currentTactic;
    }


    public Dictionary<TacticCardType, int> GetTacticCardsData()
    {
        return tacticMap;
    }
}

