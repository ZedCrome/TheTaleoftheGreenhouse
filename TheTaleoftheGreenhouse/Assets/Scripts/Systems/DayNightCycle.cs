﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

[DefaultExecutionOrder(-9)]
public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private AudioSource deliverySound;
    [SerializeField] GameObject nightCanvas;
    [SerializeField] GameObject alreadyBoughtCanvas;
    private GameObject buyMenuCanvas;
    private GameObject deliveryManager;
    private GameObject sellBox;
    public RectTransform nightPanel;
    public static DayNightCycle instance;
    public float realSecondsPerIngameDay;
    public float nightFadeDuration;
    public Light2D light;
    
    public static float day;
    public float hoursPerDay = 24f;
    public float minutesPerHour = 60f;

    private string hourString;
    private string minutesString;

    [SerializeField] Color dayColor;
    [SerializeField] Color eveningColor;
    [SerializeField] float dayIntensity = 1f;
    [SerializeField] float eveningIntensity = 0.75f;
    [SerializeField] float transitionTime = 2;
    private float currentIntensity;

    private bool firstMorning = true;
    private bool allowedToSleep = false;
    private bool isSleeping = false;
    private bool isAlreadySleeping = false;
    
    private float timer = 0;

    [SerializeField] Transform hourHandTransform;
    [SerializeField] TMP_Text sleepText;

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
        Debugger.instance.Log("Allowed To Sleep: ", allowedToSleep);
        
        day += Time.deltaTime / realSecondsPerIngameDay;
        float dayNormalized = day % 1f;
        
        hourString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");
        minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

        float rotationDegreesPerDay = 360f;
        hourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay);

        lightChange();
    }


    void lightChange()
    {
        if (float.Parse(hourString) > 18f && float.Parse(hourString) < 24f)
        {
            timer += Time.deltaTime;
            allowedToSleep = true;
            
            if(!isSleeping)
                light.intensity = Mathf.Lerp(dayIntensity, eveningIntensity, transitionTime * timer);
            
            
            light.color = Color.Lerp(dayColor, eveningColor, transitionTime * timer);
            firstMorning = false;
        }
        else if (float.Parse(hourString) > 0f && float.Parse(hourString) < 5 && !firstMorning)
        {
            allowedToSleep = true;
            
        }

        else if (float.Parse(hourString) > 6f && float.Parse(hourString) < 11f && !firstMorning)
        {
            timer += Time.deltaTime;
            allowedToSleep = false;
            sleepText.text = "";
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerRenderer>().enabled = true;
            
            if (!isSleeping)
            {
                light.intensity = Mathf.Lerp(eveningIntensity, dayIntensity, transitionTime * timer);
            }
            
            light.color = Color.Lerp(eveningColor, dayColor, transitionTime * timer);
            
            if(isSleeping)
            {
                
                GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
                realSecondsPerIngameDay *= 8f;
                isAlreadySleeping = false;
                isSleeping = false;
                nightFadeOut();
                StartCoroutine(DelayCoroutine());
                nightCanvas.SetActive(false);
            }
        }
        else
        {
            timer = 0f;
        }
    }
    
    void nightFadeOut()
    {
        LeanTween.alpha(nightPanel, 0f, nightFadeDuration).setEase(LeanTweenType.linear);
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(1f);
        
    }
    
    public event Action onSleep;
    
    public void Sleep()
    {
        isSleeping = true;
        if (allowedToSleep)
        {
            nightCanvas.SetActive(true);
            LeanTween.alpha(nightPanel, 1f, nightFadeDuration).setEase(LeanTweenType.linear);
            sleepText.text = "Sleeping";
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

            realSecondsPerIngameDay /= 8f;
            if (buyMenuCanvas.GetComponent<ShopBehaviourBuy>().hasBoughtSomething == true)
            {
                deliverySound.Play();
                buyMenuCanvas.GetComponent<ShopBehaviourBuy>().hasBoughtSomething = false;
            }
            
            for (int i = 0; i < 3; i++)
            {
                deliveryManager.GetComponent<DeliveryManager>().Delivery();
            }

            buyMenuCanvas.GetComponent<ShopBehaviourBuy>().
            playerMoney+= sellBox.GetComponent<SellItems>().GetGold();

            onSleep?.Invoke();
        }
    }
}
