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
    private bool godTextEnabled = false;
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
        if (!godTextEnabled)
        {
            GodText();
        }
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
                godTextEdit.text = "You seem VERY tired... you should probably go to bed quickly!";
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;
            
            case godTextStates.CompostWarning:
                godTextEdit.text = "You cannot compost that item. Only plants and cuttings are allowed.";
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;
            
            case godTextStates.SellWarning:
                godTextEdit.text = "You cannot sell that item. Only plants, cuttings and pots are sellable.";
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;
            
            case godTextStates.DeliveryInfo:
                godTextEdit.text = "Your delivery has arrived!";
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;
            
            case godTextStates.CuttingsWarning:
                godTextEdit.text = "The plants has to be fully grown for the flowers to give a cutting.";
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;
            
            case godTextStates.DoneForToday:
                godTextEdit.text = "You seem done for today, go to sleep to proceed to the next day!";
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;
                
            default:
                break;
        }
    }

    
    public void BacktoDefault()
    {
        Debug.Log("Done!");
        godTextState = godTextStates.Default;
        godTextEnabled = false;
    }


    
    public IEnumerator SleepWarning()
    {
        LeanTween.moveY(godTextTransform, 35, 0.25f).setEaseLinear();
        yield return new WaitForSeconds(2);
        LeanTween.moveY(godTextTransform, 140, 0.25f).setEaseLinear().setOnComplete(BacktoDefault);
    }
}
