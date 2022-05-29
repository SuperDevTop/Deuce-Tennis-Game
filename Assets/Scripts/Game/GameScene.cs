using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public static GameScene Instance;
    public Text[] tacticCardTitle1;
    public Text[] tacticCardDes1;
    public Text[] tacticCardTitle2;
    public Text[] tacticCardDes2;
    public Text[] playerNames;
    public Text[] others;
    public Image[] strokeCards;
    public Image[] strokeCards1;
    public Image[] strokeButton;
    public Image[] bonusCards;
    public Sprite[] strokeEngCards;
    public Sprite[] strokeSwedCards;
    public Sprite[] strokeEngCards1;
    public Sprite[] strokeSwedCards1;
    public Sprite[] strokeEngButton;
    public Sprite[] strokeSwedButton;
    public Sprite[] oStrokeEngButton;
    public Sprite[] oStrokeSwedButton;
    public Sprite[] bonuscardEngSprites;
    public Sprite[] bonuscardSwedSprites;
    public GameObject[] bonusCardSet;
    public Sprite[] bonusCardSprites;
    public Image[] playerBatsInScore;
    public Image[] playerBats;
    public Sprite[] playerBatSprites;
    public GameObject[] bonusPans;
    public GameObject[] strokePans;
    public GameObject[] tacticCards;
    public Sprite[] circleSprites;   

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerNames[0].text = MainMenu.player1Name;
        playerNames[1].text = MainMenu.player2Name;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < MainMenu.taticGoalCount; j++)
            {
                tacticCards[i].GetComponentsInChildren<Image>()[j + 1].enabled = true;
            }
        }        

        for (int i = 0; i < strokeCards.Length; i++)
        {
            if (MainMenu.isEnglish)
            {
                strokeCards[i].sprite = strokeEngCards[i];
            }
            else if (MainMenu.isSweeden)
            {
                strokeCards[i].sprite = strokeSwedCards[i];
            }
        }

        for (int i = 0; i < strokeCards1.Length; i++)
        {
            if (MainMenu.isEnglish)
            {
                strokeCards1[i].sprite = strokeEngCards1[i];
            }
            else if (MainMenu.isSweeden)
            {
                strokeCards1[i].sprite = strokeSwedCards1[i];
            }
        }


        if (MainMenu.isEnglish)
        {
            strokeButton[0].sprite = oStrokeEngButton[0];
            strokeButton[1].sprite = oStrokeEngButton[1];



        }
        else if (MainMenu.isSweeden)
        {
            strokeButton[0].sprite = oStrokeSwedButton[0];
            strokeButton[1].sprite = oStrokeSwedButton[1];


        }

        //for (int i = 0; i < bonusCards.Length; i++)
        //{
        //    if (MainMenu.isEnglish)
        //    {
        //        bonusCards[i].sprite = bonuscardEngSprites[i];
        //    }
        //    else if (MainMenu.isSweeden)
        //    {
        //        bonusCards[i].sprite = bonuscardSwedSprites[i];
        //    }
        //}

        switch (MainMenu.player1BatNumber)
        {
            case 0:
                playerBatsInScore[0].sprite = playerBatSprites[0];
                playerBats[0].sprite = playerBatSprites[0];
                break;
            case 1:
                playerBatsInScore[0].sprite = playerBatSprites[1];
                playerBats[0].sprite = playerBatSprites[1];
                break;
            case 2:
                playerBatsInScore[0].sprite = playerBatSprites[2];
                playerBats[0].sprite = playerBatSprites[2];
                break;
            case 3:
                playerBatsInScore[0].sprite = playerBatSprites[3];
                playerBats[0].sprite = playerBatSprites[3];
                break;
            case 4:
                playerBatsInScore[0].sprite = playerBatSprites[4];
                playerBats[0].sprite = playerBatSprites[4];
                break;
        }

        switch (MainMenu.player2BatNumber)
        {
            case 0:
                playerBatsInScore[1].sprite = playerBatSprites[0];
                playerBats[1].sprite = playerBatSprites[0];
                break;
            case 1:
                playerBatsInScore[1].sprite = playerBatSprites[1];
                playerBats[1].sprite = playerBatSprites[1];
                break;
            case 2:
                playerBatsInScore[1].sprite = playerBatSprites[2];
                playerBats[1].sprite = playerBatSprites[2];
                break;
            case 3:
                playerBatsInScore[1].sprite = playerBatSprites[3];
                playerBats[1].sprite = playerBatSprites[3];
                break;
            case 4:
                playerBatsInScore[1].sprite = playerBatSprites[4];
                playerBats[1].sprite = playerBatSprites[4];
                break;
        }

        if (!MainMenu.isShowBonusCard)
        {
            bonusPans[0].SetActive(false);
            bonusPans[1].SetActive(false);
        }

        if (!MainMenu.isShowTacticCard)
        {
            strokePans[0].SetActive(false);
            strokePans[1].SetActive(false);
        }

        for (int i = 0; i < MultilangStrings.Instance.tacticCardTitleEngStrings.Count; i++)
        {
            if (MainMenu.isEnglish)
            {
                tacticCardTitle1[i].text = MultilangStrings.Instance.tacticCardTitleEngStrings[i];
                tacticCardTitle2[i].text = MultilangStrings.Instance.tacticCardTitleEngStrings[i];
                tacticCardDes1[i].text = MultilangStrings.Instance.tacticCardDescriptionEngStrings[i];
                tacticCardDes2[i].text = MultilangStrings.Instance.tacticCardDescriptionEngStrings[i];
            }
            else if (MainMenu.isSweeden)
            {
                tacticCardTitle1[i].text = MultilangStrings.Instance.tacticCardTitleSwedStrings[i];
                tacticCardTitle2[i].text = MultilangStrings.Instance.tacticCardTitleSwedStrings[i];
                tacticCardDes1[i].text = MultilangStrings.Instance.tacticCardDescriptionSwedStrings[i];
                tacticCardDes2[i].text = MultilangStrings.Instance.tacticCardDescriptionSwedStrings[i];

            }
        }

        for (int i = 0; i < MultilangStrings.Instance.othersEngStrings.Count; i++)
        {
            if (MainMenu.isEnglish)
            {
                if (i == 1 && MainMenu.isAI)
                {
                    others[i].text = "Smash Please choose a tactic card";
                    
                }
                else
                {
                    others[i].text = MultilangStrings.Instance.othersEngStrings[i];
                }                
            }
            else if (MainMenu.isSweeden)
            {
                others[i].text = MultilangStrings.Instance.othersSwedStrings[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bonusCard1Click()
    {
        if (bonusPans[0].GetComponent<Image>().sprite == bonusCardSprites[0])
        {
            bonusCardSet[0].SetActive(true);
        }
    }

    public void bonusCard2Click()
    {
        if (bonusPans[1].GetComponent<Image>().sprite == bonusCardSprites[0])
        {
            bonusCardSet[1].SetActive(true);
        }
    }

    public void ReturnButtonClick()
    {
        Application.LoadLevel("MainMenu");
    }
}
