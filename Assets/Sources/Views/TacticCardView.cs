using UnityEngine;
using System.Collections;
using DG.Tweening;

public enum TacticCardType
{
    TO_THE_NET, ON_THE_DEFENSIVE, DOWN_THE_LINE, ON_THE_OFFENSIVE, SMART_GAME
}

public class TacticCardView : MonoBehaviour 
{
    public TacticCardType type;
    public UISprite Image;
    public UISprite ChoosenBG;
    public UISprite[] points;
    public UISprite Choosable;

    void Start()
    {
        UIEventListener.Get(gameObject).onClick += ClickHandle;
        switch (type)
        {
            case TacticCardType.DOWN_THE_LINE:
                Image.spriteName = "tacticscard_downtheline_zoom";
                break;
            case TacticCardType.ON_THE_DEFENSIVE:
                Image.spriteName = "tacticscard_defensive_zoom";
                break;
            case TacticCardType.ON_THE_OFFENSIVE:
                Image.spriteName = "tacticscard_offensive_zoom";
                break;
            case TacticCardType.SMART_GAME:
                Image.spriteName = "tacticscard_smartgame_zoom";
                break;
            case TacticCardType.TO_THE_NET:
                Image.spriteName = "tacticscard_tothenet_zoom";
                break;
        }
    }

    public void SetPoint(int point)
    {
        if (point < 0)
            point = 0;
        if (point > 5)
            point = 5;

        for( int i = 0; i < point; i++ )
        {
            points[i].gameObject.SetActive(true);
        }
        for (int i = point; i < 5; i++)
        {
            points[i].gameObject.SetActive(false);
        }
    }

    public void SetActive(bool isActivated)
    {
        ChoosenBG.gameObject.SetActive(isActivated);
    }

    public void SetChoosable( bool isChoosable)
    {
        Choosable.gameObject.SetActive(!isChoosable);
    }
    public void ClickHandle(GameObject gameObject)
    {
        transform.DOScale(1.2f, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(ClickEndEvent);
    }

    public void ClickEndEvent()
    {
        Debug.Log("TacticCardView: " + type);
        EventManager.Instance().fire(InGameEventType.CHOOSE_TACTIC_CARD, type);
    }

}
