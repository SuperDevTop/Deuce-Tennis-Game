using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class Utils
    {
        public static void SetTacticCardData(BounceGame t)
        {
            EventManager.Instance().fire(InGameEventType.SET_ACTIVE_TACTIC_CARD, t.currentPlayer.GetCurrentTacticCard());

            EventManager.Instance().fire(InGameEventType.SET_DATA_TACTIC_CARD, t.currentPlayer.GetTacticCardsData());
        }

        public static bool CheckVector2ExistInList(Vector2 target, List<Vector2> vectors)
        {
            //Debug.Log("Check: " + target.ToString());
            foreach (var vector2 in vectors)
            {
                //Debug.Log("vector2: " + vector2.ToString());
                if (target.x == vector2.x && target.y == vector2.y)
                {
                    //Debug.Log("YES~!");
                    return true;
                }
                //Debug.Log("NO!");
            }
            return false;
        }
    }

