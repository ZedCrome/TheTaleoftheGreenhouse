﻿using System.Collections;
using UnityEngine;
using TMPro;

public class TextRevealer : MonoBehaviour
{
    public TMP_Text text11;
    public TMP_Text text12;
    public TMP_Text text13;
    public string[] texts;
    private char[] letters;
    public float textTime = 0.05f;
    private float timeUntillChange = 2f;
    private float timer;

    private int LineCounter;
    
    void Start()
    {
        texts = new string[]
        {
            "",
            "Sister, I'm back home because you need me...",
            "Your sickness is spreading each day...",
            "Legend has it that a magical knife can cure all diseases...",
            "And for some reason, the knife has appeared in our parent's old greenhouse.",
            "I know what I have to do.",
            "The knife requires mana...",
            "It's known that some special flowers absorbs mana from the air.",
            "Just hold on, I will save you!",
            "",
            "",
            ""
        };
        StartCoroutine(StartDelay());
    }

    public IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1);
        NextLine();
    }
    
    void NextLine()
    {
        letters = texts[LineCounter].ToCharArray();
        LineCounter++;
        StartCoroutine(ShowText(texts[LineCounter]));
    }
    
    public IEnumerator ShowText(string txt)
    {
        
        text11.alpha = 0.2f;
        text12.alpha = 0.45f;
        text13.alpha = 1f;
        
        yield return new WaitForSeconds(0.33f);

        for (int i = 0; i < letters.Length + 2; i++)
        {
            timer += textTime;
            if (i < letters.Length)
            { 
                text11.text += letters[i];
            }
            if (i > 0 && i < letters.Length + 1)
            { 
                text12.text += letters[i - 1];
            }
            if (i > 1 && i < letters.Length + 2)
            { 
                text13.text += letters[i - 2];
            }
            yield return new WaitForSeconds(textTime);
        }
        yield return new WaitForSeconds( timeUntillChange - timer);
        timer = 0f;
        StartCoroutine(FadeText());
    }

    
    public IEnumerator FadeText()
    {
        yield return new WaitForSeconds(2.33f);

        while (text13.alpha > 0)
        {
            text11.alpha--;
            text12.alpha--;
            text13.alpha -= 0.01f;
            yield return new WaitForSeconds(0.005f);
        }

        text11.text = "";
        text12.text = "";
        text13.text = "";
        NextLine();
    }
}