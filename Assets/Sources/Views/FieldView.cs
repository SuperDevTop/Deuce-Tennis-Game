using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldView : MonoBehaviour, IEventListener
{
    public GameObject TilePrefab;
    public GameObject SpdZoneDividerPrefab;
    public GameObject SpeedTextPrefab;
    public UIWidget FieldAnchor;
    public UISprite SpdZone;
    public UISprite WhiteBound1;
    public UISprite WhiteBound2;
    public UISprite WhiteBound3;
    public UISprite WhiteBound4;

    public UISprite Player1;
    public UISprite Player2;
    public UISprite FirstBounce;
    public UISprite SecondBounce;
    public UISprite FirstBounceLine;
    public UISprite SecondBounceLine;

    private TileView[][] field;
    private int tileHeight;
    private int tileWidth;
    private int groundIndexWidth;
    private int groundIndexHeight;

    private int startFarBoundX = 0;
    private int endFarBoundX = 0;

    private int startNearBoundX = 0;
    private int endNearBoundX = 0;

    private int fieldStartY = 0;
    private int fieldEndY = 0;

    BaseGround ground;

    void Start()
    {
        RegisterEvent();
    }

    private void RegisterEvent()
    {
        EventManager.Instance().attach(InGameEventType.SET_PLAYERS_POSITION, this);
        EventManager.Instance().attach(InGameEventType.ACTIVE_SERVABLE_FIELD, this);
        EventManager.Instance().attach(InGameEventType.DEACTIVE_SERVABLE_FIELD, this);
        EventManager.Instance().attach(InGameEventType.ACTIVE_MOVABLE_FIELD, this);
        EventManager.Instance().attach(InGameEventType.DEACTIVE_MOVABLE_FIELD, this);
        EventManager.Instance().attach(InGameEventType.ACTIVE_ROLLABLE_TO_TILES, this);
        EventManager.Instance().attach(InGameEventType.DEACTIVE_ROLLABLE_TO_TILES, this);
        EventManager.Instance().attach(InGameEventType.SET_BALL, this);
        EventManager.Instance().attach(InGameEventType.HIDE_BALL, this);
        EventManager.Instance().attach(InGameEventType.RESET_COLLISION, this);
        EventManager.Instance().attach(InGameEventType.CHECK_STRIKABLE, this);
        EventManager.Instance().attach(InGameEventType.DEACTIVE_STRIKABLE_TILES, this);
        EventManager.Instance().attach(InGameEventType.ACTIVE_STRIKABLE_TILES, this);
        EventManager.Instance().attach(InGameEventType.DEACTIVE_COLLISION_BOX, this);
        EventManager.Instance().attach(InGameEventType.ACTIVE_COLLISION_BOX, this);
        EventManager.Instance().attach(InGameEventType.ACTIVE_START_POSITION, this);
        EventManager.Instance().attach(InGameEventType.DEACTIVE_START_POSITION, this);
        EventManager.Instance().attach(InGameEventType.HIDE_PLAYERS, this);
    }
    public void Init(BaseGround ground)
    {
        NGUIDebug.Log("FieldView Init");
        InitData(ground);
        InitView();
    }
    private void InitData(BaseGround ground)
    {
        this.ground = ground;
        //DebugGroundLogger logger = new DebugGroundLogger();
        //logger.LogGround(ground);
    }
	
    private void InitView()
    {
        InitGroundView();
        InitWhiteBoundView();
        InitSpdZoneView();
    }
    private void InitGroundView()
    {
        groundIndexWidth = ground.GetGroundLength();
        groundIndexHeight = ground.GetGroundWidth();
        field = new TileView[groundIndexWidth][];
        for (int i = 0; i < groundIndexWidth; i++)
        {
            field[i] = new TileView[groundIndexHeight];
            for (int j = 0; j < groundIndexHeight; j++)
            {
                var createdObj = NGUITools.AddChild(FieldAnchor.gameObject, TilePrefab);
                field[i][j] = createdObj.GetComponent<TileView>();
                field[i][j].TileX = i;
                field[i][j].TileY = j;
                createdObj.name = "Tile_" + i + "_" + j;
                UIEventListener.Get(field[i][j].gameObject).onClick += OnTileClick;
                
                tileWidth = field[i][j].GetWidth();
                tileHeight = field[i][j].GetHeight();
                createdObj.transform.localPosition = new Vector3(tileWidth/2 + i * tileWidth - tileWidth * groundIndexWidth / 2
                    , tileHeight/2 + j * tileHeight - tileHeight * groundIndexHeight / 2, 0);

                createdObj.transform.localScale = Vector3.one;

                var tileType = ground.GetTile(i, j).Type;
                if (tileType == TileType.IN_BOUND_FAR_POST
                    || tileType == TileType.IN_BOUND_NEAR_POST
                    || tileType == TileType.IN_BOUND_NO_STRIKE)
                {
                    field[i][j].FieldBG.gameObject.SetActive(true);
                }

                if (tileType == TileType.IN_BOUND_FAR_POST)
                {
                    if (startFarBoundX == 0)
                        startFarBoundX = i;
                    endFarBoundX = i;
                    if (fieldStartY == 0)
                        fieldStartY = j;
                    fieldEndY = j;
                }
                else if (tileType == TileType.IN_BOUND_NEAR_POST)
                {
                    if (startNearBoundX == 0)
                        startNearBoundX = i;
                    endNearBoundX = i;
                }
            }
        }
    }
    private void OnTileClick(GameObject obj)
    {
        TileView tile = obj.GetComponent<TileView>();
        //Debug.Log("Tile Click : " + tile.TileX + " - " + tile.TileY);

        BaseObject input = new BaseObject();
        input.SetProperty("inputType", InputType.SELECT_TILE);
        input.SetProperty("inputData", tile.GetPosition());
        EventManager.Instance().fire(InGameEventType.INPUT, input);
    }
    private void InitWhiteBoundView()
    {
        WhiteBound1.width = (endFarBoundX - startFarBoundX + 1) * tileWidth;
        WhiteBound1.height = (fieldEndY - fieldStartY + 1) * tileHeight;
        WhiteBound1.gameObject.SetActive(true);

        WhiteBound2.width = (endNearBoundX - startNearBoundX + 1)/2 * tileWidth + 4;
        WhiteBound2.height = (fieldEndY - fieldStartY + 1) * tileHeight;
        WhiteBound2.transform.localPosition = new Vector3(-WhiteBound2.width/2, 0, 0);
        WhiteBound2.gameObject.SetActive(true);

        WhiteBound3.width = WhiteBound2.width;
        WhiteBound3.height = WhiteBound2.height;
        WhiteBound3.transform.localPosition = new Vector3(WhiteBound3.width / 2, 0, 0);
        WhiteBound3.gameObject.SetActive(true);

        WhiteBound4.width = WhiteBound2.width*2;
        WhiteBound4.height = WhiteBound2.height/2;
        WhiteBound4.transform.localPosition = new Vector3(0, WhiteBound4.height/2, 0);
        WhiteBound4.gameObject.SetActive(true);

    }

    private void InitSpdZoneView()
    {
        SpdZone.width = tileWidth * groundIndexWidth;
        SpdZone.transform.localPosition = new Vector3(0, -tileHeight * groundIndexHeight / 2 - 5, 0);
        SpdZone.gameObject.SetActive(true);


        InitFixedPositionDividerZone();
        InitDividerZoneLeft();
        InitDividerZoneRight();

        
    }

    private void InitFixedPositionDividerZone()
    {
        var leftSpdDivider = NGUITools.AddChild(FieldAnchor.gameObject, SpdZoneDividerPrefab);
        leftSpdDivider.transform.localPosition = new Vector3(-tileWidth * groundIndexWidth / 2,
            -tileHeight * groundIndexHeight / 2 - 5, 0);

        var rightSpdDivider = NGUITools.AddChild(FieldAnchor.gameObject, SpdZoneDividerPrefab);
        rightSpdDivider.transform.localPosition = new Vector3(tileWidth * groundIndexWidth / 2,
            -tileHeight * groundIndexHeight / 2 - 5, 0);

        var middleSpdDivider = NGUITools.AddChild(FieldAnchor.gameObject, SpdZoneDividerPrefab);
        middleSpdDivider.transform.localPosition = new Vector3(0,
            -tileHeight * groundIndexHeight / 2 - 5, 0);
    }
    private void InitDividerZoneLeft()
    {
        int middleX = groundIndexWidth / 2;
        int lastKnowSpd = ground.GetTile(middleX - 1, 0).SPD;
        int lastX = 0;
        for (int i = 0; i < middleX; i++)
        {
            BaseTile tile = ground.GetTile(middleX - 1 - i, 0);
            var spd = tile.SPD;
            if (lastKnowSpd != spd)
            {
                int newX = -tileWidth * i;

                var spdText = NGUITools.AddChild(FieldAnchor.gameObject, SpeedTextPrefab);
                spdText.GetComponent<UILabel>().text = lastKnowSpd.ToString();
                spdText.transform.localPosition = new Vector3((newX - lastX) / 2 + lastX, -tileHeight * groundIndexHeight / 2 - 5, 0);

                var spdDivider = NGUITools.AddChild(FieldAnchor.gameObject, SpdZoneDividerPrefab);
                spdDivider.transform.localPosition = new Vector3(newX,
                    -tileHeight * groundIndexHeight / 2 - 5, 0);
                lastKnowSpd = spd;
                lastX = newX;
            }
        }

        var lastSpdText = NGUITools.AddChild(FieldAnchor.gameObject, SpeedTextPrefab);
        var lastNewX = -tileWidth * groundIndexWidth / 2;
        lastSpdText.GetComponent<UILabel>().text = lastKnowSpd.ToString();
        lastSpdText.transform.localPosition = new Vector3(( lastNewX- lastX) / 2 + lastX, -tileHeight * groundIndexHeight / 2 - 5, 0);
    }

    private void InitDividerZoneRight()
    {
        int middleX = groundIndexWidth / 2;
        int lastKnowSpd = ground.GetTile(middleX, 0).SPD;
        int lastX = 0;
        for (int i = middleX; i < groundIndexWidth; i++)
        {
            BaseTile tile = ground.GetTile(i, 0);
            var spd = tile.SPD;
            if (lastKnowSpd != spd)
            {
                int newX = tileWidth * ( i - middleX );

                var spdText = NGUITools.AddChild(FieldAnchor.gameObject, SpeedTextPrefab);
                spdText.GetComponent<UILabel>().text = lastKnowSpd.ToString();
                spdText.transform.localPosition = new Vector3((newX - lastX) / 2 + lastX, -tileHeight * groundIndexHeight / 2 - 5, 0);

                var spdDivider = NGUITools.AddChild(FieldAnchor.gameObject, SpdZoneDividerPrefab);
                spdDivider.transform.localPosition = new Vector3(newX,
                    -tileHeight * groundIndexHeight / 2 - 5, 0);

                lastKnowSpd = spd;
                lastX = newX;
            }
        }

        var lastSpdText = NGUITools.AddChild(FieldAnchor.gameObject, SpeedTextPrefab);
        var lastNewX = tileWidth * groundIndexWidth / 2;
        lastSpdText.GetComponent<UILabel>().text = lastKnowSpd.ToString();
        lastSpdText.transform.localPosition = new Vector3((lastNewX - lastX) / 2 + lastX, -tileHeight * groundIndexHeight / 2 - 5, 0);
    }



    public int GetGroundHeight()
    {
        return groundIndexHeight * tileHeight;
    }

    public int GetGroundWidth()
    {
        return groundIndexWidth * tileWidth;
    }



    public void handle(InGameEventType aEventType, object aEventData)
    {
        switch( aEventType )
        {
            case InGameEventType.SET_PLAYERS_POSITION:
                SetPlayerPosition(aEventData);
                break;
            case InGameEventType.HIDE_PLAYERS:
                HidePlayers();
                break;
            case InGameEventType.ACTIVE_SERVABLE_FIELD:
                ToggleSelectableField(aEventData, true);
                break;
            case InGameEventType.DEACTIVE_SERVABLE_FIELD:
                ToggleSelectableField(aEventData, false);
                break;

            case InGameEventType.ACTIVE_MOVABLE_FIELD:
                ToggleSelectableField(aEventData, true);
                break;
            case InGameEventType.DEACTIVE_MOVABLE_FIELD:
                ToggleSelectableField(aEventData, false);
                break;
            case InGameEventType.SET_BALL:
                SetBall(aEventData);
                break;
            case InGameEventType.ACTIVE_ROLLABLE_TO_TILES:
                ToggleRollableToTile(aEventData, true);
                break;
            case InGameEventType.DEACTIVE_ROLLABLE_TO_TILES:
                ToggleRollableToTile(aEventData, false);
                break;
            case InGameEventType.HIDE_BALL:
                HideBall();
                break;
            case InGameEventType.RESET_COLLISION:
                ResetCollision();
                break;
            case InGameEventType.CHECK_STRIKABLE:
                CheckStrikable(aEventData);
                break;
            case InGameEventType.DEACTIVE_STRIKABLE_TILES:
                StartCoroutine( ToggleStrikableTiles(aEventData, false, false) );
                break;
            case InGameEventType.ACTIVE_STRIKABLE_TILES:
                var dataMap = (Dictionary<string, object>)aEventData;
                var movableField = dataMap["movableField"];
                var player = (IPlayer)dataMap["player"];
                Debug.Log("NO_HIT_BEFORE_FIRST_BOUNCE: " + player.GetTotalBuffValue(BUFF_TYPE.NO_HIT_BEFORE_FIRST_BOUNCE));
                var canHit = player.GetTotalBuffValue(BUFF_TYPE.NO_HIT_BEFORE_FIRST_BOUNCE ) != 0? false: true;
                StartCoroutine(ToggleStrikableTiles(movableField, canHit, true));
                break;
            case InGameEventType.DEACTIVE_COLLISION_BOX:
                StartCoroutine(ToggleCollisionBoundBox(false));
                break;
            case InGameEventType.ACTIVE_COLLISION_BOX:
                StartCoroutine(ToggleCollisionBoundBox(true));
                break;
            case InGameEventType.ACTIVE_START_POSITION:
                ToggleSelectableField(aEventData, true);
                break;
            case InGameEventType.DEACTIVE_START_POSITION:
                ToggleSelectableField(aEventData, false);
                break;
        }
    }
    private void SetPlayerPosition(object aEventData)
    {
        var data = (Dictionary<string, Vector2>)aEventData;



        if(  data.ContainsKey("player1Pos") )
        {
            var player1Pos = data["player1Pos"];
            //Debug.Log("player1Pos " + player1Pos.ToString());
            TileView player1StartTile = field[(int)player1Pos.x][(int)player1Pos.y];
            var player1StartLocalPos = player1StartTile.transform.localPosition;
            Player1.transform.localPosition = new Vector3(player1StartLocalPos.x, player1StartLocalPos.y, player1StartLocalPos.z);
            Player1.gameObject.SetActive(true);
        }

        if (data.ContainsKey("player2Pos"))
        {
            var player2Pos = data["player2Pos"];
            //Debug.Log("player2Pos " + player2Pos.ToString());
            TileView player2StartTile = field[(int)player2Pos.x][(int)player2Pos.y];
            var player2StartLocalPos = player2StartTile.transform.localPosition;
            Player2.transform.localPosition = new Vector3(player2StartLocalPos.x, player2StartLocalPos.y, player2StartLocalPos.z);
            Player2.gameObject.SetActive(true);
        }
    }

    private void HidePlayers()
    {
        Player1.gameObject.SetActive(false);
        Player2.gameObject.SetActive(false);
    }

    private void ToggleSelectableField(object aEventData, bool isActive)
    {
        List<Vector2> selectableFields = (List<Vector2>)aEventData;
        foreach( var tile in selectableFields )
        {
            var tileView = field[(int)tile.x][(int)tile.y];
            tileView.Selectable.gameObject.SetActive(isActive);
            tileView.GetComponent<BoxCollider>().enabled = isActive;
        }
    }

    private void ToggleMovableField(object aEventData, bool isActive)
    {
        List<Vector2> movableFields = (List<Vector2>)aEventData;
        foreach (var tile in movableFields)
        {
            var tileView = field[(int)tile.x][(int)tile.y];
            tileView.Selectable.gameObject.SetActive(isActive);
            tileView.GetComponent<BoxCollider>().enabled = isActive;
        }
    }

    private void SetBall(object aEventData)
    {
        BounceGame game = (BounceGame)aEventData;
        Vector2 ballPos = game.firstBounce.GetPosition();
        FirstBounce.gameObject.SetActive(true);
        TileView ball = field[(int)ballPos.x][(int)ballPos.y];
        FirstBounce.transform.localPosition = ball.transform.localPosition;
        var fixedDistance = game.currentPlayer.GetBuffValue(BUFF_DURATION.STROKE, BUFF_TYPE.FIXED_BOUNCE_DISTANCE);
        var additionDistance = game.currentPlayer.GetTotalBuffValue(BUFF_TYPE.ADDITION_SECOND_BOUNCE);
        Debug.Log("FieldView: addistionDistance: " + additionDistance);
        CalculateAndSetSecondBounce(ballPos, fixedDistance, additionDistance);
        CalculateAndSetFirstBounceLine(game);
        CalculateAndSetSecondBounceLine();
    }

    private void CalculateAndSetSecondBounce(Vector2 firstBounce, int fixedDistance, int additionDistance)
    {
        int middleX = field.Length / 2;
        //Debug.Log("MiddX:" + middleX);
        //Debug.Log("firstBounce.x:" + firstBounce.x);
        if( firstBounce.x < middleX )//p2 play the ball
        {
            
            var anchorXIndex = fixedDistance == 0 ? middleX - (middleX - firstBounce.x) * 2 - additionDistance: firstBounce.x - 1;
            if( anchorXIndex < 0  )
            {
                anchorXIndex = 0;
            }

            TileView p2Tile = GetTileContainPosition(Player2.transform.localPosition.x, Player2.transform.localPosition.y);
            TileView anchorTile = field[(int)anchorXIndex][p2Tile.TileY];

            Vector3 p2LocalPos = Player2.transform.localPosition;
            Vector3 firstBounceLocalPos = FirstBounce.transform.localPosition;
            Vector3 anchorBounceLocalPos = anchorTile.transform.localPosition;

            var deltaX = p2LocalPos.x - firstBounceLocalPos.x;
            var deltaY = firstBounceLocalPos.y - p2LocalPos.y;
            var outerDeltaX = p2LocalPos.x - anchorBounceLocalPos.x;
            var outerDeltaY = deltaY * outerDeltaX / deltaX;
            var secondBoundX = anchorBounceLocalPos.x;
            var secondBoundY = anchorBounceLocalPos.y + outerDeltaY;

            //TileView secondBounceTile = GetTileContainPosition(secondBoundX, secondBoundY);
            //SecondBounce.transform.localPosition = secondBounceTile.transform.localPosition;
            SecondBounce.transform.localPosition = new Vector3(secondBoundX, secondBoundY, 0);
            SecondBounce.gameObject.SetActive(true);
            
        }
        else//p1 play the ball
        {
            var anchorXIndex = fixedDistance == 0? middleX - 1 + ( firstBounce.x - middleX + 1 )*2 + additionDistance: firstBounce.x + 1;
            if (anchorXIndex >= field.Length)
            {
                anchorXIndex = field.Length - 1;
            }
            TileView p1Tile = GetTileContainPosition(Player1.transform.localPosition.x, Player1.transform.localPosition.y);
            //Debug.Log("anchor X Index: " + anchorXIndex);
            //Debug.Log("p1TileX: " + p1Tile.TileX);
            //Debug.Log("p1TileY: " + p1Tile.TileY);
            TileView anchorTile = field[(int)anchorXIndex][p1Tile.TileY];

            Vector3 p1LocalPos = Player1.transform.localPosition;
            Vector3 firstBounceLocalPos = FirstBounce.transform.localPosition;
            Vector3 anchorBounceLocalPos = anchorTile.transform.localPosition;

            var deltaX = firstBounceLocalPos.x - p1LocalPos.x;
            var deltaY = firstBounceLocalPos.y - p1LocalPos.y;
            var outerDeltaX = anchorBounceLocalPos.x - p1LocalPos.x;
            var outerDeltaY = deltaY * outerDeltaX / deltaX;
            var secondBoundX = anchorBounceLocalPos.x;
            var secondBoundY = anchorBounceLocalPos.y + outerDeltaY;

            //TileView secondBounceTile = GetTileContainPosition(secondBoundX, secondBoundY);
            //SecondBounce.transform.localPosition = secondBounceTile.transform.localPosition;
            SecondBounce.transform.localPosition = new Vector3(secondBoundX, secondBoundY, 0);
            SecondBounce.gameObject.SetActive(true);
        }

    }
    private void CalculateAndSetFirstBounceLine(BounceGame t)
    {
        FirstBounceLine.gameObject.SetActive(true);
        UISprite currentPlayerSprite;
        if (t.currentPlayer == t.firstPlayer)
        {
            currentPlayerSprite = Player1;
        }
        else
        {
            currentPlayerSprite = Player2;

        }
        FirstBounceLine.width = (int)Vector3.Distance(currentPlayerSprite.transform.localPosition, FirstBounce.transform.localPosition);
        FirstBounceLine.transform.localPosition = FirstBounce.transform.localPosition;
        //BounceLine.transform.localRotation.SetLookRotation(SecondBounce.transform.localPosition);
        FirstBounceLine.transform.localRotation = Quaternion.identity;
        FirstBounceLine.transform.Rotate(Vector3.forward, AngleBetweenVector2(Vector2.right
            , new Vector2(currentPlayerSprite.transform.localPosition.x - FirstBounce.transform.localPosition.x,
                currentPlayerSprite.transform.localPosition.y - FirstBounce.transform.localPosition.y)));
        //= Vector3.Angle(FirstBounce.transform.localPosition, SecondBounce.transform.localPosition);
    }
    private void CalculateAndSetSecondBounceLine()
    {
        SecondBounceLine.gameObject.SetActive(true);
        SecondBounceLine.width = (int)Vector3.Distance(FirstBounce.transform.localPosition, SecondBounce.transform.localPosition);
        SecondBounceLine.transform.localPosition = FirstBounce.transform.localPosition;
        //BounceLine.transform.localRotation.SetLookRotation(SecondBounce.transform.localPosition);
        SecondBounceLine.transform.localRotation = Quaternion.identity;
        SecondBounceLine.transform.Rotate(Vector3.forward, AngleBetweenVector2(Vector2.right
            , new Vector2(SecondBounce.transform.localPosition.x - FirstBounce.transform.localPosition.x,
                SecondBounce.transform.localPosition.y - FirstBounce.transform.localPosition.y)));
        //= Vector3.Angle(FirstBounce.transform.localPosition, SecondBounce.transform.localPosition);
    }

    private TileView GetTileContainPosition(float x, float y)
    {
        foreach( var col in field )
        {
            foreach( var tile in col )
            {
                if (IsTileContainPosition(tile, x, y))
                    return tile;
            }
        }
        return null;
    }

    private bool IsTileContainPosition(TileView tile, float x, float y)
    {
        var TileHeight = tile.TileBG.height;
        var TileWidth = tile.TileBG.width;
        var TileX = tile.transform.localPosition.x;
        var TileY = tile.transform.localPosition.y;
        if (TileX - tileWidth / 2 <= x && x <= TileX + tileWidth / 2
            && TileY - tileHeight / 2 <= y && y <= TileY + tileHeight / 2)
            return true;

        else return false;
    }
    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    private void ToggleRollableToTile(object aEventData, bool isActive)
    {
        Dictionary<int, Vector2> map = (Dictionary<int, Vector2>) aEventData;
        foreach( var value in map.Keys)
        {
            var tilePos = map[value];
            var tile = field[(int)tilePos.x][(int)tilePos.y];
            tile.DiceNumber.text = value.ToString();
            tile.DiceNumber.gameObject.SetActive(isActive);

        }
    }

    private void HideBall()
    {
        FirstBounce.gameObject.SetActive(false);
        SecondBounce.gameObject.SetActive(false);
        SecondBounceLine.gameObject.SetActive(false);
        FirstBounceLine.gameObject.SetActive(false);
    }

    private void ResetCollision()
    {
        foreach( var col in field )
        {
            foreach( var tile in col )
            {
                tile.IsCollideWithFirstBounceLine = false;
                tile.IsCollideWithSecondBounceLine = false;
                tile.IsCollideWithSecondBounce = false;
            }
        }
    }

    private void CheckStrikable(object aEventData)
    {
        IPlayer player = (IPlayer)aEventData;
        Vector2 pos = player.GetPosition();
        Debug.Log("FieldView: CheckStrikable: " + pos);
        bool strikable = false;
        if (field[(int)pos.x][(int)pos.y].IsCollideWithSecondBounce
            || field[(int)pos.x][(int)pos.y].IsCollideWithSecondBounceLine)
        {
            strikable = true;
        }
        var canHit = player.GetTotalBuffValue(BUFF_TYPE.NO_HIT_BEFORE_FIRST_BOUNCE ) != 0? false: true;
        if (field[(int)pos.x][(int)pos.y].IsCollideWithFirstBounceLine && canHit)
        {
            strikable = true;
        }
        Debug.Log("FieldView: CheckStrikable: " + strikable);
        EventManager.Instance().fire(InGameEventType.CHECK_STRIKABLE_DONE, strikable);
    }

    private IEnumerator ToggleStrikableTiles(object aEventData, bool canHitBeforeFirstBounce, bool isActive)
    {
        yield return 0;
        var movableField = (List<Vector2>)aEventData;
        foreach( var tile in movableField )
        {
            if (field[(int)tile.x][(int)tile.y].IsCollideWithSecondBounce
                || field[(int)tile.x][(int)tile.y].IsCollideWithSecondBounceLine )
            {
                field[(int)tile.x][(int)tile.y].Strikable.gameObject.SetActive(isActive);
            }
            if (field[(int)tile.x][(int)tile.y].IsCollideWithFirstBounceLine )
            {
                if( !isActive )
                    field[(int)tile.x][(int)tile.y].Strikable.gameObject.SetActive(isActive);
                else
                    if(canHitBeforeFirstBounce)
                        field[(int)tile.x][(int)tile.y].Strikable.gameObject.SetActive(isActive);
            }

        }
    }

    private IEnumerator ToggleCollisionBoundBox(bool isActive)
    {
        yield return 0;
        FirstBounceLine.GetComponent<BoxCollider>().enabled = isActive;
        SecondBounceLine.GetComponent<BoxCollider>().enabled = isActive;
        SecondBounce.GetComponent<BoxCollider>().enabled = isActive;
    }
}
