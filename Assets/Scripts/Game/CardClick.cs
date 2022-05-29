using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {        
        this.GetComponent<Button>().onClick.AddListener(() => ButtonClick());        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick()
    {
        switch (this.tag)
        {
            case "Tactic Card1":
                GameEngine.Instance.playerAvatars[0].sprite = GameEngine.Instance.tacticCardSprites[0];
                GameEngine.Instance.isPlayer1SetNet = true;

                if (MainMenu.isEnglish)
                {
                    GameEngine.Instance.playerAvatars[0].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleEngStrings[0];
                }
                else
                {
                    GameEngine.Instance.playerAvatars[0].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleSwedStrings[0];
                }

                break;
            case "Tactic Card2":
                GameEngine.Instance.playerAvatars[0].sprite = GameEngine.Instance.tacticCardSprites[1];
                GameEngine.Instance.isPlayer1SetDefensive = true;

                if (MainMenu.isEnglish)
                {
                    GameEngine.Instance.playerAvatars[0].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleEngStrings[1];
                }
                else
                {
                    GameEngine.Instance.playerAvatars[0].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleSwedStrings[1];
                }
                break;
            case "Tactic Card3":
                GameEngine.Instance.playerAvatars[0].sprite = GameEngine.Instance.tacticCardSprites[2];
                GameEngine.Instance.isPlayer1SetAccurate = true;

                if (MainMenu.isEnglish)
                {
                    GameEngine.Instance.playerAvatars[0].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleEngStrings[2];
                }
                else
                {
                    GameEngine.Instance.playerAvatars[0].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleSwedStrings[2];
                }

                break;
            case "Tactic Card4":
                GameEngine.Instance.playerAvatars[0].sprite = GameEngine.Instance.tacticCardSprites[3];
                GameEngine.Instance.isPlayer1SetOffensive = true;

                if (MainMenu.isEnglish)
                {
                    GameEngine.Instance.playerAvatars[0].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleEngStrings[3];
                }
                else
                {
                    GameEngine.Instance.playerAvatars[0].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleSwedStrings[3];
                }

                break;
            case "Tactic Card5":
                GameEngine.Instance.playerAvatars[0].sprite = GameEngine.Instance.tacticCardSprites[4];
                GameEngine.Instance.isPlayer1SetSmart = true;

                if (MainMenu.isEnglish)
                {
                    GameEngine.Instance.playerAvatars[0].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleEngStrings[4];
                }
                else
                {
                    GameEngine.Instance.playerAvatars[0].GetComponentInChildren<Text>().text = MultilangStrings.Instance.tacticCardTitleSwedStrings[4];
                }

                break;
            case "Tactic Card6":
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
            case "Tactic Card7":
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
            case "Tactic Card8":
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
            case "Tactic Card9":
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
            case "Tactic Card10":                
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
    }
}
