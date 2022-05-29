using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    class BaseBall: IBall
    {
        Vector2 position;
        public BaseBall()
        {
            position = new Vector2(0, 0);
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


        public void SetPosition(Vector2 position)
        {
            this.position.x = position.x;
            this.position.y = position.y;
        }
    }

