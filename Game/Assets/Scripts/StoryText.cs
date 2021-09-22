using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryText : MonoBehaviour
{
    public Text[] text;
    public string originalText;
    public string currentText;
    public int index = 1;
    public int currentTextBox = 0;
    void Start()
    {
        originalText = text[currentTextBox].text;
        StartCoroutine(StoryType());
    }

    void NextTextBox()
    {
        index = 1;
        text[currentTextBox - 1].gameObject.SetActive(false);
        text[currentTextBox].gameObject.SetActive(true);
        originalText = text[currentTextBox].text;
        currentText = "";
        StartCoroutine(StoryType());
    }

    IEnumerator StoryType()
    {
        while(currentText.Length < originalText.Length)
        {
            float waitFor = 0.075f;
            //float waitFor = 0.001f;
            text[currentTextBox].text = currentText;
            currentText = originalText.Substring(0, index++);
            if (index>2 && currentText.ToCharArray()[index-3] == '.')
            {
                waitFor *= 6;
            }
            yield return new WaitForSeconds(waitFor);
        }
        if (text.Length > 1 && ++currentTextBox < text.Length)
            NextTextBox();
        else FindObjectOfType<StartGame>().StartGameMethod();
    }
}
