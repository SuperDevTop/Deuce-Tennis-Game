using Assets.Scripts.GamePlay;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Score
{
    First = 0,
    Second = 15,
    Third = 30,
    Forth = 40
}

public class GameEngine : MonoBehaviour
{
    public static GameEngine Instance;
    public Image[] playerAvatars;
    public Sprite[] tacticCardSprites;
    public GameObject[] tiles;
    public GameObject[] player1ServeTiles;
    public GameObject[] player2ServeTiles;
    public GameObject[] player1whereToServeTiles;
    public GameObject[] player2whereToserveTiles;
    public GameObject[] playerPans;
    public GameObject[] notifications;
    public GameObject[] numberTexts;
    public Sprite [] coach1Commands;
    public Sprite[] coach2Commands;
    public Sprite[] coach11Commands;
    public Sprite[] coach22Commands;
    public Image[] coachCommandImages;
    public GameObject[] rollBall;
    public GameObject[] tennisBall;
    public GameObject player1Position;
    public GameObject player2Position;
    public GameObject[] lines;
    public GameObject[] rollText;
    public GameObject tacticalCards1;
    public GameObject characterCards;
    public GameObject panel;
    public GameObject[] serveNotifications;
    public Text player1ScoreText;
    public Text player1LevelText;
    public Text player2ScoreText;
    public Text player2LevelText;    
    public GameObject strokeCards;
    public GameObject strokeCards1;
    public GameObject bonusButton0;
    public GameObject bonusButton1;
    //public Sprite rollNetSprite;
    public GameObject[] netGameObject;    
    public GameObject[] tacticCards2;
    public GameObject tacticCardset2;
    public GameObject panel2;
    public GameObject characters2;
    public GameObject tacticCardMessage2;
    public GameObject[] numberSets;

    public GameObject tutorialObject;
    public GameObject playObject;

    int gameRoundNumber;
    public bool isNoRepeat;
    public bool isMoveToStrike;

    public PlayerInfo player1 = new PlayerInfo();
    public PlayerInfo player2 = new PlayerInfo();
    public GamePlayInfo gameplayInfo = new GamePlayInfo();
    int rowNumber;
    int columnNumber;
    int rollNumber;
    int x;
    int y;
    int x3; int y3;
    public int x1; public int y1; public int x2; public int y2;
    int maxMove;
    int step;    

    public bool isFirstServeFailed;
    bool isHideMessgae;
    public bool isServeState;
    public bool isNoMove;

    int SpecialStrokeCardUseCount;
    public bool isTopSpin;
    public bool isSliceShot;
    public bool isPowerShot;
    public bool isLobShot;
    public bool isDropShot;

    int dividedRowNumber;
    int dividedColumnNumber;

    GameObject prevPlayerPan;

    
    public bool isPlayer1SetNet;
    public bool isPlayer1SetDefensive;
    public bool isPlayer1SetAccurate;
    public bool isPlayer1SetOffensive;
    public bool isPlayer1SetSmart;
    
    public bool isPlayer2SetNet;
    public bool isPlayer2SetDefensive;
    public bool isPlayer2SetAccurate;
    public bool isPlayer2SetOffensive;
    public bool isPlayer2SetSmart;

    public int player1GetNetCount;
    public int player1GetDefensiveCount;
    public int player1GetAccurateCount;
    public int player1GetOffensiveCount;
    public int player1GetSmartCount;

    public int player2GetNetCount;
    public int player2GetDefensiveCount;
    public int player2GetAccurateCount;
    public int player2GetOffensiveCount;
    public int player2GetSmartCount;

    public int rallyCount;
    public bool isPlayer1HitAccurate;
    public bool isPlayer2HitAccurate;

    public bool isPlayer1Volley;
    public bool isPlayer2Volley;

    public List<GameObject> enabledHitTiles = new List<GameObject>();

    public bool isAIAction = false;
    public bool isPlayer1Serve;

    void Awake()
    {
        Instance = this;   
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
        {
            if (PlayerPrefs.GetString("isFirstPlay") == "true")
            {
                tutorialObject.SetActive(true);
                playObject.SetActive(false);
                PlayerPrefs.SetString("isFirstPlay", "false");
            }
        }
           

        if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
        {
            dividedRowNumber = 26;
            dividedColumnNumber = 12;
        }        
        else if (Application.loadedLevelName == "GameOrange1")
        {
            dividedRowNumber = 20;
            dividedColumnNumber = 8;
        }
        else if (Application.loadedLevelName == "GameRed1")
        {
            dividedRowNumber = 18;
            dividedColumnNumber = 8;
        }        

        player1.Level = 0;
        player1.Score = (int)Score.First;
        player2.Level = 0;
        player2.Level = (int)Score.First;
        player1.StrokeCardCount = 0;
        player2.StrokeCardCount = 0;

        if (!MainMenu.isRollAnimatin)
        {
            rollBall[0].GetComponent<Animator>().enabled = false;
        }

        int index = UnityEngine.Random.RandomRange(0, 2);

        if (index == 0)
        {
            isPlayer1Serve = true;
            player1.IsMyServe = true;
            player2.IsMyServe = false;
            serveNotifications[0].GetComponent<Image>().enabled = true;
            serveNotifications[1].GetComponent<Image>().enabled = false;
            player1.IsMyTurn = true;
            player2.IsMyTurn = false;
        }
        else
        {
            isPlayer1Serve = false;
            player1.IsMyServe = false;
            player2.IsMyServe = true;
            player1.IsMyTurn = false;
            player2.IsMyTurn = true;
            serveNotifications[0].GetComponent<Image>().enabled = false;
            serveNotifications[1].GetComponent<Image>().enabled = true;
        }

        //for (int i = 0; i < GameScene.Instance.strokePans.Length; i++)
        //{
        //    GameScene.Instance.strokePans[i].SetActive(false);
        //} 
    }

    public void FormatBoolFlag()
    {
        isTopSpin = false;
        isSliceShot = false;
        isPowerShot = false;
        isLobShot = false;
        isDropShot = false;
        player1.BonusMove = 0;
        player2.BonusMove = 0;
    }

    public void FormatBoolFlag1()
    {
        isPlayer1SetNet = false;
        isPlayer1SetDefensive = false;
        isPlayer1SetAccurate = false;
        isPlayer1SetOffensive = false;
        isPlayer1SetSmart = false;

        isPlayer2SetNet = false;
        isPlayer2SetDefensive = false;
        isPlayer2SetAccurate = false;
        isPlayer2SetOffensive = false;
        isPlayer2SetSmart = false;
        rallyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //print("player1.strokecardcount:" + player1.StrokeCardCount);
        //print("MainMenu.strokeNumber:" + player1.StrokeCardCount);
        //print("player2.strokecardcount:" + player2.StrokeCardCount);        

        


        if (gameplayInfo.IsStart && isNoRepeat)
        {
            isFirstServeFailed = false;            
            characterCards.SetActive(false);
            //notifications[0].SetActive(true);
            //panel.SetActive(true);
            //tacticalCards1.SetActive(true);
            player1.StrokeCardCount = 0;
            player2.StrokeCardCount = 0;
            //FormatBoolFlag();
            //FormatBoolFlag1();

            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                tiles[i].GetComponentsInChildren<Image>()[1].enabled = false;
            }

            //for (int i = 0; i < GameScene.Instance.strokePans.Length; i++)
            //{
            //    GameScene.Instance.strokePans[i].SetActive(false);
            //}

            for (int i = 0; i < numberTexts.Length; i++)
            {
                numberTexts[i].SetActive(false);
            }

            lines[0].SetActive(false);
            lines[1].SetActive(false);
            rollBall[0].SetActive(false);
            tennisBall[0].SetActive(false);
            tennisBall[1].SetActive(false);
            playerPans[0].SetActive(false);
            playerPans[1].SetActive(false);

            if (MainMenu.isAI && isAIAction)
            {
                //print("got it!");
                isNoRepeat = false;
                gameplayInfo.IsStart = false;
                gameplayInfo.IsServe = true;
                //characterCards.SetActive(true);
                isNoRepeat = true;
            }
            else if (!MainMenu.isAI)
            {
                isNoRepeat = false;
                gameplayInfo.IsStart = false;
                gameplayInfo.IsServe = true;
                //characterCards.SetActive(true);
                isNoRepeat = true;
            }
        }       
        else if (gameplayInfo.IsServe && isNoRepeat)
        {
            isNoRepeat = false;
            isServeState = true;
            rollBall[0].SetActive(false);
            rollBall[1].SetActive(false);
            rollText[0].SetActive(false);
            rollText[1].SetActive(false);

            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                tiles[i].GetComponentsInChildren<Image>()[1].enabled = false;
            }

            //for (int i = 0; i < GameScene.Instance.strokePans.Length; i++)
            //{
            //    GameScene.Instance.strokePans[i].SetActive(false);
            //}

            for (int i = 0; i < numberTexts.Length; i++)
            {
                numberTexts[i].SetActive(false);
            }

            lines[0].SetActive(false);
            lines[1].SetActive(false);
            tennisBall[0].SetActive(false);
            tennisBall[1].SetActive(false);
            playerPans[0].SetActive(false);
            playerPans[1].SetActive(false);
            //notifications[2].SetActive(true);

            if (player1.IsMyTurn)
            {                
                coachCommandImages[0].gameObject.SetActive(true);

                if (MainMenu.isEnglish)
                {
                    coachCommandImages[0].sprite = coach1Commands[6];
                }
                else
                {
                    coachCommandImages[0].sprite = coach11Commands[6];
                }


            }
            else if (player2.IsMyTurn)
            {
                coachCommandImages[1].gameObject.SetActive(true);

                if (MainMenu.isEnglish)
                {
                    coachCommandImages[1].sprite = coach2Commands[6];
                }
                else
                {
                    coachCommandImages[1].sprite = coach22Commands[6];
                }

            }

            //StartCoroutine("ServeMessgaeShowDelay");            

            for (int i = 0; i < player1ServeTiles.Length; i++)
            {
                player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                //player1ServeTiles[i].GetComponentsInChildren<Image>()[1].enabled = false;
            }

            if (player1.IsMyServe)
            {
                if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                {
                    numberSets[0].SetActive(false);
                    numberSets[1].SetActive(true);
                }
                
                player1.IsMyTurn = true;
                player2.IsMyTurn = false;

                if (CheckServerPosition())
                {
                    if (Application.loadedLevelName == "GameRed1")
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }

                }
                else
                {
                    if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                    {
                        numberSets[0].SetActive(false);
                        numberSets[1].SetActive(true);
                    }

                    if (Application.loadedLevelName == "GameRed1")
                    {
                        for (int i = 2; i < 4; i++)
                        {
                            player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 3; i < 6; i++)
                        {
                            player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }

                }

            }
            else if (player2.IsMyServe)
            {
                if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                {
                    numberSets[0].SetActive(false);
                    numberSets[2].SetActive(true);
                }

                player1.IsMyTurn = false;
                player2.IsMyTurn = true;

                if (CheckServerPosition())
                {
                    if (Application.loadedLevelName == "GameRed1")
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            player2ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            player2ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }

                }
                else
                {
                    if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                    {
                        numberSets[0].SetActive(false);
                        numberSets[2].SetActive(true);
                    }

                    if (Application.loadedLevelName == "GameRed1")
                    {
                        for (int i = 2; i < 4; i++)
                        {
                            player2ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 3; i < 6; i++)
                        {
                            player2ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }

                }

            }

            if (MainMenu.isAI && isAIAction)
            {
                if (player2.IsMyTurn)
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        tiles[i].GetComponent<Button>().enabled = false;
                    }

