using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum BUFF_TYPE
{
    SCORE, POINT, MOVEMENT_SPD, RECOVER_SPD, ADDITION_FIRST_BOUNCE, ADDITION_SECOND_BOUNCE
        , DOUBLE_POINT, UNLIMIT_STROKE, STROKE_CARD, DICE
        , BONUS_POINT //homesupport
        //STROKE TYPE OF BUFF
        , FIXED_BOUNCE_DISTANCE // ball bounce reduce to 1
        , TAKE_BALL_AFTER_BOUNCE
        , NO_ZERO_NET_HITTING
        , NO_HIT_BEFORE_FIRST_BOUNCE
}

public enum BUFF_DURATION
{
    ONE_POINT, WHOLE_GAME, STROKE
}


