using UnityEngine;
using System.Collections;
using System;

public class DiceRollView : MonoBehaviour, IEventListener
{
    public UILabel RollNumberLabel;
    public UISprite RollButton;

    private int rollNumber;
    private bool isRolling;
    private bool isDone;
    private float time;
    private bool isOpening;

	// Use this for initialization
	void Start () 
    {
        RegisterEvent();
        UIEventListener.Get(RollButton.gameObject).onClick += Roll;
        GetComponent<UIWidget>().alpha = 1;
        gameObject.SetActive(false);
	}
    private void RegisterEvent()
    {
        EventManager.Instance().attach(InGameEventType.ROLL_DICE, this);
    }
	// Update is called once per frame
	void Update ()
    {
	    if(isRolling)
        {
            rollNumber = UnityEngine.Random.Range(0, 10);
            //rollNumber = 5;
            RollNumberLabel.text = rollNumber.ToString();
            if( Time.time - time >=1.5f)
            {
                isRolling = false;
                isDone = true;
                time = Time.time;
            }
        }
        else if (isDone)
        {
            if (Time.time - time >= 0.5f)
            {
                isDone = false;
                //Fire event here
                EventManager.Instance().fire(InGameEventType.ROLL_DONE, rollNumber);
                Close();
            }
        }
	}

    private void Roll(GameObject go)
    {
        Debug.Log("Roll");
        time = Time.time;
        isRolling = true;
        RollButton.GetComponent<BoxCollider>().enabled = false;
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        isOpening = false;
        time = 0;
    }
    public void Open()
    {
        if( !isOpening )
        {
            this.gameObject.SetActive(true);
            RollButton.GetComponent<BoxCollider>().enabled = true;
            isRolling = false;
            isDone = false;
            isOpening = true;

        }       
    }

    public void handle(InGameEventType aEventType, object aEventData)
    {
        switch (aEventType)
        {
            case InGameEventType.ROLL_DICE:
                Open();
                break;
        }
    }
}
