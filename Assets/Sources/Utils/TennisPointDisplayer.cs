using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class TennisPointDisplayer: IPointDisplayer
{
    public string GetFirstFourPointsDisplayValue(int score)
    {
        switch( score )
        {
            case 0:
                return "0";
            case 1: 
                return "15";
            case 2:
                return "30";
            case 3:
                return "40";
            default:
                return "0";
        }
    }

    public string[] GetPointDisplayValue(int score1, int score2)
    {
        string[] returnValue = new string[2];
        //Debug.Log("Tennis Poin Displayer: " + score1 + " _ " + score2);
        returnValue[0] = "40";
        returnValue[1] = "40";
        if( score1 <= 3 )
            returnValue[0] = GetFirstFourPointsDisplayValue(score1);
        
        if( score2 <= 3 )
            returnValue[1] = GetFirstFourPointsDisplayValue(score2);
        if (score1 > 3 || score2 > 3)
        {
            if (score1 > score2)
            {
                returnValue[0] = "AD";
            }
            else if (score1 < score2)
            {
                returnValue[1] = "AD";
            }
        }
        return returnValue;
    }
}

