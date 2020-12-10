using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class GodTextManager : MonoBehaviour
{
    public static GodTextManager instance;
    public RectTransform godTextTransform;
    public TMP_Text godTextEdit;
    public godTextStates godTextState;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        GodText();
    }

    public enum godTextStates
    {
        Default,
        SleepWarning,
        CompostWarning,
        SellWarning, 
        DeliveryInfo,
        CuttingsWarning,
        DoneForToday
    }


    public void GodText()
    {
        switch (godTextState)
        {
            case godTextStates.Default:
                godTextEdit.text = "";
                break;
            
            case godTextStates.SleepWarning:
                StartCoroutine(SleepWarning());
                break;
            
            case godTextStates.CompostWarning:
                godTextEdit.text = "You cannot compost that item. Only plants and cuttings are allowed.";
                break;
            
            case godTextStates.SellWarning:
                godTextEdit.text = "You cannot sell that item. Only plants, cuttings and pots are sellable.";
                break;
            
            case godTextStates.DeliveryInfo:
                godTextEdit.text = "Your delivery has arrived!";
                break;
            
            case godTextStates.CuttingsWarning:
                godTextEdit.text = "The plants has to be fully grown for the flowers to give a cutting.";
                break;
            
            case godTextStates.DoneForToday:
                godTextEdit.text = "You seem done for today, go to sleep to proceed to the next day!";
                break;
                
            default:
                break;
        }
        
    }

    public void BacktoDefault()
    {
        Debug.Log("Done!");
        godTextState = godTextStates.Default;
    }

    
    public void Text()
    {
        Debug.Log("Done!");
        godTextState = godTextStates.Default;
    }

    
    public IEnumerator SleepWarning()
    {
        godTextEdit.text = "You are starting to get VERY tired, you should head to bed...";
        LeanTween.moveY(godTextTransform, 940, 0.5f).setEaseLinear();
        yield return new WaitForSeconds(2);
        LeanTween.moveY(godTextTransform, 1080, 0.5f).setEaseLinear();
        yield return new WaitForSeconds(2);
        BacktoDefault();
        
        yield return null;
    }
}
