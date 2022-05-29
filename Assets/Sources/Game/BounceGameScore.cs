using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class BounceGameScore: BaseScore
    {
        public override void IncreasePoint(IPlayer player)
        {
            if( gamePoints.ContainsKey(player) )
            {
                gamePoints[player] = gamePoints[player] + 1 + player.GetTotalBuffValue(BUFF_TYPE.POINT);
            }
            else
            {
                gamePoints[player] = 1;
            }
        }

        public override void IncreaseScore(IPlayer player)
        {
            if (gameScores.ContainsKey(player))
            {
                gameScores[player] = gameScores[player] + 1 + player.GetTotalBuffValue(BUFF_TYPE.SCORE);
            }
            else
            {
                gameScores[player] = 1;
            }
        }

        public override void IncreaseScoreForWinner()
        {
            IPlayer winner = null;
            foreach( var player in gamePoints.Keys )
            {
                if (winner == null)
                    winner = player;
                else
                {
                    if( gamePoints[winner] < gamePoints[player] )
                    {
                        winner = player;
                    }
                }
            }
            if (winner != null)
                IncreaseScore(winner);
        }
    }

