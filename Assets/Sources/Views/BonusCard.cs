using UnityEngine;
using System.Collections;
using System;
public enum BonusCardType
{
    ATHLETIC, CHANGE_SIDE, COACH_ADVICE, ENTERTAINMENT, FAKE_SHOT, GRAND_SLAM, HAVE_FUN, HOME_SUPPORT, JUMP_SHOT, KEEP_GOING
    , NEW_RACKET, PRECISION, TWEENER
}
public class BonusCard : MonoBehaviour, IEventListener
{

    private UISprite BonusCardSprite;
    private BonusCardType Type;

    private int rollNumber;
    private bool isRandomizing;
    private bool isDone;
    private float time;
    private bool isOpening;
    private int bonusCardLength;
    public void SetType(BonusCardType type)
    {
        Type = type;
        string spriteName = "bonus_athletic";
        switch( Type )
        {
            case BonusCardType.ATHLETIC:
                spriteName = "bonus_athletic";
                break;
            case BonusCardType.CHANGE_SIDE:
                spriteName = "bonus_changesides";
                break;
            case BonusCardType.COACH_ADVICE:
                spriteName = "bonus_coachadvice";
                break;
            case BonusCardType.ENTERTAINMENT:
                spriteName = "bonus_entertainment";
                break;
            case BonusCardType.FAKE_SHOT:
                spriteName = "bonus_fakeshot";
                break;
            case BonusCardType.GRAND_SLAM:
                spriteName = "bonus_grandslam";
                break;
            case BonusCardType.HAVE_FUN:
                spriteName = "bonus_havefun";
                break;
            case BonusCardType.HOME_SUPPORT:
                spriteName = "bonus_homesupport";
                break;
            case BonusCardType.JUMP_SHOT:
                spriteName = "bonus_jumpshot";
                break;
            case BonusCardType.KEEP_GOING:
                spriteName = "bonus_keepgoing";
                break;
            case BonusCardType.NEW_RACKET:
                spriteName = "bonus_newracket";
                break;
            case BonusCardType.PRECISION:
                spriteName = "bonus_precision";
                break;
            case BonusCardType.TWEENER:
                spriteName = "bonus_tweener";
                break;
        }
        BonusCardSprite.spriteName = spriteName;
    }

    public BonusCardType GetType()
    {
        return Type;
    }

    void Start()
    {
        bonusCardLength = Enum.GetNames(typeof(BonusCardType)).Length;
        BonusCardSprite = GetComponent<UISprite>();
        RegisterEvent();
        GetComponent<UIWidget>().alpha = 1;
        gameObject.SetActive(false);
    }

    private void RegisterEvent()
    {
        EventManager.Instance().attach(InGameEventType.OPEN_BONUS_CARD, this);
    }

    public void handle(InGameEventType aEventType, object aEventData)
    {
        switch (aEventType)
        {
            case InGameEventType.OPEN_BONUS_CARD:
                Open();
                break;
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        isOpening = false;
        time = 0;
    }
    public void Open()
    {
        if (!isOpening)
        {
            this.gameObject.SetActive(true);
            isRandomizing = true;
            isDone = false;
            isOpening = true;
            time = Time.time;
        }
    }

    void Update()
    {
        if (isRandomizing)
        {
            rollNumber = UnityEngine.Random.Range(0, bonusCardLength);
            //rollNumber = (int)BonusCardType.GRAND_SLAM;
            Debug.Log("isRandomizing: " + (BonusCardType)rollNumber);
            SetType((BonusCardType)rollNumber);
            //rollNumber = 0;
            if (Time.time - time >= 2f)
            {
                isRandomizing = false;
                isDone = true;
                time = Time.time;
                EventManager.Instance().fire(InGameEventType.POPUP, "Winner got " + (BonusCardType)rollNumber + "!!!");
            }
        }
        else if (isDone)
        {
            if (Time.time - time >= 2f)
            {
                isDone = false;
                //Fire event here
                EventManager.Instance().fire(InGameEventType.BONUS_CARD_GETTED, rollNumber);
                Close();
            }
        }
    }
    
}
