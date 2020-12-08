﻿using System;
using System.Collections;
using UnityEngine;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

[DefaultExecutionOrder(-9)]
public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private AudioSource deliverySound;
    [SerializeField] GameObject nightCanvas;
    [SerializeField] GameObject sleepPromptCanvas;
    [SerializeField] GameObject alreadyBoughtCanvas;
    private GameObject buyMenuCanvas;
    private GameObject deliveryManager;
    private GameObject sellBox;
    public RectTransform nightPanel;
    public RectTransform sleepPrompt;
    public static DayNightCycle instance;
    public float realSecondsPerIngameDay;
    public float nightFadeDuration;
    public Light2D light;
    
    public static float day;
    public float hoursPerDay = 24f;
    public float minutesPerHour = 60f;

    public string hourString;
    public string minutesString;

    [SerializeField] Color dayColor;
    [SerializeField] Color eveningColor;
    [SerializeField] float dayIntensity = 1f;
    [SerializeField] float eveningIntensity = 0.75f;
    [SerializeField] float transitionTime = 2;
    private float currentIntensity;

    private bool firstMorning = true;
    private bool isSleeping = false;
    private bool isAlreadySleeping = false;
    private bool wantToSleep = false;
    
    private float timer = 0;

    [SerializeField] Transform hourHandTransform;

    [SerializeField] GameObject player;


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

    void Start()
    {
        buyMenuCanvas = GameObject.Find("Shop");
        deliveryManager = GameObject.Find("Delivery");
        sellBox = GameObject.Find("SellBox");


        if (light == null)
        {
            light = FindObjectOfType<Light2D>();
        }
    }
    
    
    void Update()
    {
        CalculateTime();
        lightChange();
    }

    
    void CalculateTime()
    {
        day += Time.deltaTime / realSecondsPerIngameDay;
        float dayNormalized = day % 1f;
        
        hourString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");
        minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

        float rotationDegreesPerDay = 360f;
        hourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay);
    }


    void lightChange()
    {
        
        if (float.Parse(hourString) > 18f && float.Parse(hourString) < 21f)
        {
            timer += Time.deltaTime;
            
            light.intensity = Mathf.Lerp(dayIntensity, eveningIntensity, transitionTime * timer);
            light.color = Color.Lerp(dayColor, eveningColor, transitionTime * timer);
            firstMorning = false;
        }

        else if (float.Parse(hourString) > 5f && float.Parse(hourString) < 8f && !firstMorning)
        {
            timer += Time.deltaTime;
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerRenderer>().enabled = true;
            
            light.intensity = Mathf.Lerp(eveningIntensity, dayIntensity, transitionTime * timer);
            light.color = Color.Lerp(eveningColor, dayColor, transitionTime * timer);

        }
        else
        {
            timer = 0f;
        }
        
        // if (float.Parse(hourString) > 2f && float.Parse(hourString) < 3f)
        // {
        //     //Sleep();
        //     Debug.Log("Hej");
        // }

        if (float.Parse(hourString) > 5f && float.Parse(hourString) < 9f)
        {
            if(isSleeping)
            {
                realSecondsPerIngameDay *= 8f;
                isAlreadySleeping = false;
                isSleeping = false;
                player.GetComponent<PlayerMovement>().enabled = true;
                player.GetComponent<PlayerRenderer>().enabled = true;

                StartCoroutine(WakeUpRoutine());
                
            }
        }
    }
    
    
    public event Action onSleep;


    public void Sleep()
    {
        if (!isAlreadySleeping)
        {
            realSecondsPerIngameDay /= 8f;
            isAlreadySleeping = true;
            isSleeping = true;
            StartCoroutine(GoToSleepRoutine());
        }
        
    }
    
    
    public IEnumerator GoToSleepRoutine()
    {
        
        nightCanvas.SetActive(true);
        LeanTween.alpha(nightPanel, 1f, nightFadeDuration).setEase(LeanTweenType.linear);

        yield return new WaitForSeconds(1);
        
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerRenderer>().enabled = false;
        buyMenuCanvas.GetComponent<ShopBehaviourBuy>().currentlyBuyingTables = 0;
        buyMenuCanvas.GetComponent<ShopBehaviourBuy>().currentlyBuyingPots = 0;
        buyMenuCanvas.GetComponent<ShopBehaviourBuy>().currentlyBuyingPlants = 0;
        buyMenuCanvas.GetComponent<ShopBehaviourBuy>().plantCurrentBuy1.value = 0;
        buyMenuCanvas.GetComponent<ShopBehaviourBuy>().currentlyBuyingManaPlants = 0;
        buyMenuCanvas.GetComponent<ShopBehaviourBuy>().manaPlantCurrentBuy1.value = 0;
        buyMenuCanvas.GetComponent<ShopBehaviourBuy>().currentlyBuyingManaStorageItems = 0;
        buyMenuCanvas.GetComponent<ShopBehaviourBuy>().hasOrderedItems = false;

        LeanTween.scale(alreadyBoughtCanvas, new Vector3(1f, 1f, 1f), 0.01f);
        alreadyBoughtCanvas.SetActive(false);
        
        GameManager.instance.ChangeGameState(GameManager.GameState.GameNight);



        if (sellBox.GetComponent<SellBoxBehaviour>().maxNumbertoSell > 0)
        {
            buyMenuCanvas.GetComponent<ShopBehaviourBuy>().
                playerMoney+= sellBox.GetComponent<SellItems>().GetGold();
            for (int i = 0; i < sellBox.GetComponent<SellBoxBehaviour>().maxNumbertoSell; i++)
            {
                if (sellBox.GetComponent<SellBoxBehaviour>().itemsToSell[i] != null)
                {
                    Destroy(sellBox.GetComponent<SellBoxBehaviour>().itemsToSell[i]);
                }
                else
                {
                    break;
                }
            } 
        }
        
        yield return null;
        onSleep?.Invoke();
    }

    public IEnumerator WakeUpRoutine()
    {

        for (int i = 0; i < 3; i++)
        {
            deliveryManager.GetComponent<DeliveryManager>().Delivery();
        }
        
        if (buyMenuCanvas.GetComponent<ShopBehaviourBuy>().hasBoughtSomething)
        {
            deliverySound.Play();
            buyMenuCanvas.GetComponent<ShopBehaviourBuy>().hasBoughtSomething = false;
        }
        
        yield return new WaitForSeconds(1);

        isAlreadySleeping = false;
        GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
        
        LeanTween.alpha(nightPanel, 0f, nightFadeDuration).setEase(LeanTweenType.linear);
        yield return new WaitForSeconds(nightFadeDuration);
        nightCanvas.SetActive(false);
        
        yield return null;
    }

    public void SleepPrompt()
    {
        if (!firstMorning)
        {
            sleepPromptCanvas.SetActive(true);
            LeanTween.scale(sleepPrompt, new Vector3(1, 1, 1), 0.15f).setEaseLinear();
        }
    }

    public void AcceptSleep()
    {
        wantToSleep = true;
        LeanTween.scale(sleepPrompt, new Vector3(0, 0, 0), 0.5f).setEaseLinear().setOnComplete(DeactivateSleep).setOnComplete(Sleep);
    }


    public void DeclineSleep()
    {
        wantToSleep = false;
        LeanTween.scale(sleepPrompt, new Vector3(0, 0, 0), 0.5f).setEaseLinear().setOnComplete(DeactivateSleep);
    }

    public void DeactivateSleep()
    {
        sleepPromptCanvas.SetActive(false);
    }
}
