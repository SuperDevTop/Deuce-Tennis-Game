using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{    
    // Start is called before the first frame update
    void Start()
    {        
        StartCoroutine("Tap");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   IEnumerator Tap()
    {
        yield return new WaitForSeconds(3);

        Application.LoadLevel("MainMenu");
    }
}
