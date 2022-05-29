using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public enum TileType
{
    OUT_BOUND, IN_BOUND_FAR_POST, IN_BOUND_NEAR_POST, IN_BOUND_NO_STRIKE
}
public class BaseTile: BaseObject
{
    protected TileType type;
    protected int Spd;
    public TileType Type
    {
        get { return type; }
        set { type = value; }
    }

    public int SPD
    {
        get { return Spd; }
        set { Spd = value; }
    }
}
