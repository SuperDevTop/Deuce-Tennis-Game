using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class StandardGroundTileStrategy: ITileStrategy
{
    List<Vector2> LeftFieldSpd;
    List<Vector2> RightFieldSpd;
    private int length;
    private int width;
    private int middleX;
    private int middleY;
    private int fieldLength;
    private int fieldWidth;
    public void SetTileProperties(BaseGround ground)
    {
        InitData();
        GetGroundWidthHeight(ground.GetGround());
        SetTilesType(ground.GetGround());
        SetTilesMovementSpd(ground.GetGround());
        SetStartPosition(ground);
        SetServableTile(ground);
        SetServeStrikableTile(ground);
        SetStrikableTile(ground);
        SetDownTheLineTile(ground);
        SetAtTheNetTile(ground);
    }

    private void InitData()
    {
        length = 0;
        width = 0;
        middleX = 0;
        middleY = 0;
        fieldLength = 18;
        fieldWidth = 6;

        //using a Vector2 to store data of spd zone, x is the number of column with the spd stored in y
        //example: Vector2(3,2) means 3 column will have spd of 2

        //LeftField store spd data from middleX to the left
        LeftFieldSpd = new List<Vector2>();
        LeftFieldSpd.Add(new Vector2(3, 2));
        LeftFieldSpd.Add(new Vector2(3, 3));
        LeftFieldSpd.Add(new Vector2(3, 4));
        LeftFieldSpd.Add(new Vector2(100, 5));

        //LeftField store spd data from middleX + 1 to the right
        RightFieldSpd = new List<Vector2>();
        RightFieldSpd.Add(new Vector2(3, 2));
        RightFieldSpd.Add(new Vector2(3, 3));
        RightFieldSpd.Add(new Vector2(3, 4));
        RightFieldSpd.Add(new Vector2(100, 5));

    }


    private void GetGroundWidthHeight(BaseTile[][] ground)
    {
        length = ground.Length;
        if (length == 0)
            throw new Exception("Not Supported kind of Ground. Width = 0");
        width = ground[0].Length;
        middleX = length / 2 - 1;
        middleY = width / 2 - 1;
        if (length < fieldLength)
            fieldLength = length;
        if (width < fieldWidth)
            fieldWidth = width;
    }
    private void SetTilesType(BaseTile[][] ground)
    {
        SetAllTilesToDefaultType(ground);       


        for (int i = 0; i < fieldLength / 2; i++)
        {
            var leftCol = ground[middleX - i];
            for (int j = 0; j < leftCol.Length; j++)
            {
                if (middleY - j < fieldWidth / 2 && j - middleY <= fieldWidth / 2)
                {
                    if (i <= 1)
                        leftCol[j].Type = TileType.IN_BOUND_NO_STRIKE;
                    else if (i <= 4)
                        leftCol[j].Type = TileType.IN_BOUND_NEAR_POST;
                    else
                        leftCol[j].Type = TileType.IN_BOUND_FAR_POST;
                }

            }
            var rightCol = ground[middleX + i + 1];
            for (int j = 0; j < rightCol.Length; j++)
            {
                if (middleY - j < fieldWidth / 2 && j - middleY <= fieldWidth / 2)
                {
                    if (i <= 1)
                        rightCol[j].Type = TileType.IN_BOUND_NO_STRIKE;
                    else if (i <= 4)
                        rightCol[j].Type = TileType.IN_BOUND_NEAR_POST;
                    else
                        rightCol[j].Type = TileType.IN_BOUND_FAR_POST;
                }
            }
        }  
    }

    private void SetAllTilesToDefaultType(BaseTile[][] ground)
    {
        foreach (var col in ground)
        {
            foreach (var tile in col)
            {
                tile.Type = TileType.OUT_BOUND;
            }
        }
    }

    private void SetTilesMovementSpd(BaseTile[][] ground)
    {
        SetLeftFieldMovementSpd(ground);
        SetRightFieldMovementSpd(ground);
    }

    private void SetLeftFieldMovementSpd(BaseTile[][] ground)
    {
        int counter = 0;
        for (int i = 0; i < LeftFieldSpd.Count; i++ )
        {
            var spdData = LeftFieldSpd[i];
            int numberOfColumn = (int)spdData.x;
            int spd = (int)spdData.y;
            for( int j = 0; j < numberOfColumn; j++ )
            {
                var col = ground[middleX - counter];
                foreach( var tile in col)
                {
                    tile.SPD = spd;
                }
                counter++;
                if (middleX - counter < 0)
                    return;
            }
        }
    }

    private void SetRightFieldMovementSpd(BaseTile[][] ground)
    {
        int counter = 0;
        for (int i = 0; i < RightFieldSpd.Count; i++)
        {
            var spdData = RightFieldSpd[i];
            int numberOfColumn = (int)spdData.x;
            int spd = (int)spdData.y;
            for (int j = 0; j < numberOfColumn; j++)
            {
                var col = ground[middleX + 1 + counter];
                foreach (var tile in col)
                {
                    tile.SPD = spd;
                }
                counter++;
                if (middleX + 1 + counter >= length)
                    return;
            }
        }
    }

    private void SetStartPosition(BaseGround ground)
    {
        ground.player1ServeUpPos = new Vector2(middleX - fieldLength/2, middleY + fieldWidth / 2);
        ground.player2ServeDownPos = new Vector2(middleX + fieldLength / 2 + 1, middleY - fieldWidth / 2 + 1);
        ground.player1ServeDownPos = new Vector2(middleX - fieldLength/2, middleY - fieldWidth/2 + 1);
        ground.player2ServeLeftPos = new Vector2(middleX + fieldLength / 2 + 1, middleY + fieldWidth / 2);

        for (int i = 0; i < fieldWidth / 2; i++)
        {
            var downField = middleY - i;
            var upField = middleY + 1 + i;
            ground.leftStartUpTiles.Add(new Vector2(middleX - fieldLength / 2, upField));
            ground.leftStartDownTiles.Add(new Vector2(middleX - fieldLength / 2, downField));
            ground.rightStartUpTiles.Add(new Vector2(middleX + fieldLength / 2 + 1, upField));
            ground.rightStartDownTiles.Add(new Vector2(middleX + fieldLength / 2 + 1, downField));
        }
    }


    private void SetServableTile(BaseGround baseGround)
    {

        var ground = baseGround.GetGround();
        for (int i = 0; i < fieldLength / 2; i++)
        {
            var leftCol = ground[middleX - i];
            for (int j = 0; j < leftCol.Length; j++)
            {
                if (leftCol[j].Type == TileType.IN_BOUND_NEAR_POST)
                {
                    if( j >= leftCol.Length/2 )
                        baseGround.player2UpServableTiles.Add(new Vector2(middleX - i, j));
                    else
                        baseGround.player2DownServableTiles.Add(new Vector2(middleX - i, j));
                }

            }
            var rightCol = ground[middleX + i + 1];
            for (int j = 0; j < rightCol.Length; j++)
            {
                if (rightCol[j].Type == TileType.IN_BOUND_NEAR_POST)
                {
                    if (j >= rightCol.Length / 2)
                        baseGround.player1UpServableTiles.Add(new Vector2(middleX + i + 1, j));
                    else
                        baseGround.player1DownServableTiles.Add(new Vector2(middleX + i + 1, j));
                }
            }
        }  
    }

    private void SetServeStrikableTile(BaseGround baseGround)
    {

        var ground = baseGround.GetGround();
        for (int i = 0; i < fieldLength / 2; i++)
        {
            var leftCol = ground[middleX - i];
            for (int j = 0; j < leftCol.Length; j++)
            {
                if (leftCol[j].Type == TileType.IN_BOUND_NEAR_POST
                    || (leftCol[j].Type == TileType.IN_BOUND_NO_STRIKE &&  i == 1))
                {
                    if (j >= leftCol.Length / 2)
                        baseGround.player2UpServeStrikableTiles.Add(new Vector2(middleX - i, j));
                    else
                        baseGround.player2DownServeStrikableTiles.Add(new Vector2(middleX - i, j));
                }

            }
            var rightCol = ground[middleX + i + 1];
            for (int j = 0; j < rightCol.Length; j++)
            {
                if (rightCol[j].Type == TileType.IN_BOUND_NEAR_POST
                    || (leftCol[j].Type == TileType.IN_BOUND_NO_STRIKE && i == 1))
                {
                    if (j >= rightCol.Length / 2)
                        baseGround.player1UpServeStrikableTiles.Add(new Vector2(middleX + i + 1, j));
                    else
                        baseGround.player1DownServeStrikableTiles.Add(new Vector2(middleX + i + 1, j));
                }
            }
        }
    }
    private void SetStrikableTile(BaseGround baseGround)
    {

        var ground = baseGround.GetGround();
        for (int i = 0; i < fieldLength / 2; i++)
        {
            var leftCol = ground[middleX - i];
            for (int j = 0; j < leftCol.Length; j++)
            {
                if (leftCol[j].Type == TileType.IN_BOUND_NEAR_POST || leftCol[j].Type == TileType.IN_BOUND_FAR_POST
                    || leftCol[j].Type == TileType.IN_BOUND_NO_STRIKE)
                {
                    baseGround.player2StrikableTile.Add(new Vector2(middleX - i, j));
                }
                if (leftCol[j].Type == TileType.IN_BOUND_NEAR_POST || leftCol[j].Type == TileType.IN_BOUND_FAR_POST)
                {
                    baseGround.player2SelectableTile.Add(new Vector2(middleX - i, j));
                }

            }
            var rightCol = ground[middleX + i + 1];
            for (int j = 0; j < rightCol.Length; j++)
            {
                if (rightCol[j].Type == TileType.IN_BOUND_NEAR_POST || rightCol[j].Type == TileType.IN_BOUND_FAR_POST
                    || rightCol[j].Type == TileType.IN_BOUND_NO_STRIKE)
                {
                    baseGround.player1StrikableTile.Add(new Vector2(middleX + i + 1, j));
                }
                if (rightCol[j].Type == TileType.IN_BOUND_NEAR_POST || rightCol[j].Type == TileType.IN_BOUND_FAR_POST)
                {
                    baseGround.player1SelectableTile.Add(new Vector2(middleX + i + 1, j));
                }
            }
        }
    }

    private void SetDownTheLineTile(BaseGround baseGround)
    {

        var ground = baseGround.GetGround();

        var leftDownTheLineX = middleX - (fieldLength / 2 - 1);
        var righttDownTheLineX = middleX + 1 + (fieldLength / 2 - 1);
        var leftCol = ground[leftDownTheLineX];
        var rightCol = ground[righttDownTheLineX];
        for (int j = 0; j < leftCol.Length; j++)
        {
            baseGround.player2DownTheLineTile.Add(new Vector2( leftDownTheLineX, j));
        }

        for (int j = 0; j < rightCol.Length; j++)
        {
            baseGround.player1DownTheLineTile.Add(new Vector2( righttDownTheLineX, j));
        }
        var colLength = rightCol.Length;
        for (int i = 0; i < fieldLength / 2 - 1; i++)
        {
            var leftField = middleX - i;
            var rightField = middleX + 1 + i;
            baseGround.player2DownTheLineTile.Add(new Vector2(leftField, middleY - (fieldWidth/2 - 1)));
            baseGround.player2DownTheLineTile.Add(new Vector2(leftField, middleY + 1 + (fieldWidth / 2 - 1)));
            baseGround.player1DownTheLineTile.Add(new Vector2(rightField, middleY - (fieldWidth / 2 - 1)));
            baseGround.player1DownTheLineTile.Add(new Vector2(rightField, middleY + 1 + (fieldWidth / 2 - 1)));
        }
    }

    private void SetAtTheNetTile(BaseGround baseGround)
    {

        var ground = baseGround.GetGround();
        for (int i = 0; i < fieldLength / 2; i++)
        {
            var leftCol = ground[middleX - i];
            for (int j = 0; j < leftCol.Length; j++)
            {
                if (leftCol[j].Type == TileType.IN_BOUND_NEAR_POST
                    || leftCol[j].Type == TileType.IN_BOUND_NO_STRIKE)
                {
                    baseGround.player2AtTheNetTile.Add(new Vector2(middleX - i, j));
                }

            }
            var rightCol = ground[middleX + i + 1];
            for (int j = 0; j < rightCol.Length; j++)
            {
                if (rightCol[j].Type == TileType.IN_BOUND_NEAR_POST
                    || rightCol[j].Type == TileType.IN_BOUND_NO_STRIKE)
                {
                    baseGround.player1AtTheNetTile.Add(new Vector2(middleX + i + 1, j));
                }
            }
        }
    }
}

