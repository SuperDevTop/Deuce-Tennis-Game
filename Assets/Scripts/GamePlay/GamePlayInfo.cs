using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GamePlay
{
    public class GamePlayInfo
    {
        private bool isStart;
        private bool isServe;
        private bool isReceive;
        private bool isWhereToServe;
        private bool isRoll;
        private bool isHitNet;
        private bool isRecoverMove;
        private bool isMoveToStrike;
        private bool isWhereToStrike;
        private bool isQus;

        public bool IsQus
        {
            get { return isQus; }
            set { isQus = value; }
        }

        public bool IsStart
        {
            get { return isStart; }
            set { isStart = value; }
        }

        public bool IsServe
        {
            get { return isServe; }
            set { isServe = value; }
        }

        public bool IsReceive
        {
            get { return isReceive; }
            set { isReceive = value; }
        }

        public bool IsWhereToServe
        {
            get { return isWhereToServe; }
            set { isWhereToServe = value; }
        }

        public bool IsRoll
        {
            get { return isRoll; }
            set { isRoll = value; }
        }

        public bool IsRecoveryMove
        {
            get { return isRecoverMove; }
            set { isRecoverMove = value; }
        }

        public bool IsHitNet
        {
            get { return isHitNet; }
            set { isHitNet = value; }
        }

        public bool IsMoveToStrike
        {
            get { return isMoveToStrike; }
            set { isMoveToStrike = value; }
        }

        public bool IsWhereToStrike
        {
            get { return isWhereToStrike; }
            set { isWhereToStrike = value; }
        }
    }
}
