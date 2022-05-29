using UnityEngine;
using System.Collections;

public class StrokeCardCancelButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIEventListener.Get(gameObject).onClick += cancel;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void cancel(GameObject gameObject)
    {
        EventManager.Instance().fire(InGameEventType.DONT_CHOOSE_STROKE_CARD, null);
    }
}
