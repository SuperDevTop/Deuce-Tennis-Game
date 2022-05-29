using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    public static TypeWriter Instance;
    public float delay = 0.03f;
    public string currentText = "";
    public static string fullText = "";

    public Text textContent;
    public bool isCompleteSentence;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        writeDialogue("Hi, Nice to meet you. My name is Dmitry and I am from Russia.");
    }

    void Update()
    {        
    }

    public void writeDialogue(string text)
    {
        //List<string> levelDialogue = dialogueMap [currentLevel-1];
        //fullText = levelDialogue [0];
        fullText = textContent.text;
        currentText = "";
        StopCoroutine("ShowText");
        StartCoroutine("ShowText");
    }

    public void skipTyping()
    {
        print("skipping typing");
        StopCoroutine("ShowText");
        currentText = fullText;
        this.GetComponent<Text>().text = fullText;
        isCompleteSentence = true;  
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    public void CompleteSentence()
    {        
        StopCoroutine("ShowText");
        this.GetComponent<Text>().text = fullText;
    }

    public void FullTextClick()
    {
        if (this.GetComponent<Text>().text == fullText)
        {

        }
    }  
}
