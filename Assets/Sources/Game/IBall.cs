using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    public interface IBall
    {
        Vector2 GetPosition();
        void SetPosition(int x, int y);

        void SetPosition(Vector2 position);
    }

