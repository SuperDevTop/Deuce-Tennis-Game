using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUDView : MonoBehaviour, IEventListener
{
    public GameObject scorePrefab;
    public FieldView fieldView;
    public UIWidget topContainer;
    public UISprite BG;
    public UIGrid player1Score;
    public UIGrid player2Score;
    public PointBoard player1PointBoard;
    public PointBoard player2PointBoard;
    private List<UISprite> player1ScoreList;
    private List<UISprite> player2ScoreList;
	// Use this for initialization
    private IPointDisplayer displayer;
	void Start () 
    {
        RegisterEvent();
        Init();
        StartCoroutine(InitHUDView());


	}
    private void RegisterEvent()
    {
        EventManager.Instance().attach(InGameEventType.UPDATE_GAME_SCORE, this);
    }

    private void Init()
    {
        displayer = new TennisPointDisplayer();
        player1ScoreList = new List<UISprite>();
        player2ScoreList = new List<UISprite>();
    }
    private IEnumerator InitHUDView()
    {
        yield return 0;

        var screenHeight = BG.height;
        topContainer.height = (int)(screenHeight - fieldView.GetGroundHeight()) / 2;
        topContainer.ResetAndUpdateAnchors();
        yield return 0;

        InitPlayer1ScoreView();
        InitPlayer2ScoreView();


    }

    private void InitPlayer1ScoreView()
    {
        //To make this true, the TOP Panel has to anchored Left, Right To Panel HUD
        //Debug.Log("BG.width: " + BG.width);
        //Debug.Log("player1PointBoard.transform.localPosition.x: " + player1PointBoard.transform.localPosition.x);
        //Debug.Log("player1PointBoard.GetBoardWidth() / 2: " + player1PointBoard.GetBoardWidth() / 2);
        int availableSpace = (int)(BG.width/2 - Mathf.Abs( player1PointBoard.transform.localPosition.x ) - player1PointBoard.GetBoardWidth() / 2 ) -40;
        player1Score.cellWidth = availableSpace / GameConfig.NUMBER_OF_GAMES_FOR_WIN;
        for( int i = 0; i < GameConfig.NUMBER_OF_GAMES_FOR_WIN; i++ )
        {
            var createdObj = NGUITools.AddChild(player1Score.gameObject, scorePrefab);
            createdObj.transform.localScale = Vector3.one;
            player1ScoreList.Add(createdObj.GetComponent<UISprite>());
            createdObj.SetActive(false);
        }
        player1ScoreList.Reverse();
        player1Score.Reposition();
    }

    private void InitPlayer2ScoreView()
    {
        //To make this true, the TOP Panel has to anchored Left, Right To Panel HUD
        //Use the same space as player1PointBoard
        int availableSpace = (int)(BG.width / 2 - Mathf.Abs(player1PointBoard.transform.localPosition.x) - player1PointBoard.GetBoardWidth() / 2) - 40;
        player2Score.cellWidth = availableSpace / GameConfig.NUMBER_OF_GAMES_FOR_WIN;
        for (int i = 0; i < GameConfig.NUMBER_OF_GAMES_FOR_WIN; i++)
        {
            var createdObj = NGUITools.AddChild(player2Score.gameObject, scorePrefab);
            createdObj.transform.localScale = Vector3.one;
            player2ScoreList.Add(createdObj.GetComponent<UISprite>());
            createdObj.SetActive(false);
        }
        player2Score.Reposition();
    }

    public void handle(InGameEventType aEventType, object aEventData)
    {
        switch( aEventType )
        {
            case InGameEventType.UPDATE_GAME_SCORE:
                UpdateGameScore(aEventData);
                break;
        }
    }

    private void UpdateGameScore(object aEventData)
    {
        Debug.Log("Call To Update Game Score!!!");
        var data = (Dictionary<string, int>)aEventData;
        var player1Score = data["player1Score"];
        var player2Score = data["player2Score"];
        var player1Point = data["player1Point"];
        var player2Point = data["player2Point"];
        Debug.Log("player1Score: " + player1Score);
        Debug.Log("player2Score: " + player2Score);
        Debug.Log("player1Point: " + player1Point);
        Debug.Log("player2Point: " + player2Point);
        string[] displayValues = displayer.GetPointDisplayValue(player1Point, player2Point);
        player1PointBoard.SetPoint(displayValues[0]);
        player2PointBoard.SetPoint(displayValues[1]);

        for( int i = 0; i < player1Score; i++ )
        {
            if( i >= player1ScoreList.Count )
                break;
            player1ScoreList[i].color = Color.red;
            player1ScoreList[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < player2Score; i++)
        {
            if (i >= player2ScoreList.Count)
                break;
            player2ScoreList[i].color = Color.red;
            player2ScoreList[i].gameObject.SetActive(true);
        }
    }
}
