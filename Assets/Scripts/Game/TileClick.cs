using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileClick : MonoBehaviour
{
    public GameObject AISelectTileObject;
    public List<GameObject> CandidatesObject = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (GameEngine.Instance.gameplayInfo.IsServe)
        {
            //print(this.gameObject.name);
            //print(GameEngine.Instance.player1.IsMyTurn);
            //print(GameEngine.Instance.player2.IsMyTurn);

            if (GameEngine.Instance.player1.IsMyTurn)
            {
                
                for (int i = 0; i < GameEngine.Instance.player1ServeTiles.Length; i++)
                {
                    if (GameEngine.Instance.player1ServeTiles[i] == this.gameObject)
                    {                        
                        GameEngine.Instance.gameplayInfo.IsServe = false;
                        GameEngine.Instance.gameplayInfo.IsReceive = true;
                        GameEngine.Instance.player1.IsMyTurn = false;
                        GameEngine.Instance.player2.IsMyTurn = true;
                        GameEngine.Instance.isNoRepeat = true;
                        GameEngine.Instance.playerPans[0].SetActive(true);
                        GameEngine.Instance.playerPans[0].transform.position = new Vector3(GameEngine.Instance.player1ServeTiles[i].gameObject.transform.position.x, GameEngine.Instance.player1ServeTiles[i].gameObject.transform.position.y, GameEngine.Instance.player1ServeTiles[i].gameObject.transform.position.z);
                        GameEngine.Instance.player1Position = GameEngine.Instance.playerPans[0];

                        for (int j = 0; j < GameEngine.Instance.player1ServeTiles.Length; j++)
                        {
                            GameEngine.Instance.player1ServeTiles[j].GetComponentsInChildren<Image>()[0].enabled = false;
                        }

                    }
                }
            }
            else if (GameEngine.Instance.player2.IsMyTurn)
            {
                for (int i = 0; i < GameEngine.Instance.player2ServeTiles.Length; i++)
                {
                    if (GameEngine.Instance.player2ServeTiles[i] == this.gameObject)
                    {
                        print(GameEngine.Instance.player1.IsMyTurn);
                        print(GameEngine.Instance.player2.IsMyTurn);
                        GameEngine.Instance.gameplayInfo.IsServe = false;
                        GameEngine.Instance.gameplayInfo.IsReceive = true;
                        GameEngine.Instance.player1.IsMyTurn = true;
                        GameEngine.Instance.player2.IsMyTurn = false;
                        GameEngine.Instance.isNoRepeat = true;
                        GameEngine.Instance.playerPans[1].SetActive(true);
                        GameEngine.Instance.playerPans[1].transform.position = new Vector3(GameEngine.Instance.player2ServeTiles[i].gameObject.transform.position.x, GameEngine.Instance.player2ServeTiles[i].gameObject.transform.position.y, GameEngine.Instance.player2ServeTiles[i].gameObject.transform.position.z);
                        GameEngine.Instance.player2Position = GameEngine.Instance.playerPans[1];

                        for (int j = 0; j < GameEngine.Instance.player2ServeTiles.Length; j++)
                        {
                            GameEngine.Instance.player2ServeTiles[j].GetComponentsInChildren<Image>()[0].enabled = false;
                        }
                    }
                }
            }
        }
        else if (GameEngine.Instance.gameplayInfo.IsReceive)
        {
            if (GameEngine.Instance.player1.IsMyTurn)
            {
                for (int i = 0; i < GameEngine.Instance.player1ServeTiles.Length; i++)
                {
                    if (GameEngine.Instance.player1ServeTiles[i] == this.gameObject)
                    {
                        for (int j = 0; j < GameEngine.Instance.player1ServeTiles.Length; j++)
                        {
                            GameEngine.Instance.player1ServeTiles[j].GetComponentsInChildren<Image>()[0].enabled = false;
                        }

                        GameEngine.Instance.player2.IsMyTurn = true;
                        GameEngine.Instance.player1.IsMyTurn = false;
                        GameEngine.Instance.gameplayInfo.IsReceive = false;
                        GameEngine.Instance.gameplayInfo.IsWhereToServe = true;
                        GameEngine.Instance.isNoRepeat = true;

                        GameEngine.Instance.playerPans[0].SetActive(true);
                        GameEngine.Instance.playerPans[0].transform.position = this.gameObject.transform.position;
                        GameEngine.Instance.player1Position = GameEngine.Instance.playerPans[0];                       
                    }
                }
            }
            else if (GameEngine.Instance.player2.IsMyTurn)
            {
                for (int i = 0; i < GameEngine.Instance.player2ServeTiles.Length; i++)
                {
                    if (GameEngine.Instance.player2ServeTiles[i] == this.gameObject)
                    {
                        for (int j = 0; j < GameEngine.Instance.player2ServeTiles.Length; j++)
                        {
                            GameEngine.Instance.player2ServeTiles[j].GetComponentsInChildren<Image>()[0].enabled = false;
                        }

                        GameEngine.Instance.player1.IsMyTurn = true;
                        GameEngine.Instance.player2.IsMyTurn = false;
                        GameEngine.Instance.gameplayInfo.IsReceive = false;
                        GameEngine.Instance.gameplayInfo.IsWhereToServe = true;
                        GameEngine.Instance.isNoRepeat = true;

                        GameEngine.Instance.playerPans[1].SetActive(true);
                        GameEngine.Instance.playerPans[1].transform.position = this.gameObject.transform.position;                        
                        GameEngine.Instance.player2Position = GameEngine.Instance.playerPans[1];                       
                    }
                }
            }
        }
        else if (GameEngine.Instance.gameplayInfo.IsWhereToServe)
        {
            if (GameEngine.Instance.player1.IsMyTurn)
            {
                GameEngine.Instance.ShowNumbers(this.gameObject);
                if (Application.loadedLevelName == "GameRed1")
                {
                    for (int i = 0; i < 12; i++)
                    {
                        GameEngine.Instance.player1whereToServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                    }
                }
                else
                {
                    for (int i = 0; i < 18; i++)
                    {
                        GameEngine.Instance.player1whereToServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                    }
                }
                
            }
            else if (GameEngine.Instance.player2.IsMyTurn)
            {
                GameEngine.Instance.ShowNumbers(this.gameObject);

                if (Application.loadedLevelName == "GameRed1")
                {
                    for (int i = 0; i < 12; i++)
                    {
                        GameEngine.Instance.player2whereToserveTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                    }
                }
                else
                {
                    for (int i = 0; i < 18; i++)
                    {
                        GameEngine.Instance.player2whereToserveTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                    }
                }

               
            }

            GameEngine.Instance.gameplayInfo.IsRoll = true;
            GameEngine.Instance.gameplayInfo.IsWhereToServe = false;
            GameEngine.Instance.isNoRepeat = true;
        }
        else if (GameEngine.Instance.gameplayInfo.IsRecoveryMove)
        {            
            GameEngine.Instance.ShowRollNumber();

            if (this.gameObject.GetComponentsInChildren<Image>()[0].enabled)
            {
                if (GameEngine.Instance.player1.IsMyTurn)
                {
                    GameEngine.Instance.playerPans[0].transform.position = this.gameObject.transform.position;
                    GameEngine.Instance.player1Position = GameEngine.Instance.playerPans[0];
                    GameEngine.Instance.player1.IsMyTurn = false;
                    GameEngine.Instance.player2.IsMyTurn = true;

                }
                else if (GameEngine.Instance.player2.IsMyTurn)
                {
                    GameEngine.Instance.playerPans[1].transform.position = this.gameObject.transform.position;
                    GameEngine.Instance.player2Position = GameEngine.Instance.playerPans[1];
                    GameEngine.Instance.player1.IsMyTurn = true;
                    GameEngine.Instance.player2.IsMyTurn = false;
                }

                for (int i = 0; i < GameEngine.Instance.tiles.Length; i++)
                {
                    GameEngine.Instance.tiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                }

                GameEngine.Instance.gameplayInfo.IsRecoveryMove = false;
                GameEngine.Instance.gameplayInfo.IsMoveToStrike = true;
                GameEngine.Instance.isNoRepeat = true;
                
                GameEngine.Instance.ShowMoveToStrike(GameEngine.Instance.tennisBall[0]);
            }
        }
        else if (GameEngine.Instance.gameplayInfo.IsMoveToStrike && !GameEngine.Instance.isMoveToStrike)
        {
            for (int i = 0; i < GameEngine.Instance.tiles.Length; i++)
            {
                if (GameEngine.Instance.tiles[i].gameObject == this.gameObject)
                {
                    if (GameEngine.Instance.tiles[i].GetComponentsInChildren<Image>()[0].enabled)
                    {
                        GameEngine.Instance.isMoveToStrike = false;
                        GameEngine.Instance.gameplayInfo.IsMoveToStrike = false;
                        GameEngine.Instance.notifications[9].SetActive(false);
                        //GameEngine.Instance.notifications[12].SetActive(true);

                        if (GameEngine.Instance.player1.IsMyTurn)
                        {
                            GameEngine.Instance.coachCommandImages[0].gameObject.SetActive(true);
                            GameEngine.Instance.coachCommandImages[0].sprite = GameEngine.Instance.coach1Commands[7];

                        }
                        else if (GameEngine.Instance.player2.IsMyTurn)
                        {
                            GameEngine.Instance.coachCommandImages[1].gameObject.SetActive(true);
                            GameEngine.Instance.coachCommandImages[1].sprite = GameEngine.Instance.coach2Commands[7];

                        }

                        if (GameEngine.Instance.player1.IsMyTurn)
                        {
                            GameEngine.Instance.GetScore(GameEngine.Instance.player2);

                        }
                        else if (GameEngine.Instance.player2.IsMyTurn)
                        {
                            GameEngine.Instance.GetScore(GameEngine.Instance.player1);
                        }
                    }
                    else if (GameEngine.Instance.tiles[i].GetComponentsInChildren<Image>()[1].enabled)
                    {
                        GameEngine.Instance.FormatBoolFlag();
                        GameEngine.Instance.isMoveToStrike = false;
                        GameEngine.Instance.gameplayInfo.IsMoveToStrike = false;
                        GameEngine.Instance.CheckAccurateHit(GameEngine.Instance.tennisBall[1]);
                        GameEngine.Instance.gameplayInfo.IsWhereToStrike = true;
                        GameEngine.Instance.notifications[9].SetActive(false);
                        //GameEngine.Instance.notifications[8].SetActive(true);
                        GameEngine.Instance.isNoRepeat = true;

                        if (GameEngine.Instance.player1.IsMyTurn)
                        {
                            GameEngine.Instance.playerPans[0].transform.position = this.gameObject.transform.position;
                            GameEngine.Instance.player1Position = GameEngine.Instance.playerPans[0];
                            //GameEngine.Instance.player1.IsMyTurn = false;
                            //GameEngine.Instance.player2.IsMyTurn = true;

                        }
                        else if (GameEngine.Instance.player2.IsMyTurn)
                        {
                            GameEngine.Instance.playerPans[1].transform.position = this.gameObject.transform.position;
                            GameEngine.Instance.player2Position = GameEngine.Instance.playerPans[1];
                            //GameEngine.Instance.player1.IsMyTurn = true;
                            //GameEngine.Instance.player2.IsMyTurn = false;
                        }

                        GameEngine.Instance.CheckVolley(this.gameObject);
                        GameEngine.Instance.rallyCount++;
                    }
                }
            }

        }
        else if (GameEngine.Instance.gameplayInfo.IsMoveToStrike && GameEngine.Instance.isMoveToStrike)
        {
            for (int i = 0; i < GameEngine.Instance.tiles.Length; i++)
            {
                if (GameEngine.Instance.tiles[i].gameObject == this.gameObject)
                {
                    if (GameEngine.Instance.tiles[i].GetComponentsInChildren<Image>()[0].enabled)
                    {
                        GameEngine.Instance.isMoveToStrike = false;
                        GameEngine.Instance.gameplayInfo.IsMoveToStrike = false;
                        GameEngine.Instance.notifications[9].SetActive(false);
                        //GameEngine.Instance.notifications[12].SetActive(true);

                        if (GameEngine.Instance.player1.IsMyTurn)
                        {
                            GameEngine.Instance.coachCommandImages[0].gameObject.SetActive(true);
                            GameEngine.Instance.coachCommandImages[0].sprite = GameEngine.Instance.coach1Commands[7];

                        }
                        else if (GameEngine.Instance.player2.IsMyTurn)
                        {
                            GameEngine.Instance.coachCommandImages[1].gameObject.SetActive(true);
                            GameEngine.Instance.coachCommandImages[1].sprite = GameEngine.Instance.coach2Commands[7];

                        }

                        if (GameEngine.Instance.player1.IsMyTurn)
                        {
                            GameEngine.Instance.GetScore(GameEngine.Instance.player2);

                        }
                        else if (GameEngine.Instance.player2.IsMyTurn)
                        {
                            GameEngine.Instance.GetScore(GameEngine.Instance.player1);
                        }
                    }
                    else if (GameEngine.Instance.tiles[i].GetComponentsInChildren<Image>()[1].enabled)
                    {
                        GameEngine.Instance.isMoveToStrike = false;
                        GameEngine.Instance.gameplayInfo.IsMoveToStrike = false;
                        GameEngine.Instance.gameplayInfo.IsWhereToStrike = true;
                        GameEngine.Instance.notifications[9].SetActive(false);
                        //GameEngine.Instance.notifications[8].SetActive(true);
                        GameEngine.Instance.isNoRepeat = true;

                        if (GameEngine.Instance.player1.IsMyTurn)
                        {
                            GameEngine.Instance.playerPans[0].transform.position = this.gameObject.transform.position;
                            GameEngine.Instance.player1Position = GameEngine.Instance.playerPans[0];
                            //GameEngine.Instance.player1.IsMyTurn = false;
                            //GameEngine.Instance.player2.IsMyTurn = true;

                        }
                        else if (GameEngine.Instance.player2.IsMyTurn)
                        {
                            GameEngine.Instance.playerPans[1].transform.position = this.gameObject.transform.position;
                            GameEngine.Instance.player2Position = GameEngine.Instance.playerPans[1];
                            //GameEngine.Instance.player1.IsMyTurn = true;
                            //GameEngine.Instance.player2.IsMyTurn = false;
                        }
                    }
                }
            }
        }
        else if (GameEngine.Instance.gameplayInfo.IsWhereToStrike)
        {
            for (int i = 0; i < GameEngine.Instance.tiles.Length; i++)
            {
                GameEngine.Instance.tiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                GameEngine.Instance.tiles[i].GetComponentsInChildren<Image>()[1].enabled = false;
            }

            GameEngine.Instance.ShowNumbers(this.gameObject);

            if (GameEngine.Instance.player1.IsMyTurn)
            {
                            
            }
            else if (GameEngine.Instance.player2.IsMyTurn)
            {      
                        
            }

            GameEngine.Instance.gameplayInfo.IsRoll = true;
            GameEngine.Instance.gameplayInfo.IsWhereToStrike = false;
            GameEngine.Instance.isNoRepeat = true;
            GameEngine.Instance.notifications[8].SetActive(false);

            if (MainMenu.isEnglish)
            {
                GameScene.Instance.strokeButton[1].sprite = GameScene.Instance.oStrokeEngButton[1];
                GameScene.Instance.strokeButton[0].sprite = GameScene.Instance.oStrokeEngButton[0];
            }
            else if (MainMenu.isSweeden)
            {
                GameScene.Instance.strokeButton[1].sprite = GameScene.Instance.oStrokeSwedButton[1];
                GameScene.Instance.strokeButton[0].sprite = GameScene.Instance.oStrokeSwedButton[0];
            }

            GameEngine.Instance.bonusButton0.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[1];
            GameEngine.Instance.bonusButton1.GetComponent<Image>().sprite = GameScene.Instance.bonusCardSprites[1];
        }
    }
}
