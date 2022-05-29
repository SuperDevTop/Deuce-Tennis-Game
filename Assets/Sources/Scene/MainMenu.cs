using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{    
    public Text[] texts;
    public static bool isEnglish;
    public static bool isSweeden;
    public static bool isRollAnimatin;
    public static bool isAimGrid;
    public static bool isShowBonusCard;
    public static bool isShowTacticCard;
    public static string player1Name = "Player1";
    public static string player2Name = "Player2";
    public static int strokeNumber;
    public static int player1BatNumber = 0;
    public static int player2BatNumber = 1;
    public static int taticGoalCount;
    public static bool isAI = false;

    void Awake()
    {                
        //print("START!!!!!");
        //isEnglish = true;
        //isSweeden = false;
        //isRollAnimatin = true;
        //isAimGrid = true;
        //isShowBonusCard = true;
        //isShowTacticCard = true;
        //player1Name = "Player1";
        //player2Name = "Player2";
        //strokeNumber = 2;
        //player1BatNumber = 0;
        //player2BatNumber = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        isAI = false;               

        if (PlayerPrefs.GetString("isFirstPlay") != "true" && PlayerPrefs.GetString("isFirstPlay") != "false")
        {
            PlayerPrefs.SetString("isFirstPlay", "true");
            PlayerPrefs.SetString("taticGoalCount", "5");
            PlayerPrefs.SetString("strokeNumber", "2");
            PlayerPrefs.SetString("isEnglish", "true");
            PlayerPrefs.SetString("isRollAnimatin", "true");
            PlayerPrefs.SetString("isAimGrid", "true");
            PlayerPrefs.SetString("isShowBonusCard", "true");
            PlayerPrefs.SetString("isShowTacticCard", "true");
        }        
    }

    // Update is called once per frame
    void Update()
    {
        //print(MainMenu.strokeNumber);


        if (PlayerPrefs.GetString("isEnglish") == "true")
        {
            isEnglish = true;
            isSweeden = false;
        }
        else if (PlayerPrefs.GetString("isEnglish") == "false")
        {
            isEnglish = false;
            isSweeden = true;
        }

        if (PlayerPrefs.GetString("isRollAnimatin") == "true")
        {
            isRollAnimatin = true;            
        }
        else if (PlayerPrefs.GetString("isRollAnimatin") == "false")
        {
            isRollAnimatin = false;            
        }

        if (PlayerPrefs.GetString("isAimGrid") == "true")
        {
            isAimGrid = true;
        }
        else if (PlayerPrefs.GetString("isAimGrid") == "false")
        {
            isAimGrid = false;
        }

        if (PlayerPrefs.GetString("isShowBonusCard") == "true")
        {
            isShowBonusCard = true;
        }
        else if (PlayerPrefs.GetString("isShowBonusCard") == "false")
        {
            isShowBonusCard = false;
        }

        if (PlayerPrefs.GetString("isShowTacticCard") == "true")
        {
            isShowTacticCard = true;
        }
        else if (PlayerPrefs.GetString("isShowTacticCard") == "false")
        {
            isShowTacticCard = false;
        }

        switch (PlayerPrefs.GetString("taticGoalCount"))
        {
            case "3":
                taticGoalCount = 3;
                break;
            case "4":
                taticGoalCount = 4;
                break;
            case "5":
                taticGoalCount = 5;
                break;
            case "6":
                taticGoalCount = 6;
                break;
        }

        switch (PlayerPrefs.GetString("strokeNumber"))
        {
            case "0":
                strokeNumber = 0;
                break;
            case "1":
                strokeNumber = 1;
                break;
            case "2":
                strokeNumber = 2;
                break;
            case "3":
                strokeNumber = 3;
                break;
            case "1000":
                strokeNumber = 1000;
                break;
        }


        //print("IsEnglish:" + isEnglish);

        for (int i = 0; i < MultilangStrings.Instance.mainMenuEngStrings.Count;i++)
        {
            if (isEnglish)
            {
                texts[i].text = MultilangStrings.Instance.mainMenuEngStrings[i];
            }
            else
            {
                texts[i].text = MultilangStrings.Instance.mainMenuSwedStrings[i];
            }
        }
            
    }

    public void PlayButtonClick()
    {        
        Application.LoadLevel("Game1");        
    }

    public void PlayAIButtonClick()
    {
        isAI = true;
        player2Name = "Smash";
        Application.LoadLevel("GameAI");
    }

    public void SettingButtonClick()
    {
        Application.LoadLevel("Settings");
    }

    public void PlayRedClick()
    {
        Application.LoadLevel("GameRed1");
    }

    public void PlayOrangeClick()
    {
        Application.LoadLevel("GameOrange1");
    }
}
