using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class TacticCards : MonoBehaviour, IEventListener
{
    public UISprite BG;
    private bool isOpening;
    public TacticCardView[] tacticsCards;
    public UISprite ZoomBtn;
    Tweener[] zoomInTweens;
    Tweener[] zoomOutTweens;

    void Start()
    {
        UIEventListener.Get(ZoomBtn.gameObject).onClick += ToggleZoom;
        RegisterEvent();
        initTweener();
    }

    private void RegisterEvent()
    {
        EventManager.Instance().attach(InGameEventType.OPEN_TACTIC_CARD, this);
        EventManager.Instance().attach(InGameEventType.CLOSE_TACTIC_CARD, this);
        EventManager.Instance().attach(InGameEventType.SET_ACTIVE_TACTIC_CARD, this);
        EventManager.Instance().attach(InGameEventType.SET_DATA_TACTIC_CARD, this);
        EventManager.Instance().attach(InGameEventType.REMOVE_ACTIVE_TACTIC_CARD, this);
    }

    private void initTweener()
    {
        zoomInTweens = new Tweener[tacticsCards.Length];
        zoomOutTweens = new Tweener[tacticsCards.Length];
    }

    public void handle(InGameEventType aEventType, object aEventData)
    {
        switch (aEventType)
        {
            case InGameEventType.OPEN_TACTIC_CARD:
                Open();
                break;
            case InGameEventType.CLOSE_TACTIC_CARD:
                Close();
                break;
            case InGameEventType.SET_ACTIVE_TACTIC_CARD:
                TacticCardType activeType = (TacticCardType)aEventData;
                foreach( var tacticCard in tacticsCards )
                {
                    if (tacticCard.type == activeType)
                    {
                        tacticCard.SetActive(true);
                    }
                    else
                        tacticCard.SetActive(false);
                }
                break;
            case InGameEventType.SET_DATA_TACTIC_CARD:
                Dictionary<TacticCardType, int> tacticMap = (Dictionary<TacticCardType, int>)aEventData;
                foreach (var tacticCard in tacticsCards)
                {
                    if (tacticMap.ContainsKey(tacticCard.type))
                    {
                        //Debug.Log("Type: " + tacticCard.type + ": " + tacticMap[tacticCard.type]);
                        var point = tacticMap[tacticCard.type];
                        if( point >= 0 )
                        {
                            tacticCard.SetChoosable(true);
                            tacticCard.SetPoint(point);
                        }
                        else
                        {
                            tacticCard.SetPoint(0);
                            tacticCard.SetChoosable(false);
                        }
                    }
                    else
                        tacticCard.SetPoint(0);
                }

                break;
            case InGameEventType.REMOVE_ACTIVE_TACTIC_CARD:
                foreach (var tacticCard in tacticsCards)
                {
                        tacticCard.SetActive(false);
                }
                break;
        }
    }

    public void Close()
    {
        BG.gameObject.SetActive(false);
        isOpening = false;
        foreach (var tacticCard in tacticsCards)
        {
            tacticCard.GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void Open()
    {
        if (!isOpening)
        {
            BG.gameObject.SetActive(true);
            isOpening = true;
            foreach( var tacticCard in tacticsCards )
            {
                tacticCard.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
    private void ToggleZoom(GameObject gameObject)
    {
        if (ZoomBtn.spriteName == "zoom_in")
        {
            ZoomBtn.spriteName = "zoom_out";
            foreach( var zoomOutTween in zoomOutTweens )
                if (zoomOutTween != null)
                    zoomOutTween.Kill();
            for( int i = 0; i < tacticsCards.Length; i++ )
            {
                zoomInTweens[i] = tacticsCards[i].transform.DOScale(1.6f, 0.5f);
            }
        }
        else
        {
            ZoomBtn.spriteName = "zoom_in";
            foreach (var zoomInTween in zoomInTweens)
                if (zoomInTween != null)
                    zoomInTween.Kill();
            for (int i = 0; i < tacticsCards.Length; i++)
            {
                zoomOutTweens[i] = tacticsCards[i].transform.DOScale(1f, 0.5f);
            }
        }
    }
}
