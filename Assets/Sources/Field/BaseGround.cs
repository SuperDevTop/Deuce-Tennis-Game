using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public abstract class BaseGround
{
    protected BaseTile[][] ground;
    protected ITileStrategy tileStrategy;
    public Vector2 player1ServeDownPos;
    public Vector2 player1ServeUpPos;
    public Vector2 player2ServeLeftPos;
    public Vector2 player2ServeDownPos;

    public List<Vector2> leftStartDownTiles;
    public List<Vector2> leftStartUpTiles;
    public List<Vector2> rightStartDownTiles;
    public List<Vector2> rightStartUpTiles;

    public List<Vector2> player1UpServableTiles;
    public List<Vector2> player2UpServableTiles;
    public List<Vector2> player1DownServableTiles;
    public List<Vector2> player2DownServableTiles;

    public List<Vector2> player1UpServeStrikableTiles;
    public List<Vector2> player2UpServeStrikableTiles;
    public List<Vector2> player1DownServeStrikableTiles;
    public List<Vector2> player2DownServeStrikableTiles;

    public List<Vector2> player1StrikableTile;
    public List<Vector2> player2StrikableTile;

    public List<Vector2> player1SelectableTile;
    public List<Vector2> player2SelectableTile;

    public List<Vector2> player1DownTheLineTile;
    public List<Vector2> player2DownTheLineTile;

    public List<Vector2> player1AtTheNetTile;
    public List<Vector2> player2AtTheNetTile;
    public BaseGround(ITileStrategy tileStrategy)
    {
        this.tileStrategy = tileStrategy;
        leftStartDownTiles = new List<Vector2>();
        leftStartUpTiles = new List<Vector2>();
        rightStartDownTiles = new List<Vector2>();
        rightStartUpTiles = new List<Vector2>();

        player1UpServableTiles = new List<Vector2>();
        player2UpServableTiles = new List<Vector2>();
        player1DownServableTiles = new List<Vector2>();
        player2DownServableTiles = new List<Vector2>();

        player1UpServeStrikableTiles = new List<Vector2>();
        player2UpServeStrikableTiles = new List<Vector2>();
        player1DownServeStrikableTiles = new List<Vector2>();
        player2DownServeStrikableTiles = new List<Vector2>();

        player1StrikableTile = new List<Vector2>();
        player2StrikableTile = new List<Vector2>();

        player1SelectableTile = new List<Vector2>();
        player2SelectableTile = new List<Vector2>();

        player1DownTheLineTile = new List<Vector2>();
        player2DownTheLineTile = new List<Vector2>();

        player1AtTheNetTile = new List<Vector2>();
        player2AtTheNetTile = new List<Vector2>();
        InitGround();
        SetTileProperties();
    }

    protected abstract void InitGround();
    protected void SetTileProperties()
    {
        if( tileStrategy != null )
        {
            tileStrategy.SetTileProperties(this);
        }
    }
    public BaseTile GetTile(int x, int y)
    {
        if (x < 0 || x > ground.Length)
            throw new Exception("out of bound!!!");
        var col = ground[x];
        if (y < 0 || y > col.Length)
            throw new Exception("out of bound!!!");
        return col[y];
    }

    public BaseTile[][] GetGround()
    {
        return ground;
    }

    public int GetGroundLength()
    {
        if (ground != null)
            return ground.Length;
        else return 0;
    }

    public int GetGroundWidth()
    {
        var width = 0;

        if( ground != null )
            foreach( var col in ground )
            {
                if(col != null)
                    return col.Length;
            }
        return width;
    }

    

}
