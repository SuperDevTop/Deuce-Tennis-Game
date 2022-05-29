using UnityEngine;
using System.Collections;

public class CardsView : MonoBehaviour, IEventListener
{

    public FieldView fieldView;
    public UIWidget bottomContainer;
    public UISprite BG;
    public UIWidget StrokeCards;


	void Start ()
    {
        RegisterEvent();
        StartCoroutine(InitCardsView());
	}
    private void RegisterEvent()
    {
        EventManager.Instance().attach(InGameEventType.TOGGLE_OPEN_STROKE_CARD, this);
        EventManager.Instance().attach(InGameEventType.OPEN_BONUS_CARD, this);
    }
    IEnumerator InitCardsView()
    {
        yield return 0;
        var screenHeight = BG.height;
        bottomContainer.height = (int)(screenHeight - fieldView.GetGroundHeight()) / 2 - 10;
        bottomContainer.ResetAndUpdateAnchors();
    }

    public void handle(InGameEventType aEventType, object aEventData)
    {
        switch(aEventType)
        {
            case InGameEventType.TOGGLE_OPEN_STROKE_CARD:
                bool isStrokeCardsOpen = (bool)aEventData;
                StrokeCards.gameObject.SetActive(isStrokeCardsOpen);
                break;
        }
    }

    
}
