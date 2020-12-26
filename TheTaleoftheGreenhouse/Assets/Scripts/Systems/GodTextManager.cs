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
    private bool firstWarning = false;
    private bool guideInfo = false;

    private AudioSource audioSource;
    public AudioClip warningSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
        if (!godTextEnabled)
        {
            if (firstWarning == false)
            {
                StartCoroutine(FirstWarning());
                firstWarning = true;
            }
            GodText();
        }
    }

    public enum godTextStates
    {
        Default,
        FirstWarning,
        GuideInfo,
        SleepWarning,
        CompostWarning,
        SellWarning, 
        PotSellWarning,
        DeliveryInfo,
        CuttingsWarning,
        DoneForToday,
        PlantOfGift,
        PlantOfGiftRoast,
        WaterDeadPlant,
        DeliveryFull,
        ManaCatcherLeaking,
    }


    public void GodText()
    {
        switch (godTextState)
        {
            case godTextStates.Default:
                godTextEdit.text = "";
                break;
            
            case godTextStates.FirstWarning:
                godTextEnabled = true;
                StartCoroutine(FirstWarning());
                break;
            
            case godTextStates.GuideInfo:
                godTextEdit.text =
                    "Great, now buy and grow some plants! If you get stuck, all help you need can be found in the book";
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;
            
            case godTextStates.SleepWarning:
                godTextEdit.text = "You seem VERY tired... you should probably go to bed quickly!";
                audioSource.PlayOneShot(warningSound);
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;
            
            case godTextStates.CompostWarning:
                godTextEdit.text = "You cannot compost that item. Only plants and cuttings are allowed.";
                audioSource.PlayOneShot(warningSound);
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;
            
            case godTextStates.SellWarning:
                godTextEdit.text = "You cannot sell that item. Only plants, cuttings and pots are sellable.";
                audioSource.PlayOneShot(warningSound);
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;

            case godTextStates.PotSellWarning:
                godTextEdit.text = "The pot needs to be empty to sell.";
                audioSource.PlayOneShot(warningSound);
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
                audioSource.PlayOneShot(warningSound);
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;
            
            case godTextStates.DoneForToday:
                godTextEdit.text = "You seem done for today, go to sleep to proceed to the next day!";
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;

            case godTextStates.PlantOfGift:
                godTextEdit.text = "A plant has appeared outside. It is like this is your destiny.";
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;

            case godTextStates.PlantOfGiftRoast:
                String[] roastTexts = new string[] {"Maybe this is not your destiny.",
                                                    "Looks like you are giving 100%",
                                                    "You smell bad... I think?",
                                                    "Are you even trying?",
                                                    "Are you playing on a console?",
                                                    "Your hands are bleeding"};
                String randomString;
                int randomValue = UnityEngine.Random.Range(0,roastTexts.Length);
                randomString = roastTexts[randomValue];
                godTextEdit.text = randomString;
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;

            case godTextStates.WaterDeadPlant:
                godTextEdit.text = "This plant is dead and nothing can save it.";
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;
            
            case godTextStates.DeliveryFull:
                godTextEdit.text = "Clean up delivery if you want to buy more items.";
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;

            case godTextStates.ManaCatcherLeaking:
                godTextEdit.text = "The mana catcher is leaking mana.";
                StartCoroutine(SleepWarning());
                godTextEnabled = true;
                break;

            default:
                break;
        }
    }

    public void ChangeGodTextState(godTextStates newGodTextState)
    {
        godTextState = newGodTextState;
    }
    
    public void BacktoDefault()
    {
        godTextState = godTextStates.Default;
        godTextEnabled = false;
    }

    public IEnumerator FirstWarning()
    {
        godTextState = godTextStates.FirstWarning;
        LeanTween.moveY(godTextTransform, 35, 0.4f).setEaseLinear();
        godTextEdit.text = "You should clean out the dead plants from the greenhouse.";
        yield return new WaitForSeconds(6);
        LeanTween.moveY(godTextTransform, 140, 0.4f).setEaseLinear();
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveY(godTextTransform, 35, 0.4f).setEaseLinear();
        godTextEdit.text = "All help you need can be found in the book! Happy Planting!";
        yield return new WaitForSeconds(6);
        LeanTween.moveY(godTextTransform, 140, 0.4f).setEaseLinear();
        yield return null;
    }

    
    public IEnumerator SleepWarning()
    {
        LeanTween.moveY(godTextTransform, 35, 0.4f).setEaseLinear();
        yield return new WaitForSeconds(6);
        LeanTween.moveY(godTextTransform, 140, 0.4f).setEaseLinear().setOnComplete(BacktoDefault);
    }

    public IEnumerator GeneralWarning()
    {
        LeanTween.moveY(godTextTransform, 35, 0.25f).setEaseLinear();
        yield return new WaitForSeconds(2);
        LeanTween.moveY(godTextTransform, 140, 0.25f).setEaseLinear().setOnComplete(BacktoDefault);
    }
}
