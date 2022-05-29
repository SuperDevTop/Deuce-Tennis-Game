using UnityEngine;
using System.Collections;
using DG.Tweening;
public enum StrokeCardType
{
    DROP_SHOT, LOB_SHOT, POWER, SLICE, TOP_SPIN
}
public class StrokeCardView : MonoBehaviour {

    public StrokeCardType Type;
    public UISprite Image;
	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(gameObject).onClick += ClickHandle;
	    switch(Type)
        {
            case StrokeCardType.DROP_SHOT:
                Image.spriteName = "stroke_dropshot2";
                break;
            case StrokeCardType.LOB_SHOT:
                Image.spriteName = "stroke_lobshot2";
                break;
            case StrokeCardType.POWER:
                Image.spriteName = "stroke_power2";
                break;
            case StrokeCardType.SLICE:
                Image.spriteName = "stroke_slice2";
                break;
            case StrokeCardType.TOP_SPIN:
                Image.spriteName = "stroke_topspin2";
                break;
        }
	}
	
    public void ClickHandle(GameObject gameObject)
    {
        transform.DOScale(1.2f, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(ClickEndEvent);
    }

    public void ClickEndEvent()
    {
        Debug.Log("StrokeCardView: " + Type);
        EventManager.Instance().fire(InGameEventType.CHOOSE_STROKE_CARD, Type);
    }

}
