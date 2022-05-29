using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionControl : MonoBehaviour
{
    public GameObject[] splashCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (Screen.width / Screen.height > 1.5f)
        {
            if (Application.loadedLevelName == "Splash")
            {
                splashCanvas[0].SetActive(true);
                splashCanvas[1].SetActive(false);
            }
        }
        else
        {
            if (Application.loadedLevelName == "Splash")
            {
                splashCanvas[1].SetActive(true);
                splashCanvas[0].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
