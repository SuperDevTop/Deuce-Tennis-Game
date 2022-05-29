using UnityEngine;
using System.Collections;

public class HomeSupportView : MonoBehaviour, IEventListener
{
    public UISprite noOne;
    public UISprite onePerson;
    public UISprite twoPeople;
    public UISprite threePeople;
    private bool isOpening;
    void Start()
    {
        RegisterEvent();
        UIEventListener.Get(noOne.gameObject).onClick += supportClicked;
        UIEventListener.Get(onePerson.gameObject).onClick += supportClicked;
        UIEventListener.Get(twoPeople.gameObject).onClick += supportClicked;
        UIEventListener.Get(threePeople.gameObject).onClick += supportClicked;
        GetComponent<UIWidget>().alpha = 1;
        gameObject.SetActive(false);
    }

    private void RegisterEvent()
    {
        EventManager.Instance().attach(InGameEventType.OPEN_HOME_SUPPORT, this);
    }

    public void handle(InGameEventType aEventType, object aEventData)
    {
        switch (aEventType)
        {
            case InGameEventType.OPEN_HOME_SUPPORT:
                Open();
                break;
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        isOpening = false;
    }
    public void Open()
    {
        if (!isOpening)
        {
            this.gameObject.SetActive(true);
            isOpening = true;
        }
    }

    private void supportClicked(GameObject gameObject)
    {
        switch( gameObject.name )
        {
            case "1Person":
                EventManager.Instance().fire(InGameEventType.CHOOSE_HOME_SUPPORT, 1);
                break;
            case "2People":
                EventManager.Instance().fire(InGameEventType.CHOOSE_HOME_SUPPORT, 2);
                break;
            case "3People":
                EventManager.Instance().fire(InGameEventType.CHOOSE_HOME_SUPPORT, 3);
                break;

            case "NoOne":
            default:
                EventManager.Instance().fire(InGameEventType.CHOOSE_HOME_SUPPORT, 0);
                break;
        }
        Close();
    }
}
