using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEngine : MonoBehaviour
{
    public static TutorialEngine Instance;
    public Text[] demoTexts;
    public Text[] tacticTitles;
    public Text[] tacticDess;
    public Text[] extraTacticTitles;
    public GameObject[] tutorials;
    public GameObject[] tutorialsWithText;
    public GameObject[] tutorialsWithAction;
    public GameObject[] tutorialsWithBoth;
    public GameObject currentActiveGameobject;
    public GameObject tutorialObject;
    public GameObject playObject;
    public Image skipImage;
    public Image[] roleImages;
    public Image[] specialImages;
    public Image[] strokeImages;
    public Image bonusImage;
    public Sprite[] skipSprites;
    public Sprite[] bonusSprites;
    public bool isChanged;

    int index = 0;
    int savedIndex;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < demoTexts.Length; i++)
        {
            if (MainMenu.isEnglish)
            {
                demoTexts[i].text = MultilangStrings.Instance.demoEngStrings[i];
            }
            else if (MainMenu.isSweeden)
            {
                demoTexts[i].text = MultilangStrings.Instance.demoSwedStrings[i];
            }
        }

        if (MainMenu.isEnglish)
        {           
            skipImage.sprite = skipSprites[0];

            for (int i = 0; i < tacticTitles.Length; i++)
            {
                tacticTitles[i].text = MultilangStrings.Instance.tacticCardTitleEngStrings[i % 5];
                tacticDess[i].text = MultilangStrings.Instance.tacticCardDescriptionEngStrings[i % 5];
            }

            for (int i = 0; i < 42; i++)
            {
                extraTacticTitles[i * 2].text = "SMART PLAY";
                extraTacticTitles[i * 2 + 1].text = "ACCURATE FINISHING";
            }

            for (int i = 42; i < 48; i++)
            {
                extraTacticTitles[i * 2].text = "SMART PLAY";
                extraTacticTitles[i * 2 + 1].text = "ON THE OFFENSIVE";
            }

            for (int i = 0; i < 48; i++)
            {
                specialImages[i * 2].sprite = GameScene.Instance.strokeEngButton[0];
                specialImages[i * 2 + 1].sprite = GameScene.Instance.strokeEngButton[1];
            }

            for (int i = 0; i < strokeImages.Length; i++)
            {
                strokeImages[i].sprite = GameScene.Instance.strokeEngCards[i % 5];
            }

            bonusImage.sprite = bonusSprites[0];

            roleImages[0].sprite = GameEngine.Instance.coach1Commands[6];
            roleImages[1].sprite = GameEngine.Instance.coach2Commands[6];
            roleImages[2].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[3].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[4].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[5].sprite = GameEngine.Instance.coach1Commands[6];
            roleImages[6].sprite = GameEngine.Instance.coach2Commands[6];
            roleImages[7].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[8].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[9].sprite = GameEngine.Instance.coach1Commands[5];
            roleImages[10].sprite = GameEngine.Instance.coach2Commands[3];
            roleImages[11].sprite = GameEngine.Instance.coach2Commands[3];
            roleImages[12].sprite = GameEngine.Instance.coach2Commands[2];
            roleImages[13].sprite = GameEngine.Instance.coach2Commands[2];
            roleImages[14].sprite = GameEngine.Instance.coach2Commands[2];
            roleImages[15].sprite = GameEngine.Instance.coach1Commands[6];
            roleImages[16].sprite = GameEngine.Instance.coach2Commands[6];
            roleImages[17].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[18].sprite = GameEngine.Instance.coach1Commands[8];
            roleImages[19].sprite = GameEngine.Instance.coach1Commands[6];
            roleImages[20].sprite = GameEngine.Instance.coach2Commands[6];
            roleImages[21].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[22].sprite = GameEngine.Instance.coach1Commands[8];
            roleImages[23].sprite = GameEngine.Instance.coach1Commands[6];
            roleImages[24].sprite = GameEngine.Instance.coach2Commands[6];
            roleImages[25].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[26].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[27].sprite = GameEngine.Instance.coach2Commands[7];
            roleImages[28].sprite = GameEngine.Instance.coach1Commands[6];
            roleImages[29].sprite = GameEngine.Instance.coach2Commands[6];
            roleImages[30].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[31].sprite = GameEngine.Instance.coach1Commands[8];
            roleImages[32].sprite = GameEngine.Instance.coach2Commands[3];
            roleImages[33].sprite = GameEngine.Instance.coach2Commands[2];
            roleImages[34].sprite = GameEngine.Instance.coach2Commands[2];
            roleImages[35].sprite = GameEngine.Instance.coach2Commands[4];
            roleImages[36].sprite = GameEngine.Instance.coach1Commands[3];
            roleImages[37].sprite = GameEngine.Instance.coach1Commands[3];
            roleImages[38].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[39].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[40].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[41].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[42].sprite = GameEngine.Instance.coach2Commands[7];
            roleImages[43].sprite = GameEngine.Instance.coach1Commands[6];
            roleImages[44].sprite = GameEngine.Instance.coach2Commands[6];
            roleImages[45].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[46].sprite = GameEngine.Instance.coach1Commands[2];
            roleImages[47].sprite = GameEngine.Instance.coach1Commands[7];
            
        }
        else if (MainMenu.isSweeden)
        {            
            skipImage.sprite = skipSprites[1];

            for (int i = 0; i < tacticTitles.Length; i++)
            {
                tacticTitles[i].text = MultilangStrings.Instance.tacticCardTitleSwedStrings[i % 5];
                tacticDess[i].text = MultilangStrings.Instance.tacticCardDescriptionSwedStrings[i % 5];
            }

            for (int i = 0; i < 42; i++)
            {
                extraTacticTitles[i * 2].text = "Smartspel";
                extraTacticTitles[i * 2 + 1].text = "Precisionspel";
            }

            for (int i = 42; i < 48; i++)
            {
                extraTacticTitles[i * 2].text = "Smartspel";
                extraTacticTitles[i * 2 + 1].text = "Offensivtspel";
            }

            for (int i = 0; i < 48; i++)
            {
                specialImages[i * 2].sprite = GameScene.Instance.strokeSwedButton[0];
                specialImages[i * 2 + 1].sprite = GameScene.Instance.strokeSwedButton[1];
            }

            for (int i = 0; i < strokeImages.Length; i++)
            {
                strokeImages[i].sprite = GameScene.Instance.strokeEngCards[i % 5];
            }

            bonusImage.sprite = bonusSprites[1];

            roleImages[0].sprite = GameEngine.Instance.coach11Commands[6];
            roleImages[1].sprite = GameEngine.Instance.coach22Commands[6];
            roleImages[2].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[3].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[4].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[5].sprite = GameEngine.Instance.coach11Commands[6];
            roleImages[6].sprite = GameEngine.Instance.coach22Commands[6];
            roleImages[7].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[8].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[9].sprite = GameEngine.Instance.coach11Commands[5];
            roleImages[10].sprite = GameEngine.Instance.coach22Commands[3];
            roleImages[11].sprite = GameEngine.Instance.coach22Commands[3];
            roleImages[12].sprite = GameEngine.Instance.coach22Commands[2];
            roleImages[13].sprite = GameEngine.Instance.coach22Commands[2];
            roleImages[14].sprite = GameEngine.Instance.coach22Commands[2];
            roleImages[15].sprite = GameEngine.Instance.coach11Commands[6];
            roleImages[16].sprite = GameEngine.Instance.coach22Commands[6];
            roleImages[17].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[18].sprite = GameEngine.Instance.coach11Commands[8];
            roleImages[19].sprite = GameEngine.Instance.coach11Commands[6];
            roleImages[20].sprite = GameEngine.Instance.coach22Commands[6];
            roleImages[21].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[22].sprite = GameEngine.Instance.coach11Commands[8];
            roleImages[23].sprite = GameEngine.Instance.coach11Commands[6];
            roleImages[24].sprite = GameEngine.Instance.coach22Commands[6];
            roleImages[25].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[26].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[27].sprite = GameEngine.Instance.coach22Commands[7];
            roleImages[28].sprite = GameEngine.Instance.coach11Commands[6];
            roleImages[29].sprite = GameEngine.Instance.coach22Commands[6];
            roleImages[30].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[31].sprite = GameEngine.Instance.coach11Commands[8];
            roleImages[32].sprite = GameEngine.Instance.coach22Commands[3];
            roleImages[33].sprite = GameEngine.Instance.coach22Commands[2];
            roleImages[34].sprite = GameEngine.Instance.coach22Commands[2];
            roleImages[35].sprite = GameEngine.Instance.coach22Commands[4];
            roleImages[36].sprite = GameEngine.Instance.coach11Commands[3];
            roleImages[37].sprite = GameEngine.Instance.coach11Commands[3];
            roleImages[38].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[39].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[40].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[41].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[42].sprite = GameEngine.Instance.coach22Commands[7];
            roleImages[43].sprite = GameEngine.Instance.coach11Commands[6];
            roleImages[44].sprite = GameEngine.Instance.coach22Commands[6];
            roleImages[45].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[46].sprite = GameEngine.Instance.coach11Commands[2];
            roleImages[47].sprite = GameEngine.Instance.coach11Commands[7];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isChanged)
        {
            isChanged = false;

            GetActiveObject();

            if (!IsTutorialWithText() && !IsTutorialWithAction())
            {
                StartCoroutine("SkipAutomatically");
                savedIndex = index;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            GetActiveObject();

            if (index == 61)
            {
                if (Application.loadedLevelName == "Game1" || Application.loadedLevelName == "GameAI")
                {
                    tutorialObject.SetActive(false);
                    playObject.SetActive(true);
                }
            }
            else
            {
                if (IsTutorialWithText())
                {
                    if (!IsTutorialWithBoth())
                    {
                        if (demoTexts[GetIndex()].GetComponent<TypeWriter>().isCompleteSentence)
                        {
                            tutorials[index].SetActive(false);
                            tutorials[index + 1].SetActive(true);
                            isChanged = true;
                        }
                    }                    
                }
                else if (IsTutorialWithAction())
                {
                }
                else
                {
                    tutorials[index].SetActive(false);
                    tutorials[index + 1].SetActive(true);
                    isChanged = true;
                }
            }
           
        }
    }

    public void GetActiveObject()
    {
        for (int i = 0; i < tutorials.Length; i++)
        {
            if (tutorials[i].active)
            {
                currentActiveGameobject = tutorials[i];
                index = i;
            }
        }
    }

    public bool IsTutorialWithText()
    {
        if (Array.Exists(tutorialsWithText, element => element == currentActiveGameobject))
        {
            return true;
        }        
        else
        {
            return false;
        }
    }

    public bool IsTutorialWithAction()
    {
        if (Array.Exists(tutorialsWithAction, element => element == currentActiveGameobject))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsTutorialWithBoth()
    {
        if (Array.Exists(tutorialsWithBoth, element => element == currentActiveGameobject))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public int GetIndex()
    {
        return Array.IndexOf(tutorialsWithText, currentActiveGameobject);
    }

    public void TutorialClick()
    {
        GetActiveObject();

        if (IsTutorialWithText())
        {
            if (demoTexts[GetIndex()].GetComponent<TypeWriter>().isCompleteSentence)
            {
                tutorials[index].SetActive(false);
                tutorials[index + 1].SetActive(true);
                isChanged = true;
            }
        }
        else
        {            
            tutorials[index].SetActive(false);
            tutorials[index + 1].SetActive(true);
            isChanged = true;          
        }
    }

    IEnumerator SkipAutomatically()
    {
        yield return new WaitForSeconds(2);

        if (savedIndex == index)
        {
            tutorials[index].SetActive(false);
            tutorials[index + 1].SetActive(true);
        }        
    }
}
