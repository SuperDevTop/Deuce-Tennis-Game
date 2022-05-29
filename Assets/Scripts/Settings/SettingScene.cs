using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScene : MonoBehaviour
{
    public static SettingScene Instance;    
    public Text player1Text;
    public Text player2Text;
    public Text[] texts;
    public GameObject[] languageObjects;
    public GameObject[] rollObjects;
    public GameObject[] aimObjects;
    public GameObject[] bonuscardObjects;
    public GameObject[] tacticcardObjects;
    public GameObject[] specialstrokeObjects;
    public GameObject[] tacticgoalObjects;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //print(MainMenu.player2Name);

        if (player1Text.text == "")
        {
           MainMenu.player1Name = "Player1";
        }
        else
        {
            MainMenu.player1Name = player1Text.text;
        }

        if (player2Text.text == "")
        {
            MainMenu.player2Name = "Player2";
        }
        else
        {
            MainMenu.player2Name = player2Text.text;
        }

        for (int i = 0; i < MultilangStrings.Instance.settingsEngStrings.Count; i++)
        {
            if (MainMenu.isEnglish)
            {
                texts[i].text = MultilangStrings.Instance.settingsEngStrings[i];
            }
            else if(MainMenu.isSweeden)
            {
                texts[i].text = MultilangStrings.Instance.settingsSwedStrings[i];
            }
        }

        if (PlayerPrefs.GetString("isEnglish") == "true")
        {
            languageObjects[0].SetActive(true);
            languageObjects[1].SetActive(false);
            languageObjects[2].SetActive(false);
            languageObjects[3].SetActive(true);
        }
        else if (PlayerPrefs.GetString("isEnglish") == "false")
        {
            languageObjects[0].SetActive(false);
            languageObjects[1].SetActive(true);
            languageObjects[2].SetActive(true);
            languageObjects[3].SetActive(false);
        }

        if (PlayerPrefs.GetString("isRollAnimatin") == "true")
        {
            rollObjects[0].SetActive(true);
            rollObjects[1].SetActive(false);
        }
        else if (PlayerPrefs.GetString("isRollAnimatin") == "false")
        {
            rollObjects[0].SetActive(false);
            rollObjects[1].SetActive(true);
        }

        if (PlayerPrefs.GetString("isAimGrid") == "true")
        {
            aimObjects[0].SetActive(true);
            aimObjects[1].SetActive(false);
        }
        else if (PlayerPrefs.GetString("isAimGrid") == "false")
        {
            aimObjects[0].SetActive(false);
            aimObjects[1].SetActive(true);
        }

        if (PlayerPrefs.GetString("isShowBonusCard") == "true")
        {
            bonuscardObjects[0].SetActive(true);
            bonuscardObjects[1].SetActive(false);
        }
        else if (PlayerPrefs.GetString("isShowBonusCard") == "false")
        {
            bonuscardObjects[0].SetActive(false);
            bonuscardObjects[1].SetActive(true);
        }

        if (PlayerPrefs.GetString("isShowTacticCard") == "true")
        {
            tacticcardObjects[0].SetActive(true);
            tacticcardObjects[1].SetActive(false);
        }
        else if (PlayerPrefs.GetString("isShowTacticCard") == "false")
        {
            tacticcardObjects[0].SetActive(false);
            tacticcardObjects[1].SetActive(true);
        }

        switch (PlayerPrefs.GetString("taticGoalCount"))
        {            
            case "3":
                tacticgoalObjects[0].SetActive(true);
                tacticgoalObjects[1].SetActive(false);
                tacticgoalObjects[2].SetActive(false);
                tacticgoalObjects[3].SetActive(false);
                break;
            case "4":
                tacticgoalObjects[0].SetActive(false);
                tacticgoalObjects[1].SetActive(true);
                tacticgoalObjects[2].SetActive(false);
                tacticgoalObjects[3].SetActive(false);
                break;
            case "5":
                tacticgoalObjects[0].SetActive(false);
                tacticgoalObjects[1].SetActive(false);
                tacticgoalObjects[2].SetActive(true);
                tacticgoalObjects[3].SetActive(false);
                break;
            case "6":
                tacticgoalObjects[0].SetActive(false);
                tacticgoalObjects[1].SetActive(false);
                tacticgoalObjects[2].SetActive(false);
                tacticgoalObjects[3].SetActive(true);
                break;
        }

        switch (PlayerPrefs.GetString("strokeNumber"))
        {
            case "0":
                specialstrokeObjects[0].SetActive(true);
                specialstrokeObjects[1].SetActive(false);
                specialstrokeObjects[2].SetActive(false);
                specialstrokeObjects[3].SetActive(false);
                specialstrokeObjects[4].SetActive(false);
                break;
            case "1":
                specialstrokeObjects[0].SetActive(false);
                specialstrokeObjects[1].SetActive(true);
                specialstrokeObjects[2].SetActive(false);
                specialstrokeObjects[3].SetActive(false);
                specialstrokeObjects[4].SetActive(false);
                break;
            case "2":
                specialstrokeObjects[0].SetActive(false);
                specialstrokeObjects[1].SetActive(false);
                specialstrokeObjects[2].SetActive(true);
                specialstrokeObjects[3].SetActive(false);
                specialstrokeObjects[4].SetActive(false);
                break;
            case "3":
                specialstrokeObjects[0].SetActive(false);
                specialstrokeObjects[1].SetActive(false);
                specialstrokeObjects[2].SetActive(false);
                specialstrokeObjects[3].SetActive(true);
                specialstrokeObjects[4].SetActive(false);
                break;
            case "1000":
                specialstrokeObjects[0].SetActive(false);
                specialstrokeObjects[1].SetActive(false);
                specialstrokeObjects[2].SetActive(false);
                specialstrokeObjects[3].SetActive(false);
                specialstrokeObjects[4].SetActive(true);
                break;
        }
    }

    public void ShowDemoButtionClick()
    {
        //PlayerPrefs.SetString("isFirstPlay", "true");
        Application.LoadLevel("Tutorial");
    }

    public void OnRollAnimation()
    {
        PlayerPrefs.SetString("isRollAnimatin", "true");
        MainMenu.isRollAnimatin = true;
    }

    public void OffRollAnimation()
    {
        PlayerPrefs.SetString("isRollAnimatin", "false");
        MainMenu.isRollAnimatin = false;
    }

    public void OnAimGrid()
    {
        PlayerPrefs.SetString("isAimGrid", "true");
        MainMenu.isAimGrid = true;
    }

    public void OffAimGrid()
    {
        PlayerPrefs.SetString("isAimGrid", "false");
        MainMenu.isAimGrid = false;
    }

    public void BackButtonClick()
    {
        Application.LoadLevel("MainMenu");
    }

    public void SetOnEnglish()
    {
        PlayerPrefs.SetString("isEnglish", "true");
        MainMenu.isEnglish = true;
        MainMenu.isSweeden = false;
    }

    public void SetOnSweeden()
    {
        PlayerPrefs.SetString("isEnglish", "false");
        MainMenu.isEnglish = false;
        MainMenu.isSweeden = true;
    }

    public void Green1Click()
    {
        MainMenu.player1BatNumber = 0;
        player1Text.color = Color.green;
    }

    public void Purple1Click()
    {
        MainMenu.player1BatNumber = 1;
        player1Text.color = new Color(212/255f, 21/255f , 205/255f);
    }

    public void Black1Click()
    {
        MainMenu.player1BatNumber = 2;
        player1Text.color = Color.black;
    }

    public void Yellow1Click()
    {
        MainMenu.player1BatNumber = 3;
        player1Text.color = Color.yellow;
    }
    public void Blue1Click()
    {
        MainMenu.player1BatNumber = 4;
        player1Text.color = Color.blue;
    }

    public void Green2Click()
    {
        MainMenu.player2BatNumber = 0;
        player2Text.color = Color.green;
    }

    public void Purple2Click()
    {
        MainMenu.player2BatNumber = 1;
        player2Text.color = new Color(212 / 255f, 21 / 255f, 205 / 255f);
    }

    public void Black2Click()
    {
        MainMenu.player2BatNumber = 2;
        player2Text.color = Color.black;
    }

    public void Yellow2Click()
    {
        MainMenu.player2BatNumber = 3;
        player2Text.color = Color.yellow;
    }
    public void Blue2Click()
    {
        MainMenu.player2BatNumber = 4;
        player2Text.color = Color.blue;
    }

    public void Stroke0Click()
    {
        PlayerPrefs.SetString("strokeNumber", "0");
        MainMenu.strokeNumber = 0;
    }

    public void Stroke1Click()
    {
        PlayerPrefs.SetString("strokeNumber", "1");
        MainMenu.strokeNumber = 1;
    }

    public void Stroke2Click()
    {
        PlayerPrefs.SetString("strokeNumber", "2");
        MainMenu.strokeNumber = 2;
    }

    public void Stroke3Click()
    {
        PlayerPrefs.SetString("strokeNumber", "3");
        MainMenu.strokeNumber = 3;
    }

    public void StrokeUnlClick()
    {
        PlayerPrefs.SetString("strokeNumber", "1000");
        MainMenu.strokeNumber = 1000;
    }

    public void OnBonusCard()
    {
        PlayerPrefs.SetString("isShowBonusCard", "true");
        MainMenu.isShowBonusCard = true;
    }

    public void OffBonusCard()
    {
        PlayerPrefs.SetString("isShowBonusCard", "false");
        MainMenu.isShowBonusCard = false;
    }

    public void OnTacticCard()
    {
        PlayerPrefs.SetString("isShowTacticCard", "true");
        MainMenu.isShowTacticCard = true;
    }

    public void OffTacticCard()
    {
        PlayerPrefs.SetString("isShowTacticCard", "false");
        MainMenu.isShowTacticCard = false;
    }

    public void TacticGoal3Click()
    {
        PlayerPrefs.SetString("taticGoalCount", "3");
        MainMenu.taticGoalCount = 3;
    }

    public void TacticGoal4Click()
    {
        PlayerPrefs.SetString("taticGoalCount", "4");
        MainMenu.taticGoalCount = 4;
    }

    public void TacticGoal5Click()
    {
        PlayerPrefs.SetString("taticGoalCount", "5");
        MainMenu.taticGoalCount = 5;
    }

    public void TacticGoal6Click()
    {
        PlayerPrefs.SetString("taticGoalCount", "6");
        MainMenu.taticGoalCount = 6;
    }
}