                    StartCoroutine("DelayAIAction");
                }
                else
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        tiles[i].GetComponent<Button>().enabled = true;
                    }
                }
            }
        }
        else if (gameplayInfo.IsReceive && isNoRepeat)
        {            
            isNoRepeat = false;
            notifications[2].SetActive(false);
            //notifications[3].SetActive(true);
            MessgaeShowDelay();

            if (player1.IsMyTurn)
            {
                coachCommandImages[0].gameObject.SetActive(true);               

                if (MainMenu.isEnglish)
                {
                    coachCommandImages[0].sprite = coach1Commands[6];
                }
                else
                {
                    coachCommandImages[0].sprite = coach11Commands[6];
                }

            }
            else if (player2.IsMyTurn)
            {
                coachCommandImages[1].gameObject.SetActive(true);                

                if (MainMenu.isEnglish)
                {
                    coachCommandImages[1].sprite = coach2Commands[6];
                }
                else
                {
                    coachCommandImages[1].sprite = coach22Commands[6];
                }
            }

            //StartCoroutine("ReceiveMessgaeShowDelay");            

            if (player2.IsMyTurn)
            {
                if (CheckServerPosition())
                {
                    if (Application.loadedLevelName == "GameRed1")
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            player2ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            player2ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    
                }
                else
                {
                    if (Application.loadedLevelName == "GameRed1")
                    {
                        for (int i = 2; i < 4; i++)
                        {
                            player2ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 3; i < 6; i++)
                        {
                            player2ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    
                }                
            }
            else if (player1.IsMyTurn)
            {
                if (CheckServerPosition())
                {
                    if (Application.loadedLevelName == "GameRed1")
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                   
                }
                else
                {
                    if (Application.loadedLevelName == "GameRed1")
                    {
                        for (int i = 2; i < 4; i++)
                        {
                            player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 3; i < 6; i++)
                        {
                            player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                   
                }
                
            }

            if (MainMenu.isAI)
            {
                if (player2.IsMyTurn)
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        tiles[i].GetComponent<Button>().enabled = false;
                    }

                    StartCoroutine("DelayAIAction");
                    //print("Fatal Bug!!!!!!!!!!!!");
                }
                else
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        tiles[i].GetComponent<Button>().enabled = true;
                    }
                }
            }
        }
        else if (gameplayInfo.IsWhereToServe && isNoRepeat)
        {
            isNoRepeat = false;
            notifications[3].SetActive(false);
            //notifications[10].SetActive(true);
            MessgaeShowDelay();

            if (player1.IsMyTurn)
            {
                coachCommandImages[0].gameObject.SetActive(true);                

                if (MainMenu.isEnglish)
                {
                    coachCommandImages[0].sprite = coach1Commands[5];
                }
                else
                {
                    coachCommandImages[0].sprite = coach11Commands[5];
                }

            }
            else if (player2.IsMyTurn)
            {
                coachCommandImages[1].gameObject.SetActive(true);

                if (MainMenu.isEnglish)
                {
                    coachCommandImages[1].sprite = coach2Commands[5];
                }
                else
                {
                    coachCommandImages[1].sprite = coach22Commands[5];
                }

            }

            //StartCoroutine("AimShotMessgaeShowDelay");           

            if (player2.IsMyTurn)
            {
                if (Application.loadedLevelName == "GameRed1")
                {
                    if (CheckServerPosition())
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            player2whereToserveTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 6; i < 12; i++)
                        {
                            player2whereToserveTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                }
                else
                {
                    if (CheckServerPosition())
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            player2whereToserveTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 9; i < 18; i++)
                        {
                            player2whereToserveTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                }

                             
            }
            else if (player1.IsMyTurn)
            {
                if (Application.loadedLevelName == "GameRed1")
                {
                    if (CheckServerPosition())
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            player1whereToServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 6; i < 12; i++)
                        {
                            player1whereToServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                }
                else
                {
                    if (CheckServerPosition())
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            player1whereToServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 9; i < 18; i++)
                        {
                            player1whereToServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                }
               

               
            }

            if (MainMenu.isAI)
            {
                if (player2.IsMyTurn)
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        tiles[i].GetComponent<Button>().enabled = false;
                    }

                    StartCoroutine("DelayAIAction");
                }
                else
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        tiles[i].GetComponent<Button>().enabled = true;
                    }
                }
            }
        }
        else if (gameplayInfo.IsRoll && isNoRepeat)
        {
            isNoRepeat = false;
            //notifications[10].SetActive(false);
            //notifications[4].SetActive(true);
            MessgaeShowDelay();

            if (player1.IsMyTurn)
            {
                coachCommandImages[0].gameObject.SetActive(true);

                if (MainMenu.isEnglish)
                {
                    coachCommandImages[0].sprite = coach1Commands[0];
                }
                else
                {
                    coachCommandImages[0].sprite = coach11Commands[0];
                }

                rollBall[0].SetActive(true);

                if (MainMenu.isRollAnimatin)
                {
                    rollBall[0].GetComponent<Animator>().enabled = true;
                }
                
                //netGameObject[0].SetActive(true);
            }
            else if (player2.IsMyTurn)
            {
                coachCommandImages[1].gameObject.SetActive(true);

                if (MainMenu.isEnglish)
                {
                    coachCommandImages[1].sprite = coach2Commands[0];
                }
                else
                {
                    coachCommandImages[1].sprite = coach22Commands[0];
                }

                rollBall[0].SetActive(true);

                if (MainMenu.isRollAnimatin)
                {
                    rollBall[0].GetComponent<Animator>().enabled = true;
                }
                //netGameObject[1].SetActive(true);
            }

           
            StartCoroutine("RollMessgaeShowDelay"); 
        }
        else if (gameplayInfo.IsRoll && isHideMessgae)
        {
            isHideMessgae = false;           
            
            notifications[4].SetActive(false);

            gameplayInfo.IsRoll = false;

            if (rollNumber == 0)
            {
                gameplayInfo.IsHitNet = true;
                isNoRepeat = true;
                //notifications[11].SetActive(true);
                MessgaeShowDelay();                

                if (player1.IsMyTurn)
                {
                    coachCommandImages[0].gameObject.SetActive(true);

                    if (MainMenu.isEnglish)
                    {
                        coachCommandImages[0].sprite = coach1Commands[1];
                    }
                    else
                    {
                        coachCommandImages[0].sprite = coach11Commands[1];
                    }

                }
                else if (player2.IsMyTurn)
                {
                    coachCommandImages[1].gameObject.SetActive(true);

                    if (MainMenu.isEnglish)
                    {
                        coachCommandImages[1].sprite = coach2Commands[1];
                    }
                    else
                    {
                        coachCommandImages[1].sprite = coach22Commands[1];
                    }

                }

                //StartCoroutine("NetMessgaeShowDelay");
            }
            else
            {
                PlaceTennisBall(rollNumber);

                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i].SetActive(true);
                }

                if (player1.IsMyTurn)
                {
                    prevPlayerPan = playerPans[0];
                    DrawLine(lines[0], playerPans[0], tennisBall[1]);
                    DrawLine(lines[1], tennisBall[0], tennisBall[1]);
                    ShowMoveRange(player1Position);
                }
                else if (player2.IsMyTurn)
                {
                    prevPlayerPan = playerPans[1];
                    DrawLine(lines[0], playerPans[1], tennisBall[1]);
                    DrawLine(lines[1], tennisBall[0], tennisBall[1]);
                    ShowMoveRange(player2Position);
                }

                if (isServeState)
                {
                    MessgaeShowDelay();

                    if (OutOfRangeServe(numberTexts[rollNumber - 1]))
                    {
                        if (player1.IsMyTurn)
                        {
                            coachCommandImages[0].gameObject.SetActive(true);

                            if (MainMenu.isEnglish)
                            {
                                coachCommandImages[0].sprite = coach1Commands[8];
                            }
                            else
                            {
                                coachCommandImages[0].sprite = coach11Commands[8];
                            }
                        }
                        else if (player2.IsMyTurn)
                        {
                            coachCommandImages[1].gameObject.SetActive(true);

                            if (MainMenu.isEnglish)
                            {
                                coachCommandImages[1].sprite = coach2Commands[8];
                            }
                            else
                            {
                                coachCommandImages[1].sprite = coach22Commands[8];
                            }
                        }


                        if (isFirstServeFailed)
                        {
                            isFirstServeFailed = false;
                            

                            if (player1.IsMyTurn)
                            {
                                GetScore(player2);
                            }
                            else if (player2.IsMyTurn)
                            {
                                GetScore(player1);
                            }
                        }
                        else
                        {                            
                            StartCoroutine("DelayToServeFailed");                            
                        }
                    }
                    else
                    {
                        gameplayInfo.IsRecoveryMove = true;

                        if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                        {
                            ChangeZonePhase();
                        }
                            
                        //notifications[5].SetActive(true);
                        MessgaeShowDelay();
                        StartCoroutine("DelayAfterRoll1");

                        if (player1.IsMyTurn)
                        {
                            coachCommandImages[0].gameObject.SetActive(true);

                            if (MainMenu.isEnglish)
                            {
                                coachCommandImages[0].sprite = coach1Commands[4];
                            }
                            else
                            {
                                coachCommandImages[0].sprite = coach11Commands[4];
                            }

                        }
                        else if (player2.IsMyTurn)
                        {
                            coachCommandImages[1].gameObject.SetActive(true);

                            if (MainMenu.isEnglish)
                            {
                                coachCommandImages[1].sprite = coach2Commands[4];
                            }
                            else
                            {
                                coachCommandImages[1].sprite = coach22Commands[4];
                            }

                        }

                        //StartCoroutine("RecoverMessgaeShowDelay");
                    }
                }
                else
                {
                    if (OutOfRange(numberTexts[rollNumber - 1]))
                    {
                        MessgaeShowDelay();

                        if (player1.IsMyTurn)
                        {
                            coachCommandImages[0].gameObject.SetActive(true);

                            if (MainMenu.isEnglish)
                            {
                                coachCommandImages[0].sprite = coach1Commands[8];
                            }
                            else
                            {
                                coachCommandImages[0].sprite = coach11Commands[8];
                            }
                        }
                        else if (player2.IsMyTurn)
                        {
                            coachCommandImages[1].gameObject.SetActive(true);

                            if (MainMenu.isEnglish)
                            {
                                coachCommandImages[1].sprite = coach2Commands[8];
                            }
                            else
                            {
                                coachCommandImages[1].sprite = coach22Commands[8];
                            }
                        }


                        isFirstServeFailed = false;                        

                        if (player1.IsMyTurn)
                        {
                            GetScore(player2);
                        }
                        else if (player2.IsMyTurn)
                        {
                            GetScore(player1);
                        }
                    }
                    else
                    {
                        gameplayInfo.IsRecoveryMove = true;

                        if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                        {
                            ChangeZonePhase();
                        }
                            
                        //notifications[5].SetActive(true);
                        MessgaeShowDelay();
                        StartCoroutine("DelayAfterRoll1");

                        if (player1.IsMyTurn)
                        {
                            coachCommandImages[0].gameObject.SetActive(true);

                            if (MainMenu.isEnglish)
                            {
                                coachCommandImages[0].sprite = coach1Commands[4];
                            }
                            else
                            {
                                coachCommandImages[0].sprite = coach11Commands[4];
                            }

                        }
                        else if (player2.IsMyTurn)
                        {
                            coachCommandImages[1].gameObject.SetActive(true);

                            if (MainMenu.isEnglish)
                            {
                                coachCommandImages[1].sprite = coach2Commands[4];
                            }
                            else
                            {
                                coachCommandImages[1].sprite = coach22Commands[4];
                            }

                        }

                        //RecoverMessgaeShowDelay();
                        //StartCoroutine("RecoverMessgaeShowDelay");
                    }
                }
                   
            }

            //if (MainMenu.isAI)
            //{
            //    if (player2.IsMyTurn)
            //    {
            //        for (int i = 0; i < tiles.Length; i++)
            //        {
            //            tiles[i].GetComponent<Button>().enabled = false;
            //        }

            //        StartCoroutine("DelayAIAction");
            //    }
            //    else
            //    {
            //        for (int i = 0; i < tiles.Length; i++)
            //        {
            //            tiles[i].GetComponent<Button>().enabled = true;
            //        }
            //    }
            //}
        }
        else if (gameplayInfo.IsHitNet && isNoRepeat)
        {
            isNoRepeat = false;
            notifications[11].SetActive(false);
            gameplayInfo.IsHitNet = false;
            ShowRollNumber();

            if (isServeState)
            {
                if (isFirstServeFailed)
                {
                    isFirstServeFailed = false;                   

                    if (player1.IsMyTurn)
                    {
                        GetScore(player2);
                    }
                    else if (player2.IsMyTurn)
                    {
                        GetScore(player1);
                    }
                }
                else
                {
                    isFirstServeFailed = true;
                    gameplayInfo.IsServe = true;
                    isNoRepeat = true;
                }
            }
            else
            {
                isFirstServeFailed = false;                

                if (player1.IsMyTurn)
                {
                    GetScore(player2);
                }
                else if (player2.IsMyTurn)
                {
                    GetScore(player1);
                }
            }

            //if (MainMenu.isAI)
            //{
            //    if (player2.IsMyTurn)
            //    {
            //        for (int i = 0; i < tiles.Length; i++)
            //        {
            //            tiles[i].GetComponent<Button>().enabled = false;
            //        }

            //        StartCoroutine("DelayAIAction");
            //    }
            //    else
            //    {
            //        for (int i = 0; i < tiles.Length; i++)
            //        {
            //            tiles[i].GetComponent<Button>().enabled = true;
            //        }
            //    }
            //}
        }
        else if (gameplayInfo.IsMoveToStrike && isNoRepeat)
        {           
            isNoRepeat = false;
            isServeState = false;
            notifications[5].SetActive(false);
            //notifications[9].SetActive(true);                      
            MessgaeShowDelay();

            if (player1.IsMyTurn)
            {
                ShowMoveRange(player1Position);                

                coachCommandImages[0].gameObject.SetActive(true);

                if (MainMenu.isEnglish)
                {
                    coachCommandImages[0].sprite = coach1Commands[3];
                }
                else
                {
                    coachCommandImages[0].sprite = coach11Commands[3];
                }
            }
            else if (player2.IsMyTurn)
            {
                ShowMoveRange(player2Position);
                
                coachCommandImages[1].gameObject.SetActive(true);

                if (MainMenu.isEnglish)
                {
                    coachCommandImages[1].sprite = coach2Commands[3];
                }
                else
                {
                    coachCommandImages[1].sprite = coach22Commands[3];
                }

            }

            ShowMoveToStrike(tennisBall[0]);

            if (MainMenu.isAI)
            {
                if (player2.IsMyTurn)
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        tiles[i].GetComponent<Button>().enabled = false;
                    }

                    StartCoroutine("DelayAIAction");
                }
                else
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        tiles[i].GetComponent<Button>().enabled = true;
                    }
                }
            }
        }        
        else if (gameplayInfo.IsWhereToStrike && isNoRepeat)
        {
            CheckStrokeStates();

            isNoRepeat = false;
            MessgaeShowDelay();            

            if (player1.IsMyTurn)
            {                
                coachCommandImages[0].gameObject.SetActive(true);
                GameScene.Instance.strokePans[0].SetActive(true);
                GameScene.Instance.strokePans[1].SetActive(true);
                GameScene.Instance.bonusPans[1].SetActive(true);
                GameScene.Instance.bonusPans[0].SetActive(true);

                if (MainMenu.isEnglish)
                {
                    GameScene.Instance.strokeButton[1].sprite = GameScene.Instance.oStrokeEngButton[1];
                }
                else if (MainMenu.isSweeden)
                {
                    GameScene.Instance.strokeButton[1].sprite = GameScene.Instance.oStrokeSwedButton[1];
                }
                //GameScene.Instance.strokePans[1].SetActive(false);

                if (MainMenu.isEnglish)
                {
                    coachCommandImages[0].sprite = coach1Commands[2];
                }
                else
                {
                    coachCommandImages[0].sprite = coach11Commands[2];
                }
            }
            else if (player2.IsMyTurn)
            {                
                coachCommandImages[1].gameObject.SetActive(true);
                GameScene.Instance.strokePans[1].SetActive(true);
                GameScene.Instance.strokePans[0].SetActive(true);
                GameScene.Instance.bonusPans[1].SetActive(true);
                GameScene.Instance.bonusPans[0].SetActive(true);
                //GameScene.Instance.strokePans[0].SetActive(false);

                if (MainMenu.isEnglish)
                {
                    GameScene.Instance.strokeButton[0].sprite = GameScene.Instance.oStrokeEngButton[0];
                }
                else if (MainMenu.isSweeden)
                {
                    GameScene.Instance.strokeButton[0].sprite = GameScene.Instance.oStrokeSwedButton[0];
                }

                if (MainMenu.isEnglish)
                {
                    coachCommandImages[1].sprite = coach2Commands[2];
                }
                else
                {
                    coachCommandImages[1].sprite = coach22Commands[2];
                }
            }

            //StartCoroutine("HitMessgaeShowDelay");


            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                tiles[i].GetComponentsInChildren<Image>()[1].enabled = false;
            }

            if (GameEngine.Instance.player1.IsMyTurn)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    int x = (i + 1) / dividedRowNumber;
                    int y = (i + 1) % dividedRowNumber;

                    if (y == 0)
                    {
                        y = dividedRowNumber;
                        x--;
                    }

                    if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                    {
                        if (x >= 3 && x <= 8)
                        {
                            if (y >= 16 && y <= 22)
                            {
                                tiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                            }
                        }
                    }
                    else if (Application.loadedLevelName == "GameOrange1")
                    {
                        if (x >= 2 && x <= 6)
                        {
                            if (y >= 13 && y <= 18)
                            {
                                tiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                            }
                        }
                    }
                    else if (Application.loadedLevelName == "GameRed1")
                    {
                        if (x >= 2 && x <= 5)
                        {
                            if (y >= 12 && y <= 16)
                            {
                                tiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                            }
                        }
                    }


                }
            }
            else if (GameEngine.Instance.player2.IsMyTurn)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    int x = (i + 1) / dividedRowNumber;
                    int y = (i + 1) % dividedRowNumber;

                    if (y == 0)
                    {
                        y = dividedRowNumber;
                        x--;
                    }


                    if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                    {
                        if (x >= 3 && x <= 8)
                        {
                            if (y >= 5 && y <= 11)
                            {
                                tiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                            }
                        }
                    }
                    else if (Application.loadedLevelName == "GameOrange1")
                    {
                        if (x >= 2 && x <= 6)
                        {
                            if (y >= 3 && y <= 8)
                            {
                                tiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                            }
                        }
                    }
                    else if (Application.loadedLevelName == "GameRed1")
                    {
                        if (x >= 2 && x <= 5)
                        {
                            if (y >= 3 && y <= 7)
                            {
                                tiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                            }
                        }
                    }
                }                            
            }

            if (MainMenu.isAI)
            {
                if (player2.IsMyTurn)
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        tiles[i].GetComponent<Button>().enabled = false;
                    }

                    StartCoroutine("DelayAIAction");
                }
                else
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        tiles[i].GetComponent<Button>().enabled = true;
                    }
                }
            }
        }
    }

    public void MessgaeShowDelay()
    {        
        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator DelayAIAction()
    {
        yield return new WaitForSeconds(2);

        AIEngine.Instance.AIClick();        
    }

    IEnumerator ReceiveMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);

        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator AimShotMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);

        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator RollMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);
        if (isTopSpin)
        {
            rollNumber = UnityEngine.Random.RandomRange(1, 10);
        }
        else if (bonusButton1.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[5] || GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[5]))
        {
            if (Application.loadedLevelName == "GameRed1")
            {
                rollNumber = UnityEngine.Random.RandomRange(1, 7);
            }
            else
            {
                rollNumber = UnityEngine.Random.RandomRange(1, 10);
            }
        }
        else if (bonusButton0.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[5] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[5]))
        {
            if (Application.loadedLevelName == "GameRed1")
            {
                rollNumber = UnityEngine.Random.RandomRange(1, 7);
            }
            else
            {
                rollNumber = UnityEngine.Random.RandomRange(1, 10);
            }
        }
        else
        {
            if (Application.loadedLevelName == "GameRed1")
            {
                rollNumber = UnityEngine.Random.RandomRange(0, 7);
            }
            else
            {
                rollNumber = UnityEngine.Random.RandomRange(0, 10);
            }

        }        
        
        
        if (player1.IsMyTurn)
        {
            rollBall[0].GetComponent<Animator>().enabled = false;

            if (rollNumber == 0)
            {
                netGameObject[0].SetActive(true);
            }
            else
            {
                rollText[0].GetComponent<Text>().text = "" + rollNumber;
                rollText[0].SetActive(true);
                
            }

        }
        else if (player2.IsMyTurn)
        {
            rollBall[0].GetComponent<Animator>().enabled = false;

            if (rollNumber == 0)
            {
                netGameObject[0].SetActive(true);
            }
            else
            {
                rollText[0].SetActive(true);
                rollText[0].GetComponent<Text>().text = "" + rollNumber;
                
            }
        }
        StartCoroutine("DelayAfterRoll");
    }

    IEnumerator DelayAfterRoll()
    {
        yield return new WaitForSeconds(2);

        isHideMessgae = true;

        //if (MainMenu.isAI)
        //{
        //    if (player2.IsMyTurn)
        //    {
        //        for (int i = 0; i < tiles.Length; i++)
        //        {
        //            tiles[i].GetComponent<Button>().enabled = false;
        //        }

        //        StartCoroutine("DelayAIAction");
        //    }
        //    else
        //    {
        //        for (int i = 0; i < tiles.Length; i++)
        //        {
        //            tiles[i].GetComponent<Button>().enabled = true;
        //        }
        //    }
        //}
    }

    IEnumerator DelayAfterRoll1()
    {
        yield return new WaitForSeconds(2);



        if (MainMenu.isAI)
        {
            if (player2.IsMyTurn)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    tiles[i].GetComponent<Button>().enabled = false;
                }

                StartCoroutine("DelayAIAction");
            }
            else
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    tiles[i].GetComponent<Button>().enabled = true;
                }
            }
        }
    }

    IEnumerator NetMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);

        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator RecoverMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);

        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator MoveMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);

        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator HitMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);

        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator ShowGamePlayMessage()
    {
        yield return new WaitForSeconds(3);

        if (OutOfRange(numberTexts[rollNumber - 1]))
        {
            FormatBoolFlag();
            FormatBoolFlag1();
            gameplayInfo.IsStart = true;
            characterCards.SetActive(false);
            isAIAction = false;
            isNoRepeat = true;
            notifications[6].SetActive(false);
            notifications[0].SetActive(true);
            panel.SetActive(true);
            tacticalCards1.SetActive(true);

        }        
    }

    public void ChangeZonePhase()
    {
        numberSets[0].SetActive(true);
        numberSets[1].SetActive(false);
        numberSets[2].SetActive(false);
    }

    public void ShowRollNumber()
    {        
        rollText[0].SetActive(false);
        rollText[1].SetActive(false);
        rollBall[0].SetActive(false);
        rollBall[0].SetActive(false);
        netGameObject[0].SetActive(false);
        netGameObject[1].SetActive(false);
    }

    public void Player2TacticCardClick()
    {
        gameplayInfo.IsServe = true;
        isNoRepeat = true;
        isAIAction = true;
    }

    public void Player1TacticCardClick()
    {
        if (MainMenu.isAI)
        {
            for (int i = 0; i < tacticCards2.Length; i++)
            {
                tacticCards2[i].GetComponent<Button>().enabled = false;
            }
            
            int randomNumber = UnityEngine.Random.RandomRange(0, 5);

            switch (randomNumber)
            {                
                case 0:
                    GameEngine.Instance.playerAvatars[1].sprite = GameEngine.Instance.tacticCardSprites[5];
                    GameEngine.Instance.isPlayer2SetNet = true;

                    if (MainMenu.isEnglish)
                    {
                        GameEngine.Instance.playerAvatars[1].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleEngStrings[0];
                    }
                    else
                    {
                        GameEngine.Instance.playerAvatars[1].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleSwedStrings[0];
                    }

                    break;
                case 1:
                    GameEngine.Instance.playerAvatars[1].sprite = GameEngine.Instance.tacticCardSprites[6];
                    GameEngine.Instance.isPlayer2SetDefensive = true;

                    if (MainMenu.isEnglish)
                    {
                        GameEngine.Instance.playerAvatars[1].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleEngStrings[1];
                    }
                    else
                    {
                        GameEngine.Instance.playerAvatars[1].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleSwedStrings[1];
                    }

                    break;
                case 2:
                    GameEngine.Instance.playerAvatars[1].sprite = GameEngine.Instance.tacticCardSprites[7];
                    GameEngine.Instance.isPlayer2SetAccurate = true;

                    if (MainMenu.isEnglish)
                    {
                        GameEngine.Instance.playerAvatars[1].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleEngStrings[2];
                    }
                    else
                    {
                        GameEngine.Instance.playerAvatars[1].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleSwedStrings[2];
                    }

                    break;
                case 3:
                    GameEngine.Instance.playerAvatars[1].sprite = GameEngine.Instance.tacticCardSprites[8];
                    GameEngine.Instance.isPlayer2SetOffensive = true;

                    if (MainMenu.isEnglish)
                    {
                        GameEngine.Instance.playerAvatars[1].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleEngStrings[3];
                    }
                    else
                    {
                        GameEngine.Instance.playerAvatars[1].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleSwedStrings[3];
                    }

                    break;
                case 4:
                    GameEngine.Instance.playerAvatars[1].sprite = GameEngine.Instance.tacticCardSprites[9];
                    GameEngine.Instance.isPlayer2SetSmart = true;

                    if (MainMenu.isEnglish)
                    {
                        GameEngine.Instance.playerAvatars[1].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleEngStrings[4];
                    }
                    else
                    {
                        GameEngine.Instance.playerAvatars[1].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleSwedStrings[4];
                    }
                    break;
            }

            StartCoroutine("DelayTacticCard");
        }
    }

    public void ShowNumbers(GameObject tileObject)
    {       
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] == tileObject)
            {
                columnNumber = (i + 1) / dividedRowNumber;
                rowNumber = (i + 1) % dividedRowNumber;

                if (rowNumber == 0)
                {
                    rowNumber = dividedRowNumber;
                    columnNumber--;
                }
            }
        }

        if (Application.loadedLevelName == "GameRed1")
        {
            for (int i = 0; i < 6; i++)
            {
                numberTexts[i].SetActive(true);
                numberTexts[i].transform.position = tiles[(columnNumber - 1 + (i / 2)) * dividedRowNumber + rowNumber - 2 + (i % 2)].transform.position;


                if (player2.IsMyTurn)
                {
                    player2whereToserveTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                }
                else if (player1.IsMyTurn)
                {
                    player1whereToServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                numberTexts[i].SetActive(true);
                numberTexts[i].transform.position = tiles[(columnNumber - 1 + (i / 3)) * dividedRowNumber + rowNumber - 2 + (i % 3)].transform.position;


                if (player2.IsMyTurn)
                {
                    player2whereToserveTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                }
                else if (player1.IsMyTurn)
                {
                    player1whereToServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                }
            }
        }          
    }

    public void PlaceTennisBall(int index)
    {
        for (int i = 0; i < 9; i++)
        {
            numberTexts[i].SetActive(false);
        }
        
        tennisBall[1].SetActive(true);

        if (Application.loadedLevelName == "GameRed1")
        {
            tennisBall[1].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 2)) * dividedRowNumber + rowNumber - 2 + ((index - 1) % 2)].transform.position;
        }
        else
        {
            tennisBall[1].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3)) * dividedRowNumber + rowNumber - 2 + ((index - 1) % 3)].transform.position;
        }
                
        if (player1.IsMyTurn)
        {
            ExtraDistance(playerPans[0],tennisBall[1]);

            tennisBall[0].SetActive(true);
            //print("Player1's turn!!!");

            if (x2 == 0 || x2 == dividedColumnNumber - 1)
            {
                //print(1);
                if (y2 + step >= dividedRowNumber)
                {
                    tennisBall[0].transform.position = tiles[(x2 + 1) * dividedRowNumber - 1].transform.position;
                }
                else
                {
                    tennisBall[0].transform.position = tiles[(x2) * dividedRowNumber + step + y2 - 1].transform.position;
                }
            }
            else
            {
                if (x1 > x2)
                {
                    //print(11);
                    if (y2 + step >= dividedRowNumber)
                    {
                        tennisBall[0].transform.position = tiles[(x2) * dividedRowNumber - 1].transform.position;
                    }
                    else
                    {
                        tennisBall[0].transform.position = tiles[(x2 - 1) * dividedRowNumber + step + y2 - 1].transform.position;
                    }
                }
                else if (x1 == x2)
                {
                    //print(111);
                    if (y2 + step >= dividedRowNumber)
                    {
                        tennisBall[0].transform.position = tiles[(x2 + 1) * dividedRowNumber - 1].transform.position;
                    }
                    else
                    {
                        tennisBall[0].transform.position = tiles[(x2) * dividedRowNumber + step + y2 - 1].transform.position;
                    }
                }
                else
                {
                    //print(1111);
                    if (y2 + step >= dividedRowNumber)
                    {
                        tennisBall[0].transform.position = tiles[(x2 + 2) * dividedRowNumber - 1].transform.position;
                    }
                    else
                    {
                        tennisBall[0].transform.position = tiles[(x2 + 1) * dividedRowNumber + step + y2 - 1].transform.position;
                    }
                }
            }
        }
        else if (player2.IsMyTurn)
        {
            ExtraDistance(playerPans[1], tennisBall[1]);
            //print("Player2's turn");
            tennisBall[0].SetActive(true);

            if (x2 == 0 || x2 == dividedColumnNumber - 1)
            {
                //print(2);
                if (y2 - step <= 0)
                {
                    tennisBall[0].transform.position = tiles[(x2) * dividedRowNumber].transform.position;
                }
                else
                {
                    tennisBall[0].transform.position = tiles[(x2) * dividedRowNumber - step + y2 - 1].transform.position;
                }
            }
            else
            {
                if (x1 > x2)
                {
                    //print(22);
                    if (y2 - step <= 0)
                    {
                       // print(3);
                        tennisBall[0].transform.position = tiles[(x2 -1 ) * dividedRowNumber].transform.position;
                    }
                    else
                    {
                       // print(33);
                        tennisBall[0].transform.position = tiles[(x2 - 1) * dividedRowNumber - step + y2 - 1].transform.position;
                    }
                }
                else if (x1 == x2)
                {
                   // print(222);
                    if (y2 - step <= 0)
                    {
                        //print(4);
                        tennisBall[0].transform.position = tiles[(x2) * dividedRowNumber].transform.position;
                    }
                    else
                    {
                       // print(44);
                        tennisBall[0].transform.position = tiles[(x2) * dividedRowNumber - step + y2 - 1].transform.position;
                    }
                }
                else
                {
                    //print(2222);
                    if (y2 - step <= 0)
                    {
                        //print(5);
                        tennisBall[0].transform.position = tiles[(x2 + 1) * dividedRowNumber].transform.position;
                    }
                    else
                    {
                        //print(55);
                        tennisBall[0].transform.position = tiles[(x2 + 1) * dividedRowNumber - step + y2 - 1].transform.position;
                    }
                }
            }
        }
    }

    public void ExtraDistance(GameObject startObject, GameObject endObject)
    {
        x1 = 0; x2 = 0; y1 = 0;y2 = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            
            if (Vector3.Distance(tiles[i].transform.position, startObject.transform.position) < 0.5f)
            {
                x1 = (i + 1) / dividedRowNumber;
                y1 = (i + 1) % dividedRowNumber;

                if (y1 == 0)
                {
                    x1--;
                    y1 = dividedRowNumber;
                }
            }

            if (Vector3.Distance(tiles[i].transform.position, endObject.transform.position) < 0.5f)
            {
                x2 = (i + 1) / dividedRowNumber;
                y2 = (i + 1) % dividedRowNumber;

                if (y2 == 0)
                {
                    x2--;
                    y2 = dividedRowNumber;
                }


                if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                {
                    if ((y2 >= 0 && y2 <= 4) || (y2 >= 23 && y2 <= 26))
                    {
                        step = 6;
                    }
                    else if ((y2 >= 5 && y2 <= 7) || (y2 >= 20 && y2 <= 22))
                    {
                        step = 5;
                    }
                    else if ((y2 >= 8 && y2 <= 10) || (y2 >= 17 && y2 <= 19))
                    {
                        step = 4;
                    }
                    else if ((y2 >= 11 && y2 <= 16))
                    {
                        step = 3;
                    }
                }
                else if (Application.loadedLevelName == "GameOrange1")
                {
                    if ((y2 >= 0 && y2 <= 2) || (y2 >= 19 && y2 <= 20))
                    {
                        step = 5;
                    }
                    else if ((y2 >= 3 && y2 <= 5) || (y2 >= 16 && y2 <= 18))
                    {
                        step = 4;
                    }
                    else if ((y2 >= 6 && y2 <= 15))
                    {
                        step = 3;
                    }                    
                }
                else if (Application.loadedLevelName == "GameRed1")
                {
                    if ((y2 >= 0 && y2 <= 3) || (y2 >= 16 && y2 <= 18))
                    {
                        step = 4;
                    }
                    else if ((y2 >= 4 && y2 <= 7) || (y2 >= 12 && y2 <= 15))
                    {
                        step = 3;
                    }
                    else if ((y2 >= 8 && y2 <= 11))
                    {
                        step = 2;
                    }
                }



                if (isTopSpin)
                {
                    step++;
                }
                else if (isSliceShot)
                {
                    step--;
                }
                else if (isDropShot)
                {
                    step = 1;
                }
            }            
        }
    }

    public void ShowMoveRange(GameObject originObject)
    {        
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].transform.position == originObject.transform.position)
            {
                x = (i + 1) / dividedRowNumber;
                y = (i + 1) % dividedRowNumber;

               

                if (y == 0)
                {
                    x--;
                    y = dividedRowNumber;
                }

                if (player1.IsMyTurn && bonusButton0.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[0] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[0] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[3] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[3]))
                {
                    if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                    {
                        if ((y >= 0 && y <= 4) || (y >= 23 && y <= 26))
                        {
                            maxMove = 4;
                        }
                        else if ((y >= 5 && y <= 7) || (y >= 20 && y <= 22))
                        {
                            maxMove = 3;
                        }
                        else if ((y >= 8 && y <= 10) || (y >= 17 && y <= 19))
                        {
                            maxMove = 2;
                        }
                        else if ((y >= 11 && y <= 16))
                        {
                            maxMove = 1;
                        }
                    }
                    else if (Application.loadedLevelName == "GameOrange1")
                    {
                        if ((y >= 0 && y <= 2) || (y >= 19 && y <= 20))
                        {
                            maxMove = 3;
                        }
                        else if ((y >= 3 && y <= 5) || (y >= 16 && y <= 18))
                        {
                            maxMove = 2;
                        }
                        else if ((y >= 6 && y <= 15))
                        {
                            maxMove = 1;
                        }
                    }
                    else if (Application.loadedLevelName == "GameRed1")
                    {
                        if ((y >= 0 && y <= 3) || (y >= 16 && y <= 18))
                        {
                            maxMove = 2;
                        }
                        else if ((y >= 4 && y <= 7) || (y >= 12 && y <= 15))
                        {
                            maxMove = 1;
                        }
                        else if ((y >= 8 && y <= 11))
                        {
                            maxMove = 0;
                        }
                    }
                }
                else if (player2.IsMyTurn && bonusButton1.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[0] || GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[0] || GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[3] || GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[3]))
                {
                    if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                    {
                        if ((y >= 0 && y <= 4) || (y >= 23 && y <= 26))
                        {
                            maxMove = 4;
                        }
                        else if ((y >= 5 && y <= 7) || (y >= 20 && y <= 22))
                        {
                            maxMove = 3;
                        }
                        else if ((y >= 8 && y <= 10) || (y >= 17 && y <= 19))
                        {
                            maxMove = 2;
                        }
                        else if ((y >= 11 && y <= 16))
                        {
                            maxMove = 1;
                        }
                    }
                    else if (Application.loadedLevelName == "GameOrange1")
                    {
                        if ((y >= 0 && y <= 2) || (y >= 19 && y <= 20))
                        {
                            maxMove = 3;
                        }
                        else if ((y >= 3 && y <= 5) || (y >= 16 && y <= 18))
                        {
                            maxMove = 2;
                        }
                        else if ((y >= 6 && y <= 15))
                        {
                            maxMove = 1;
                        }
                    }
                    else if (Application.loadedLevelName == "GameRed1")
                    {
                        if ((y >= 0 && y <= 3) || (y >= 16 && y <= 18))
                        {
                            maxMove = 2;
                        }
                        else if ((y >= 4 && y <= 7) || (y >= 12 && y <= 15))
                        {
                            maxMove = 1;
                        }
                        else if ((y >= 8 && y <= 11))
                        {
                            maxMove = 0;
                        }
                    }
                }
                if (player1.IsMyTurn && bonusButton0.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[7] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[7]))
                {
                    if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                    {
                        if ((y >= 0 && y <= 4) || (y >= 23 && y <= 26))
                        {
                            maxMove = 6;
                        }
                        else if ((y >= 5 && y <= 7) || (y >= 20 && y <= 22))
                        {
                            maxMove = 5;
                        }
                        else if ((y >= 8 && y <= 10) || (y >= 17 && y <= 19))
                        {
                            maxMove = 4;
                        }
                        else if ((y >= 11 && y <= 16))
                        {
                            maxMove = 3;
                        }
                    }
                    else if (Application.loadedLevelName == "GameOrange1")
                    {
                        if ((y >= 0 && y <= 2) || (y >= 19 && y <= 20))
                        {
                            maxMove = 5;
                        }
                        else if ((y >= 3 && y <= 5) || (y >= 16 && y <= 18))
                        {
                            maxMove = 4;
                        }
                        else if ((y >= 6 && y <= 15))
                        {
                            maxMove = 3;
                        }
                    }
                    else if (Application.loadedLevelName == "GameRed1")
                    {
                        if ((y >= 0 && y <= 3) || (y >= 16 && y <= 18))
                        {
                            maxMove = 4;
                        }
                        else if ((y >= 4 && y <= 7) || (y >= 12 && y <= 15))
                        {
                            maxMove = 3;
                        }
                        else if ((y >= 8 && y <= 11))
                        {
                            maxMove = 2;
                        }
                    }
                }
                else if (player2.IsMyTurn && bonusButton1.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[7] || GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[7]))
                {
                    if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                    {
                        if ((y >= 0 && y <= 4) || (y >= 23 && y <= 26))
                        {
                            maxMove = 6;
                        }
                        else if ((y >= 5 && y <= 7) || (y >= 20 && y <= 22))
                        {
                            maxMove = 5;
                        }
                        else if ((y >= 8 && y <= 10) || (y >= 17 && y <= 19))
                        {
                            maxMove = 4;
                        }
                        else if ((y >= 11 && y <= 16))
                        {
                            maxMove = 3;
                        }
                    }
                    else if (Application.loadedLevelName == "GameOrange1")
                    {
                        if ((y >= 0 && y <= 2) || (y >= 19 && y <= 20))
                        {
                            maxMove = 5;
                        }
                        else if ((y >= 3 && y <= 5) || (y >= 16 && y <= 18))
                        {
                            maxMove = 4;
                        }
                        else if ((y >= 6 && y <= 15))
                        {
                            maxMove = 3;
                        }
                    }
                    else if (Application.loadedLevelName == "GameRed1")
                    {
                        if ((y >= 0 && y <= 3) || (y >= 16 && y <= 18))
                        {
                            maxMove = 4;
                        }
                        else if ((y >= 4 && y <= 7) || (y >= 12 && y <= 15))
                        {
                            maxMove = 3;
                        }
                        else if ((y >= 8 && y <= 11))
                        {
                            maxMove = 2;
                        }
                    }
                }
                else
                {
                    if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                    {
                        if ((y >= 0 && y <= 4) || (y >= 23 && y <= 26))
                        {
                            maxMove = 5;
                        }
                        else if ((y >= 5 && y <= 7) || (y >= 20 && y <= 22))
                        {
                            maxMove = 4;
                        }
                        else if ((y >= 8 && y <= 10) || (y >= 17 && y <= 19))
                        {
                            maxMove = 3;
                        }
                        else if ((y >= 11 && y <= 16))
                        {
                            maxMove = 2;
                        }
                    }
                    else if (Application.loadedLevelName == "GameOrange1")
                    {
                        if ((y >= 0 && y <= 2) || (y >= 19 && y <= 20))
                        {
                            maxMove = 4;
                        }
                        else if ((y >= 3 && y <= 5) || (y >= 16 && y <= 18))
                        {
                            maxMove = 3;
                        }
                        else if ((y >= 6 && y <= 15))
                        {
                            maxMove = 2;
                        }
                    }
                    else if (Application.loadedLevelName == "GameRed1")
                    {
                        if ((y >= 0 && y <= 3) || (y >= 16 && y <= 18))
                        {
                            maxMove = 3;
                        }
                        else if ((y >= 4 && y <= 7) || (y >= 12 && y <= 15))
                        {
                            maxMove = 2;
                        }
                        else if ((y >= 8 && y <= 11))
                        {
                            maxMove = 1;
                        }
                    }
                }                                

                if (player1.IsMyTurn)
                {                    
                    maxMove = maxMove + player1.BonusMove;
                }
                else
                {                    
                    maxMove = maxMove + player2.BonusMove;
                }
                          
            }
        }

        enabledHitTiles = new List<GameObject>();

        for (int i = x - maxMove; i <= x + maxMove; i++)
        {
            if (i >= 0 && i < dividedColumnNumber)
            {
                for(int k = 0; k <= maxMove ;k++)
                {
                    if (x - i == maxMove - k || i - x == maxMove - k)
                    {
                        for (int j = 0; j < 2 * k + 1; j++)
                        {
                            if (y - k + j > 0 && y - k + j < dividedRowNumber + 1)
                            {
                                tiles[i * dividedRowNumber + y - 1 - k + j].GetComponentsInChildren<Image>()[0].enabled = true;
                                enabledHitTiles.Add(tiles[i * dividedRowNumber + y - 1 - k + j]);
                            }
                        }                        
                    }

                }              
            }            
        }
    }

    public void DrawLine(GameObject line, GameObject startObject, GameObject endObject)
    {
       
        line.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        line.transform.position = new Vector2((endObject.transform.position.x + startObject.transform.position.x) / 2, (endObject.transform.position.y + startObject.transform.position.y) / 2);
        line.GetComponent<RectTransform>().sizeDelta = new Vector2(Vector2.Distance(startObject.transform.position, endObject.transform.position), line.GetComponent<RectTransform>().sizeDelta.y);        
        //line.GetComponent<RectTransform>().Rotate(0, 0, Mathf.Atan((endObject.transform.position.y - startObject.transform.position.y) / (endObject.transform.position.x - startObject.transform.position.x)) * 180f / Mathf.PI);
        line.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Mathf.Atan((endObject.transform.position.y - startObject.transform.position.y) / (endObject.transform.position.x - startObject.transform.position.x)) * 180f / Mathf.PI);
        //line.GetComponent<MeshCollider>().size = new Vector2(Vector2.Distance(startObject.transform.position, endObject.transform.position), line.GetComponent<RectTransform>().sizeDelta.y);
    }


    public void ShowMoveToStrike(GameObject tileObject)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].transform.position == tileObject.transform.position)
            {
                x = (i + 1) / dividedRowNumber;
                y = (i + 1) % dividedRowNumber;

                if (y == 0)
                {
                    y = dividedRowNumber;
                    x--;
                }
            }
        }

        //for (int i = x - 1; i <= x + 1; i++)
        //{
        //    for (int j = y - 1; j <= y + 1; j++)
        //    {
        //        if ((i == x - 1 || i == x + 1) && j == y)
        //        {
        //            if (tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[0].enabled)
        //            {
        //                tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[0].enabled = false;
        //                tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[1].enabled = true;
        //                isNoMove = true;
        //            }
        //        }
        //        else if (i == x)
        //        {
        //            if (tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[0].enabled)
        //            {
        //                tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[0].enabled = false;
        //                tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[1].enabled = true;
        //                isNoMove = true;
        //            }
        //        }

        //    }
        //}    

        if (isLobShot)
        {
            for (int i = 0; i < enabledHitTiles.Count; i++)
            {
                if (IsBoundsIntersect(enabledHitTiles[i], tennisBall[0], tennisBall[1]))
                {
                    //print(tiles[i].gameObject.name);
                    if (enabledHitTiles[i].GetComponentsInChildren<Image>()[0].enabled)
                    {
                        enabledHitTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                        enabledHitTiles[i].GetComponentsInChildren<Image>()[1].enabled = true;
                        //isNoMove = true;
                    }
                }
            }
        }
        else
        {            
            //for (int i = x - 1; i <= x + 1; i++)
            //{
            //    for (int j = y - 1; j <= y + 1; j++)
            //    {
            //        if (Application.loadedLevelName == "GameRed1")
            //        {
            //            if (i >= 0 && i <= 7 && j >= 0 && j <= 18)
            //            {
            //                if (tiles[(i) * dividedRowNumber + j - 1].GetComponentsInChildren<Image>()[0].enabled)
            //                {
            //                    tiles[(i) * dividedRowNumber + j - 1].GetComponentsInChildren<Image>()[0].enabled = false;
            //                    tiles[(i) * dividedRowNumber + j - 1].GetComponentsInChildren<Image>()[1].enabled = true;
            //                    isNoMove = true;
            //                }
            //            }
            //        }
            //        else if (Application.loadedLevelName == "GameOrange1")
            //        {
            //            if (i >= 0 && i <= 7 && j >= 0 && j <= 20)
            //            {
            //                if (tiles[(i) * dividedRowNumber + j - 1].GetComponentsInChildren<Image>()[0].enabled)
            //                {
            //                    tiles[(i) * dividedRowNumber + j - 1].GetComponentsInChildren<Image>()[0].enabled = false;
            //                    tiles[(i) * dividedRowNumber + j - 1].GetComponentsInChildren<Image>()[1].enabled = true;
            //                    isNoMove = true;
            //                }
            //            }                                                  
            //        }
            //        else
            //        {
            //            if (i >= 0 && i <= 11 && j >= 0 && j <= 26)
            //            {
            //                if (tiles[(i) * dividedRowNumber + j - 1].GetComponentsInChildren<Image>()[0].enabled)
            //                {
            //                    tiles[(i) * dividedRowNumber + j - 1].GetComponentsInChildren<Image>()[0].enabled = false;
            //                    tiles[(i) * dividedRowNumber + j - 1].GetComponentsInChildren<Image>()[1].enabled = true;
            //                    isNoMove = true;
            //                }
            //            }
            //        }                   
            //    }
            //}

            if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
            {
                for (int i = 0; i < enabledHitTiles.Count; i++)
                {
                    if (IsBoundsIntersect(enabledHitTiles[i], prevPlayerPan, tennisBall[1]))
                    {
                        //print(tiles[i].gameObject.name);
                        if (enabledHitTiles[i].GetComponentsInChildren<Image>()[0].enabled)
                        {
                            enabledHitTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                            enabledHitTiles[i].GetComponentsInChildren<Image>()[1].enabled = true;
                           // isNoMove = true;
                        }
                    }
                }

                for (int i = 0; i < enabledHitTiles.Count; i++)
                {
                    if (IsBoundsIntersect(enabledHitTiles[i], tennisBall[0], tennisBall[1]))
                    {
                        //print(tiles[i].gameObject.name);
                        if (enabledHitTiles[i].GetComponentsInChildren<Image>()[0].enabled)
                        {
                            enabledHitTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                            enabledHitTiles[i].GetComponentsInChildren<Image>()[1].enabled = true;
                            //isNoMove = true;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < enabledHitTiles.Count; i++)
                {
                    if (IsBoundsIntersect(enabledHitTiles[i], prevPlayerPan, tennisBall[1]))
                    {
                        //print(tiles[i].gameObject.name);
                        if (enabledHitTiles[i].GetComponentsInChildren<Image>()[0].enabled)
                        {
                            enabledHitTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                            enabledHitTiles[i].GetComponentsInChildren<Image>()[1].enabled = true;
                            //isNoMove = true;
                        }
                    }
                }

                for (int i = 0; i < enabledHitTiles.Count; i++)
                {
                    if (IsBoundsIntersect(enabledHitTiles[i], tennisBall[0], tennisBall[1]))
                    {
                        //print(tiles[i].gameObject.name);
                        if (enabledHitTiles[i].GetComponentsInChildren<Image>()[0].enabled)
                        {
                            enabledHitTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                            enabledHitTiles[i].GetComponentsInChildren<Image>()[1].enabled = true;
                            //isNoMove = true;
                        }
                    }
                }
            }                        
        }
        
    }

    public void IsMoveAvaliable(GameObject tileObject)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].transform.position == tileObject.transform.position)
            {
                x = (i + 1) / dividedRowNumber;
                y = (i + 1) % dividedRowNumber;

                if (y == 0)
                {
                    y = dividedRowNumber;
                    x--;
                }
            }
        }

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (tiles[(i) * dividedRowNumber + j - 1].GetComponentsInChildren<Image>()[0].enabled)
                {                    
                    isNoMove = true;
                }
            }
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            if (lines[1].GetComponent<BoxCollider2D>().bounds.Intersects(tiles[i].GetComponent<BoxCollider2D>().bounds))
            {
                if (tiles[i].GetComponentsInChildren<Image>()[0].enabled)
                {                    
                    isNoMove = true;
                }
            }
        }

    }

    public void AfterPlayer1TacticGoalXTimesCompleted()
    {
        player1GetAccurateCount = 0;
        player1GetDefensiveCount = 0;
        player1GetNetCount = 0;
        player1GetOffensiveCount = 0;
        player1GetSmartCount = 0;

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameScene.Instance.tacticCards[j].GetComponentsInChildren<Image>()[i + 1].sprite = GameScene.Instance.circleSprites[2];
            }

        }

        bonusButton0.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[1];
        bonusButton1.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[1];
        bonusButton0.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[0];
        GameScene.Instance.bonusCardSet[0].SetActive(true);

        int index = UnityEngine.Random.RandomRange(0, 8);

        if (MainMenu.isEnglish)
        {
            GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite = GameScene.Instance.bonuscardEngSprites[index];
        }
        else if (MainMenu.isSweeden)
        {
            GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite = GameScene.Instance.bonuscardSwedSprites[index];
        }
    }

    public void AfterPlayer2TacticGoalXTimesCompleted()
    {
        player2GetAccurateCount = 0;
        player2GetDefensiveCount = 0;
        player2GetNetCount = 0;
        player2GetOffensiveCount = 0;
        player2GetSmartCount = 0;

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameScene.Instance.tacticCards[5 + j].GetComponentsInChildren<Image>()[i + 1].sprite = GameScene.Instance.circleSprites[2];
            }
            
        }

        bonusButton0.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[1];
        bonusButton1.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[1];
        bonusButton1.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[0];

        GameScene.Instance.bonusCardSet[1].SetActive(true);

        int index = UnityEngine.Random.RandomRange(0, 8);

        if (MainMenu.isEnglish)
        {
            GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite = GameScene.Instance.bonuscardEngSprites[index];
        }
        else if (MainMenu.isSweeden)
        {
            GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite = GameScene.Instance.bonuscardSwedSprites[index];
        }
    }

    public void GetScore( PlayerInfo winner)
    {
        if (winner == player1)
        {
            //print("Player1 wins!!!!!!!!!!!!!!");
        }
        else if (winner == player2)
        {
            //print("Player2 wins!!!!!!!!!!!!!!");
        }


        if (isPlayer1SetNet && winner == player1 && isPlayer1Volley)
        {
            player1GetNetCount++;
            isPlayer1Volley = false;
            //print("Net1"); 

            if (player1GetNetCount > 0)
            {
                for (int i = 0; i < player1GetNetCount; i++)
                {
                    GameScene.Instance.tacticCards[0].GetComponentsInChildren<Image>()[i + 1].sprite = GameScene.Instance.circleSprites[0];
                }
            }

            if (player1GetNetCount == MainMenu.taticGoalCount)
            {
                AfterPlayer1TacticGoalXTimesCompleted();
            }
        }
        else if (isPlayer2SetNet && winner == player2 && isPlayer2Volley)
        {
            player2GetNetCount++;
            isPlayer2Volley = false;
            //print("Net2");

            if (player2GetNetCount > 0)
            {
                for (int i = 0; i < player2GetNetCount; i++)
                {
                    GameScene.Instance.tacticCards[5].GetComponentsInChildren<Image>()[i + 1].sprite = GameScene.Instance.circleSprites[0];
                }
            }

            if (player2GetNetCount == MainMenu.taticGoalCount)
            {
                AfterPlayer2TacticGoalXTimesCompleted();
            }
        }

        if (isPlayer1SetAccurate && winner == player1 && isPlayer1HitAccurate)
        {
            player1GetAccurateCount++;
            isPlayer1HitAccurate = false;
            //print("Accurate1");

            if (player1GetAccurateCount > 0)
            {
                for (int i = 0; i < player1GetAccurateCount; i++)
                {
                    GameScene.Instance.tacticCards[2].GetComponentsInChildren<Image>()[i + 1].sprite = GameScene.Instance.circleSprites[0];
                }
            }

            if (player1GetAccurateCount == MainMenu.taticGoalCount)
            {
                AfterPlayer1TacticGoalXTimesCompleted();
            }
        }
        else if (isPlayer2SetAccurate && winner == player2 && isPlayer2HitAccurate)
        {
            player2GetAccurateCount++;
            isPlayer2HitAccurate = false;
            //print("Accurate2");

            if (player2GetAccurateCount > 0)
            {
                for (int i = 0; i < player2GetAccurateCount; i++)
                {
                    GameScene.Instance.tacticCards[7].GetComponentsInChildren<Image>()[i + 1].sprite = GameScene.Instance.circleSprites[0];
                }
            }

            if (player2GetAccurateCount == MainMenu.taticGoalCount)
            {
                AfterPlayer2TacticGoalXTimesCompleted();
            }
        }
        
         if (isPlayer1SetDefensive && rallyCount >= 10)
        {
            //print("RallyCount:" + rallyCount);
            player1GetDefensiveCount++;
            //print("Defensive1");

            if (player1GetDefensiveCount > 0)
            {
                for (int i = 0; i < player1GetDefensiveCount; i++)
                {
                    GameScene.Instance.tacticCards[1].GetComponentsInChildren<Image>()[i + 1].sprite = GameScene.Instance.circleSprites[0];
                }
            }

            if (player1GetDefensiveCount == MainMenu.taticGoalCount)
            {
                AfterPlayer1TacticGoalXTimesCompleted();
            }
        }
        else if (isPlayer2SetDefensive && rallyCount >= 10)
        {
            //print("RallyCount:" + rallyCount);
            player2GetDefensiveCount++;
            //print("Defensive2");

            if (player2GetDefensiveCount > 0)
            {
                for (int i = 0; i < player2GetDefensiveCount; i++)
                {
                    GameScene.Instance.tacticCards[6].GetComponentsInChildren<Image>()[i + 1].sprite = GameScene.Instance.circleSprites[0];
                }
            }

            if (player2GetDefensiveCount == MainMenu.taticGoalCount)
            {
                AfterPlayer2TacticGoalXTimesCompleted();
            }
        }

        
        if (winner == player1 && isPlayer1SetSmart && player1.StrokeCardCount == 0)
        {
            player1GetSmartCount++;
            //print("Smart1");

            if (player1GetSmartCount > 0)
            {
                for (int i = 0; i < player1GetSmartCount; i++)
                {
                    GameScene.Instance.tacticCards[4].GetComponentsInChildren<Image>()[i + 1].sprite = GameScene.Instance.circleSprites[0];
                }
            }

            if (player1GetSmartCount == MainMenu.taticGoalCount)
            {
                AfterPlayer1TacticGoalXTimesCompleted();
            }
        }
        else if (winner == player2 && isPlayer2SetSmart && player2.StrokeCardCount == 0)
        {
            player2GetSmartCount++;
            //print("Smart2");

            if (player2GetSmartCount > 0)
            {
                for (int i = 0; i < player2GetSmartCount; i++)
                {
                    GameScene.Instance.tacticCards[9].GetComponentsInChildren<Image>()[i + 1].sprite = GameScene.Instance.circleSprites[0];
                }
            }

            if (player2GetSmartCount == MainMenu.taticGoalCount)
            {
                AfterPlayer2TacticGoalXTimesCompleted();
            }
        }

        if (winner == player1 && isPlayer1SetOffensive && rallyCount <= 5)
        {
            player1GetOffensiveCount++;
            //print("Offensive1");

            if (player1GetOffensiveCount > 0)
            {
                for (int i = 0; i < player1GetOffensiveCount; i++)
                {
                    GameScene.Instance.tacticCards[3].GetComponentsInChildren<Image>()[i + 1].sprite = GameScene.Instance.circleSprites[0];
                }
            }

            if (player1GetOffensiveCount == MainMenu.taticGoalCount)
            {
                AfterPlayer1TacticGoalXTimesCompleted();
            }
        }
        else if (winner == player2 && isPlayer2SetOffensive && rallyCount <= 5)
        {
            player2GetOffensiveCount++;
            //print("Offensive2");

            if (player2GetOffensiveCount > 0)
            {
                for (int i = 0; i < player2GetOffensiveCount; i++)
                {
                    GameScene.Instance.tacticCards[8].GetComponentsInChildren<Image>()[i + 1].sprite = GameScene.Instance.circleSprites[0];
                }
            }

            if (player2GetOffensiveCount == MainMenu.taticGoalCount)
            {
                AfterPlayer2TacticGoalXTimesCompleted();
            }
        }

        if (winner == player1)
        {            
            switch (player1.Score)
            {
                case (int)Score.First:
                    {
                        player1.Score = (int)Score.Second;
                        ShowScore();
                        break;
                    }
                case (int)Score.Second:
                    {
                        player1.Score = (int)Score.Third;
                        ShowScore();
                        break;
                    }
                case (int)Score.Third:
                    {
                        player1.Score = (int)Score.Forth;
                        ShowScore();

                        if (player2.Score == (int)Score.Forth)
                        {
                            gameplayInfo.IsQus = true;
                        }

                        break;
                    }
                case (int)Score.Forth:
                    {
                        if (gameplayInfo.IsQus && !player1.IsQusUp)
                        {
                            if (player2.IsQusUp)
                            {
                                ShowScore();
                                player2.IsQusUp = false;
                                player1.IsQusUp = false;
                            }
                            else
                            {
                                player1ScoreText.text = "A";
                                player1.IsQusUp = true;
                                player2.IsQusUp = false;
                            }

                            print("QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ1");

                            //if (player1.IsMyServe)
                            //{
                            //    player1.IsMyServe = false;
                            //    player2.IsMyServe = true;
                            //    player1.IsMyTurn = false;
                            //    player2.IsMyTurn = true;
                            //    serveNotifications[1].GetComponent<Image>().enabled = true;
                            //    serveNotifications[0].GetComponent<Image>().enabled = false;
                            //}
                            //else if (player2.IsMyServe)
                            //{
                            //    player1.IsMyServe = true;
                            //    player2.IsMyServe = false;
                            //    player1.IsMyTurn = true;
                            //    player2.IsMyTurn = false;
                            //    serveNotifications[1].GetComponent<Image>().enabled = false;
                            //    serveNotifications[0].GetComponent<Image>().enabled = true;
                            //}
                        }                        
                        else if(gameplayInfo.IsQus && player1.IsQusUp)
                        {
                            gameplayInfo.IsQus = false;

                            print("QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ2");

                            player1.Level++;
                            bonusButton0.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[1];
                            bonusButton1.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[1];
                            bonusButton0.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[0];                            
                            GameScene.Instance.bonusCardSet[0].SetActive(true);

                            int index = UnityEngine.Random.RandomRange(0, 8);

                            if (MainMenu.isEnglish)
                            {
                                GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite = GameScene.Instance.bonuscardEngSprites[index];
                            }
                            else if (MainMenu.isSweeden)
                            {
                                GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite = GameScene.Instance.bonuscardSwedSprites[index];
                            }

                            if (bonusButton0.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[3] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[3]))
                            {
                                player1.Score = (int)Score.Third;
                                player2.Score = (int)Score.First;
                            }
                            else if (bonusButton0.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[6] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[6]))
                            {
                                player1.Score = (int)Score.Second;
                                player2.Score = (int)Score.First;
                            }
                            else
                            {
                                player1.Score = (int)Score.First;
                                player2.Score = (int)Score.First;
                            }

                            if (bonusButton1.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] || bonusButton0.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0])
                            {
                                if (GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[1] || GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[1] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[1] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[1])
                                {
                                    player1.Level++;
                                }
                            }

                            //StartCoroutine("DelayToShowBonusCard");

                            if (isPlayer1Serve)
                            {
                                player1.IsMyServe = false;
                                player2.IsMyServe = true;
                                player1.IsMyTurn = false;
                                player2.IsMyTurn = true;
                                serveNotifications[0].GetComponent<Image>().enabled = true;
                                serveNotifications[1].GetComponent<Image>().enabled = false;
                            }
                            else if (!isPlayer1Serve)
                            {
                                player1.IsMyServe = true;
                                player2.IsMyServe = false;
                                player1.IsMyTurn = true;
                                player2.IsMyTurn = false;
                                serveNotifications[0].GetComponent<Image>().enabled = false;
                                serveNotifications[1].GetComponent<Image>().enabled = true;
                            }

                            ShowScore();

                            //StartCoroutine("ShowMessage");
                        }
                        break;
                    }
            }
        }
        else if (winner == player2)
        {
            switch (player2.Score)
            {
                case (int)Score.First:
                    {
                        player2.Score = (int)Score.Second;
                        ShowScore();
                        break;
                    }
                case (int)Score.Second:
                    {
                        player2.Score = (int)Score.Third;
                        ShowScore();
                        break;
                    }
                case (int)Score.Third:
                    {
                        player2.Score = (int)Score.Forth;
                        ShowScore();

                        if (player1.Score == (int)Score.Forth)
                        {
                            gameplayInfo.IsQus = true;
                        }

                        break;
                    }
                case (int)Score.Forth:
                    {
                        if (gameplayInfo.IsQus && !player2.IsQusUp)
                        {
                            if (player1.IsQusUp)
                            {
                                ShowScore();
                                player2.IsQusUp = false;
                                player1.IsQusUp = false;
                            }
                            else
                            {
                                player2ScoreText.text = "A";
                                player2.IsQusUp = true;
                                player1.IsQusUp = false;
                            }


                            print("QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ3");
                            //if (player1.IsMyServe)
                            //{
                            //    player1.IsMyServe = false;
                            //    player2.IsMyServe = true;
                            //    player1.IsMyTurn = false;
                            //    player2.IsMyTurn = true;
                            //    serveNotifications[1].GetComponent<Image>().enabled = true;
                            //    serveNotifications[0].GetComponent<Image>().enabled = false;
                            //}
                            //else if (player2.IsMyServe)
                            //{
                            //    player1.IsMyServe = true;
                            //    player2.IsMyServe = false;
                            //    player1.IsMyTurn = true;
                            //    player2.IsMyTurn = false;
                            //    serveNotifications[1].GetComponent<Image>().enabled = false;
                            //    serveNotifications[0].GetComponent<Image>().enabled = true;
                            //}
                        }
                        else if (gameplayInfo.IsQus && player2.IsQusUp)
                        {
                            print("QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ4");
                            gameplayInfo.IsQus = false;                            

                            player2.Level++;
                            bonusButton0.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[1];
                            bonusButton1.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[1];
                            bonusButton1.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[0];
                            
                            GameScene.Instance.bonusCardSet[1].SetActive(true);

                            int index = UnityEngine.Random.RandomRange(0, 8);

                            if (MainMenu.isEnglish)
                            {
                                GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite = GameScene.Instance.bonuscardEngSprites[index];
                            }
                            else if (MainMenu.isSweeden)
                            {
                                GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite = GameScene.Instance.bonuscardSwedSprites[index];
                            }

                            if (bonusButton1.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[3] || GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[3]))
                            {
                                player1.Score = (int)Score.First;
                                player2.Score = (int)Score.Third;
                            }
                            else if (bonusButton1.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[6] || GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[6]))
                            {
                                player1.Score = (int)Score.First;
                                player2.Score = (int)Score.Second;
                            }
                            else
                            {
                                player1.Score = (int)Score.First;
                                player2.Score = (int)Score.First;
                            }



                            if (bonusButton1.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] || bonusButton0.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0])
                            {
                                if (GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[1] || GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[1] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[1] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[1])
                                {
                                    player2.Level++;
                                }
                            }

                            StartCoroutine("DelayToShowBonusCard");

                            if (isPlayer1Serve)
                            {
                                player1.IsMyServe = false;
                                player2.IsMyServe = true;
                                player1.IsMyTurn = false;
                                player2.IsMyTurn = true;
                                serveNotifications[1].GetComponent<Image>().enabled = true;
                                serveNotifications[0].GetComponent<Image>().enabled = false;
                            }
                            else if (!isPlayer1Serve)
                            {
                                player1.IsMyServe = true;
                                player2.IsMyServe = false;
                                player1.IsMyTurn = true;
                                player2.IsMyTurn = false;
                                serveNotifications[1].GetComponent<Image>().enabled = false;
                                serveNotifications[0].GetComponent<Image>().enabled = true;
                            }

                            ShowScore();
                        }
                        break;
                    }
            }
        }

        

        if (player1.IsMyServe)
        {            
            player1.IsMyTurn = true;
            player2.IsMyTurn = false;
            serveNotifications[1].GetComponent<Image>().enabled = false;
            serveNotifications[0].GetComponent<Image>().enabled = true;
        }
        else if (player2.IsMyServe)
        {            
            player1.IsMyTurn = false;
            player2.IsMyTurn = true;
            serveNotifications[1].GetComponent<Image>().enabled = true;
            serveNotifications[0].GetComponent<Image>().enabled = false;
        }

        StartCoroutine("DelayStart");
    }

    public void ShowScore()
    {
        player1LevelText.text = "" + player1.Level;
        player1ScoreText.text = "" + player1.Score;
        player2LevelText.text = "" + player2.Level;
        player2ScoreText.text = "" + player2.Score;
    }

    IEnumerator DelayToShowBonusCard()
    {
        yield return new WaitForSeconds(3);

        GameScene.Instance.bonusCardSet[0].SetActive(false);
        GameScene.Instance.bonusCardSet[1].SetActive(false);
    }

    IEnumerator DelayTacticCard()
    {
        yield return new WaitForSeconds(2);        

        tacticCardset2.SetActive(false);
        panel2.SetActive(false);
        characters2.SetActive(true);
        tacticCardMessage2.SetActive(false);
        Player2TacticCardClick();        


    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(2);

        FormatBoolFlag();
        FormatBoolFlag1();
        GameEngine.Instance.notifications[12].SetActive(false);
        GameEngine.Instance.coachCommandImages[0].gameObject.SetActive(false);
        GameEngine.Instance.coachCommandImages[1].gameObject.SetActive(false);
        gameplayInfo.IsStart = true;
        characterCards.SetActive(false);
        isAIAction = false;        
        isNoRepeat = true;
        notifications[0].SetActive(true);
        panel.SetActive(true);
        tacticalCards1.SetActive(true);
    }

    IEnumerator DelayToServeFailed()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
            tiles[i].GetComponentsInChildren<Image>()[1].enabled = false;
        }

        for (int i = 0; i < numberTexts.Length; i++)
        {
            numberTexts[i].SetActive(false);
        }

        lines[0].SetActive(false);
        lines[1].SetActive(false);
        rollBall[0].SetActive(false);
        tennisBall[0].SetActive(false);
        tennisBall[1].SetActive(false);
        playerPans[0].SetActive(false);
        playerPans[1].SetActive(false);

        gameplayInfo.IsServe = true;
        isNoRepeat = true;
        isFirstServeFailed = true;
    }

    public bool OutOfRangeServe(GameObject originObject)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            
            if (Vector3.Distance(tiles[i].transform.position, originObject.transform.position) < 0.5f)
            {
                x = (i + 1) / dividedRowNumber;
                y = (i + 1) % dividedRowNumber;

                if (y == 0)
                {
                    y = dividedRowNumber;
                    x--;
                }
            }
        }

        if (player1.IsMyServe)
        {
            if (CheckServerPosition())
            {

                if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                {
                    if (x >= 3 && x <= 5 && y >= 14 && y <= 18)
                    {
                        //print(false);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (Application.loadedLevelName == "GameOrange1")
                {
                    if (x >= 2 && x <= 4 && y >= 11 && y <= 15)
                    {
                        //print(false);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (Application.loadedLevelName == "GameRed1")
                {
                    if (x >= 2 && x <= 3 && y >= 10 && y <= 14)
                    {
                        //print(false);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }

                
            }
            else
            {

                if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                {
                    if (x >= 6 && x <= 8 && y >= 14 && y <= 18)
                    {
                        //print(false);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (Application.loadedLevelName == "GameOrange1")
                {
                    if (x >= 4 && x <= 6 && y >= 11 && y <= 15)
                    {
                        //print(false);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (Application.loadedLevelName == "GameRed1")
                {
                    if (x >= 4 && x <= 5 && y >= 10 && y <= 14)
                    {
                        //print(false);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }


            }
            
        }
        else
        {
            if (CheckServerPosition())
            {

                if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                {
                    if (x >= 6 && x <= 8 && y >= 9 && y <= 13)
                    {

                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (Application.loadedLevelName == "GameOrange1")
                {
                    if (x >= 4 && x <= 6 && y >= 6 && y <= 10)
                    {

                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (Application.loadedLevelName == "GameRed1")
                {
                    if (x >= 4 && x <= 5 && y >= 5 && y <= 9)
                    {

                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }


            }
            else
            {

                if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                {
                    if (x >= 3 && x <= 5 && y >= 9 && y <= 13)
                    {

                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (Application.loadedLevelName == "GameOrange1")
                {
                    if (x >= 2 && x <= 4 && y >= 6 && y <= 10)
                    {

                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (Application.loadedLevelName == "GameRed1")
                {
                    if (x >= 2 && x <= 3 && y >= 5 && y <= 9)
                    {

                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;                    
                }


            }
           
        }
    }

    public bool OutOfRange(GameObject originObject)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (Vector3.Distance(tiles[i].transform.position, originObject.transform.position) < 0.5f)
            {
                x = (i + 1) / dividedRowNumber;
                y = (i + 1) % dividedRowNumber;

                if (y == 0)
                {
                    y = dividedRowNumber;
                    x--;
                }
            }
        }


        if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
        {
            if (x >= 3 && x <= 8 && y >= 5 && y <= 22)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (Application.loadedLevelName == "GameOrange1")
        {
            if (x >= 2 && x <= 6 && y >= 3 && y <= 18)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (Application.loadedLevelName == "GameRed1")
        {
            if (x >= 2 && x <= 5 && y >= 3 && y <= 16)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }    
    }

    public void TopSpinClick()
    {
        FormatBoolFlag();        
        isTopSpin = true;

        if (player1.IsMyTurn)
        {           
            player1.StrokeCardCount++;
        }
        else
        {            
            player2.StrokeCardCount++;
        }

    }

    public void SliceShotClick()
    {
        FormatBoolFlag();

        if (player1.IsMyTurn)
        {
            player1.BonusMove = 1;
            player1.StrokeCardCount++;
        }
        else
        {
            player2.BonusMove = 1;
            player2.StrokeCardCount++;
        }

        isSliceShot = true;        
    }

    public void PoweShotClick()
    {
        FormatBoolFlag();

        if (player1.IsMyTurn)
        {
            player1.BonusMove = -1;
            player2.BonusMove = -1;
            player1.StrokeCardCount++;
        }
        else
        {
            player1.BonusMove = -1;
            player2.BonusMove = -1;
            player2.StrokeCardCount++;
        }

        isPowerShot = true;       
    }

    public void LobShotClick()
    {
        FormatBoolFlag();

        if (player1.IsMyTurn)
        {
            player2.BonusMove = 3;
            player1.StrokeCardCount++;
        }
        else
        {
            player1.BonusMove = 3;
            player2.StrokeCardCount++;
        }

        isLobShot = true;       
    }

    public void DropShotClick()
    {
        FormatBoolFlag();

        if (player1.IsMyTurn)
        {
            player2.BonusMove = 3;
            player1.StrokeCardCount++;
        }
        else
        {
            player1.BonusMove = 3;
            player2.StrokeCardCount++;
        }

        isDropShot = true;        
    }

    public void SpecialStrokeClick()
    {
        if (bonusButton0.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[2] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[2]))
        {
            if (player1.IsMyTurn && gameplayInfo.IsWhereToStrike)
            {
                strokeCards.SetActive(true);
            }               
        }
        else if (bonusButton0.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[4] || GameScene.Instance.bonusCardSet[0].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[4]))
        {
            
        }
        else
        {
            if (player1.IsMyTurn && player1.StrokeCardCount < MainMenu.strokeNumber && gameplayInfo.IsWhereToStrike)
            {
                strokeCards.SetActive(true);
            }

            
        }                
    }

    public void SpecialStrokeClick1()
    {
        if (bonusButton1.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[2] || GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[2]))
        {
            if (player2.IsMyTurn && gameplayInfo.IsWhereToStrike)
            {
                strokeCards1.SetActive(true);
            }
        }
        else if (bonusButton1.GetComponent<Image>().sprite == GameScene.Instance.bonusCardSprites[0] && (GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardEngSprites[4] || GameScene.Instance.bonusCardSet[1].GetComponent<Image>().sprite == GameScene.Instance.bonuscardSwedSprites[4]))
        {
            
        }
        else
        {
            if (player2.IsMyTurn && player2.StrokeCardCount < MainMenu.strokeNumber && gameplayInfo.IsWhereToStrike)
            {

                strokeCards1.SetActive(true);
            }

            
        }
        
    }

    public bool IsBoundsIntersect(GameObject tileObject, GameObject startObject, GameObject endObject)
    {
        float a = Mathf.Floor(tileObject.GetComponent<RectTransform>().rect.width);        

        for (int i = 0; i < 50; i++)
        {           
            if (tileObject.GetComponent<RectTransform>().position.x -  a/ 2 <= startObject.GetComponent<RectTransform>().position.x + (endObject.GetComponent<RectTransform>().position.x - startObject.GetComponent<RectTransform>().position.x) * i / 50f && tileObject.GetComponent<RectTransform>().position.x + a / 2 >= startObject.GetComponent<RectTransform>().position.x + (endObject.GetComponent<RectTransform>().position.x - startObject.GetComponent<RectTransform>().position.x) * i / 50f && tileObject.GetComponent<RectTransform>().position.y - a / 2 <= startObject.GetComponent<RectTransform>().position.y + (endObject.GetComponent<RectTransform>().position.y - startObject.GetComponent<RectTransform>().position.y) * i / 50f && tileObject.GetComponent<RectTransform>().position.y + a / 2 >= startObject.GetComponent<RectTransform>().position.y + (endObject.GetComponent<RectTransform>().position.y - startObject.GetComponent<RectTransform>().position.y) * i / 50f)
            {
                return true;                
            }           
        }

        return false;             
    }

    public void CheckVolley(GameObject tileObject)
    {
        isPlayer1Volley = false;
        isPlayer2Volley = false;

        for (int i = 0; i < tiles.Length; i++)
        {
            if (IsBoundsIntersect(tiles[i], prevPlayerPan, tennisBall[1]))
            {
                //print(tiles[i].gameObject.name);
                if (tiles[i] == tileObject)
                {
                    if (player1.IsMyTurn)
                    {
                        isPlayer1Volley = true;
                    }
                    else
                    {
                        isPlayer2Volley = true;
                    }
                    
                }
            }
        }
    }

    public bool CheckServerPosition()
    {
        int a;int b;

        if (player1ScoreText.text == "40")
        {
            a = 45;
        }
        else if (player1ScoreText.text == "A")
        {
            a = 10;
        }
        else
        {
            a = (int)player1.Score;
        }

        if (player2ScoreText.text == "40")
        {
            b = 45;
        }
        else if (player2ScoreText.text == "A")
        {
            b = 10;
        }
        else
        {
            b = (int)player2.Score;
        }

        if ((a + b) % 2 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckStrokeStates()
    {
        if (player1.StrokeCardCount >= MainMenu.strokeNumber)
        {
            if (MainMenu.isEnglish)
            {
                GameScene.Instance.strokeButton[0].sprite = GameScene.Instance.oStrokeEngButton[0];
            }
            else if (MainMenu.isSweeden)
            {
                GameScene.Instance.strokeButton[0].sprite = GameScene.Instance.oStrokeSwedButton[0];
            }
        }
        else
        {
            if (MainMenu.isEnglish)
            {
                GameScene.Instance.strokeButton[0].sprite = GameScene.Instance.strokeEngButton[0];
            }
            else if (MainMenu.isSweeden)
            {
                GameScene.Instance.strokeButton[0].sprite = GameScene.Instance.strokeSwedButton[0];
            }
        }

        if (player2.StrokeCardCount >= MainMenu.strokeNumber)
        {
            if (MainMenu.isEnglish)
            {
                GameScene.Instance.strokeButton[1].sprite = GameScene.Instance.oStrokeEngButton[1];
            }
            else if (MainMenu.isSweeden)
            {
                GameScene.Instance.strokeButton[1].sprite = GameScene.Instance.oStrokeSwedButton[1];
            }
        }
        else
        {
            if (MainMenu.isEnglish)
            {
                GameScene.Instance.strokeButton[1].sprite = GameScene.Instance.strokeEngButton[1];
            }
            else if (MainMenu.isSweeden)
            {
                GameScene.Instance.strokeButton[1].sprite = GameScene.Instance.strokeSwedButton[1];
            }
        }
    }

    public void CheckAccurateHit(GameObject tileObject)
    {
        isPlayer1HitAccurate = false;
        isPlayer2HitAccurate = false;

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].transform.position == tileObject.transform.position)
            {
                x3 = (i + 1) / dividedRowNumber;
                y3 = (i + 1) % dividedRowNumber;

                if (y3 == 0)
                {
                    y3 = dividedRowNumber;
                    x3--;
                }
            }
        }

        if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
        {
            if (player1.IsMyTurn)
            {
                if (x3 == 3 || x3 == 8)
                {
                    if (y3 <= 13 && y3 >= 5)
                    {
                        isPlayer1HitAccurate = true;
                    }
                }

                if (y3 == 5)
                {
                    if (x3 >= 3 && x3 <= 8)
                    {
                        isPlayer1HitAccurate = true;
                    }
                }
            }
            else
            {

                if (x3 == 3 || x3 == 8)
                {
                    if (y3 <= 22 && y3 >= 14)
                    {
                        isPlayer2HitAccurate = true;
                    }
                }

                if (y3 == 22)
                {
                    if (x3 >= 3 && x3 <= 8)
                    {
                        isPlayer2HitAccurate = true;
                    }
                }
            }
        }
        else if (Application.loadedLevelName == "GameOrange1")
        {
            if (player1.IsMyTurn)
            {
                if (x3 == 2 || x3 == 6)
                {
                    if (y3 <= 10 && y3 >= 3)
                    {
                        isPlayer1HitAccurate = true;
                    }
                }

                if (y3 == 3)
                {
                    if (x3 >= 2 && x3 <= 11)
                    {
                        isPlayer1HitAccurate = true;
                    }
                }
            }
            else
            {

                if (x3 == 2 || x3 == 6)
                {
                    if (y3 <= 18 && y3 >= 11)
                    {
                        isPlayer2HitAccurate = true;
                    }
                }

                if (y3 == 18)
                {
                    if (x3 >= 2 && x3 <= 6)
                    {
                        isPlayer2HitAccurate = true;
                    }
                }
            }
        }
        else if (Application.loadedLevelName == "GameRed1")
        {
            if (player1.IsMyTurn)
            {
                if (x3 == 2 || x3 == 5)
                {
                    if (y3 <= 9 && y3 >= 3)
                    {
                        isPlayer1HitAccurate = true;
                    }
                }

                if (y3 == 3)
                {
                    if (x3 >= 2 && x3 <= 5)
                    {
                        isPlayer1HitAccurate = true;
                    }
                }
            }
            else
            {

                if (x3 == 2 || x3 == 5)
                {
                    if (y3 <= 16 && y3 >= 10)
                    {
                        isPlayer2HitAccurate = true;
                    }
                }

                if (y3 == 16)
                {
                    if (x3 >= 2 && x3 <= 5)
                    {
                        isPlayer2HitAccurate = true;
                    }
                }
            }
        }
    }
}

