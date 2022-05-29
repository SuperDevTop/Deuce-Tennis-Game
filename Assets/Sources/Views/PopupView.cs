using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PopupView : MonoBehaviour , IEventListener
{
    public UILabel Text;
    private UIWidget thisWidget;
    Tweener currentTween;
	// Use this for initialization
	void Start () {
        RegisterEvent();
        thisWidget = this.GetComponent<UIWidget>();
        this.GetComponent<UIWidget>().alpha = 0;
	}
    private void RegisterEvent()
    {
        EventManager.Instance().attach(InGameEventType.POPUP, this);
    }
	// Update is called once per frame
	void Update () {
	
	}

    public void Show( string content )
    {
        if (currentTween != null)
            currentTween.Pause();
        thisWidget.alpha = 1;
        Text.text = content;
        //currentTween = ((Tweener)(DG.Tweening.DOTween.To(() => thisWidget.alpha, x => thisWidget.alpha = x, (int)0, 1.5f))).SetEase(Ease.InExpo)
        //    .OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        currentTween = null;
    }
    public void handle(InGameEventType aEventType, object aEventData)
    {
        switch (aEventType)
        {
            case InGameEventType.POPUP:
                Show((string)aEventData);
                break;
        }
    }
}
