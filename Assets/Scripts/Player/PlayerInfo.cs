using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{   
    public class PlayerInfo
    {
        private bool isMyServe;
        private bool isMyTurn;
        private bool isQusUp;
        private int servePositionIndex;
        private int level;
        private int score;
        private int bonusMove;
        private int strokeCardCount;

        public bool IsQusUp
        {
            get { return isQusUp; }
            set { isQusUp = value; }
        }

        public bool IsMyServe
        {
            get { return isMyServe; }
            set { isMyServe = value; }
        }

        public bool IsMyTurn
        {
            get { return isMyTurn; }
            set { isMyTurn = value; }
        }

        public int ServePositionIndex
        {
            get { return servePositionIndex; }
            set { servePositionIndex = value; }
        }

        public int StrokeCardCount
        {
            get { return strokeCardCount; }
            set { strokeCardCount = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public int BonusMove
        {
            get { return bonusMove; }
            set { bonusMove = value; }
        }
    }
}
