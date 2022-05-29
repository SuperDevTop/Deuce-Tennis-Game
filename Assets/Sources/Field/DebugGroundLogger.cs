using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    class DebugGroundLogger: IGroundLogger
    {
        public void LogGround(BaseGround ground)
        {
                for (int i = 0; i < ground.GetGround().Length; i++ )
                {
                    var col = ground.GetGround()[i];
                    Debug.Log("---------" + i);
                    for (int j = 0; j < col.Length; j++ )
                    {
                        var tile = col[j];
                        switch (tile.Type)
                        {
                            case TileType.OUT_BOUND:
                                Debug.Log("OUT");
                                break;
                            case TileType.IN_BOUND_NEAR_POST:
                                Debug.Log("IN-NEAR");
                                break;
                            case TileType.IN_BOUND_FAR_POST:
                                Debug.Log("IN-FAR");
                                break;
                            case TileType.IN_BOUND_NO_STRIKE:
                                Debug.Log("NO-STR");
                                break;
                        }
                    }

                }
        }
    }

