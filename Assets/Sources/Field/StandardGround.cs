using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class StandardGround : BaseGround
{
    private   StandardGroundTileStrategy standardGroundTileStrategy;

    public StandardGround(ITileStrategy strategy)
        : base(strategy)
    {

    }
    protected override void InitGround()
    {
        ground = new BaseTile[28][];
        if (ground.Length % 2 != 0)
            throw new Exception("Not supported kind of Ground, Width has to be an even number!");
        for (int i = 0; i < ground.Length; i++ )
        {
            ground[i] = new BaseTile[14];
            if (ground[i].Length % 2 != 0)
                throw new Exception("Not supported kind of Ground, Height has to be an even number!");
            for( int j = 0; j < ground[i].Length; j++ )
            {
                ground[i][j] = new BaseTile();
            }
        }
    }
}

