using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GroundFactory
{
    private static GroundFactory instance;
    public static GroundFactory Instance
    {
        get{
            if (instance == null)
                instance = new GroundFactory();
            return instance;
        }
        
    }

    public BaseGround CreateStandardGround()
    {
        BaseGround ground = new StandardGround(new StandardGroundTileStrategy());
        return ground;
    }
}
