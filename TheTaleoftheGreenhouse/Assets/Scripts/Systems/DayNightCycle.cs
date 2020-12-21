using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

[DefaultExecutionOrder(-9)]
public class DayNightCycle : MonoBehaviour
{
    public Earnings earnings;
    [SerializeField] private AudioSource deliverySound;
    [SerializeField] GameObject nightCanvas;
    [SerializeField] GameObject sleepPromptCanvas;
    [SerializeField] GameObject forcedSleepCanvas;
    [SerializeField] GameObject alreadyBoughtCanvas;
    [SerializeField] GameObject shopBuyMenu;
    [SerializeField] GameObject shopNoteMenu;
    private GameObject buyMenuCanvas;
    private GameObject sellBox;
    public GameObject sleepPrompt;
    public GameObject forcedSleeppanel;
    public RectTransform nightPanel;
    public static DayNightCycle instance;
    public float realSecondsPerIngameDay;
    private float initRealSecondsPerIngameDay;
    public float nightFadeDuration;
    public Light2D light;
    public int TimeAccelerator = 24;
    
    public static float day;
    public float hoursPerDay = 24f;
    public float minutesPerHour = 60f;

    public string hourString;
    public string minutesString;
    [SerializeField] private TMP_Text dayCount;
    private int numberOfDays = 0;


    [SerializeField] Color dayColor;
    [SerializeField] Color eveningColor;
    [SerializeField] float dayIntensity = 1f;
    [SerializeField] float eveningIntensity = 0.75f;
    [SerializeField] float transitionTime = 2;
    private float currentIntensity;

    private bool firstMorning = true;
    public bool allowTime = true;
    private bool isSleeping = false;
    private bool isAlreadySleeping = false;
    private bool wantToSleep = false;
    private bool forcedSleep = false;
    
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
        earnings = FindObjectOfType<Earnings>();
        buyMenuCanvas = GameObject.Find("Shop");
        sellBox = GameObject.Find("SellBox");

        initRealSecondsPerIngameDay = realSecondsPerIngameDay;
        
        if (light == null)
        {
            light = FindObjectOfType<Light2D>();
        }
    }
    
    
    void Update()
    {
        if (allowTime)
        {
            if (GameManager.instance.currentGameState != GameManager.GameState.ShopMenu)
            {
                if (GameManager.instance.currentGameState != GameManager.GameState.PauseGame)
                {
                    if (GameManager.instance.currentGameState != GameManager.GameState.Options)
                    {
                        CalculateTime(); 
                    }
                }
            }
        }
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
        if (float.Parse(hourString) >= 0f && float.Parse(hourString) < 12f)
        {
            if (float.Parse(hourString) == 1 && !firstMorning)
            {
                timer += Time.deltaTime;
                
                light.intensity = Mathf.Lerp(eveningIntensity, dayIntensity, transitionTime * timer);
                light.color = Color.Lerp(eveningColor, dayColor, transitionTime * timer);
            }
            else
            {
                timer = 0;
            }
            
            
            
            if (float.Parse(hourString) == 1)
            {
                if(isSleeping)
                {
                    realSecondsPerIngameDay *= TimeAccelerator;
                    isAlreadySleeping = false;
                    isSleeping = false;

                    StartCoroutine(WakeUpRoutine());
                }
            }
        }
        
        if (float.Parse(hourString) >= 12f && float.Parse(hourString) <= 24f)
        {
            if (float.Parse(hourString) == 12f)
            {
                timer += Time.deltaTime;
                
                light.intensity = Mathf.Lerp(dayIntensity, eveningIntensity, transitionTime * timer);
                light.color = Color.Lerp(dayColor, eveningColor, transitionTime * timer);
                firstMorning = false;
            }
            else
            {
                timer = 0;
            }
            
            if (float.Parse(hourString) == 17f && !isSleeping && !firstMorning)
            {
                GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.SleepWarning);
            }
            
            if (float.Parse(hourString) == 19 &&!isSleeping && !firstMorning)
            {
                Sleep();
                player.GetComponent<PlayerMovement>().enabled = false;
                //player.GetComponent<PlayerRenderer>().enabled = false;
                forcedSleep = true;
            }
        }
    }
    
    
    public event Action onSleep;
    public void Sleep()
    {
        if (!isAlreadySleeping)
        {
            realSecondsPerIngameDay /= TimeAccelerator;
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
        

        ShopBehaviourBuy shopBehaviourBuy = buyMenuCanvas.GetComponent<ShopBehaviourBuy>();
        shopBehaviourBuy.currentlyBuyingTables = 0;
        shopBehaviourBuy.currentlyBuyingPots = 0;
        shopBehaviourBuy.currentlyBuyingPlants = 0;
        shopBehaviourBuy.plantCurrentBuy1.value = 0;
        shopBehaviourBuy.currentlyBuyingManaPlants = 0;
        shopBehaviourBuy.manaPlantCurrentBuy1.value = 0;
        shopBehaviourBuy.currentlyBuyingManaStorageItems = 0;
        shopBehaviourBuy.hasOrderedItems = false;

        LeanTween.scale(alreadyBoughtCanvas, new Vector3(1f, 1f, 1f), 0.01f);
        alreadyBoughtCanvas.SetActive(false);
        
        GameManager.instance.ChangeGameState(GameManager.GameState.GameNight);
        
        if (forcedSleep)
        {
            forcedSleepCanvas.SetActive(true);
            LeanTween.scale(forcedSleeppanel, new Vector3(1f, 1f, 1f), 0.5f);
            allowTime = false;
        }

        
        if (SellBoxBehaviour.instance.maxNumbertoSell > 0)
        {
            shopBehaviourBuy.playerMoney+= sellBox.GetComponent<SellItems>().GetGold();
            
            SellBoxBehaviour.instance.ResetSlots();
            for (int i = 0; i < SellBoxBehaviour.instance.maxNumbertoSell; i++)
            {
                bool doOneTime = false;
                if (SellBoxBehaviour.instance.itemsToSell[i] != null)
                {
                    Destroy(SellBoxBehaviour.instance.itemsToSell[i]);
                    SellBoxBehaviour.instance.itemsToSell[i] = null;
                    sellBox.GetComponent<SellItems>().SetGoldBack();
                    if (doOneTime == false)
                    {
                        earnings.ActivateNotification();
                        earnings.EarningsNotification();
                        doOneTime = true;
                    }
                }
                else
                {
                    doOneTime = false;
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
            DeliveryManager.instance.Delivery();
        }
        
        if (buyMenuCanvas.GetComponent<ShopBehaviourBuy>().hasBoughtSomething)
        {
            deliverySound.Play();
            buyMenuCanvas.GetComponent<ShopBehaviourBuy>().hasBoughtSomething = false;
            GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.DeliveryInfo);
        }

        numberOfDays++;
        dayCount.text = numberOfDays.ToString();

        yield return new WaitForSeconds(1);

        isAlreadySleeping = false;
        GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
        
        LeanTween.alpha(nightPanel, 0f, nightFadeDuration).setEase(LeanTweenType.linear);
        yield return new WaitForSeconds(nightFadeDuration);
        nightCanvas.SetActive(false);

        if (!forcedSleep)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            
        }
        
        forcedSleep = false;

        // Increase time of day depending on how many tables owned
        if (TableManager.instance.unlockedTables - 3 > 0)
        {
            float multiplier = TableManager.instance.unlockedTables - 3;

            realSecondsPerIngameDay = initRealSecondsPerIngameDay + (multiplier * 10);
        }
        
        yield return null;
    }

    public void SleepPrompt()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        sleepPromptCanvas.SetActive(true);
        sleepPrompt.SetActive(true);
        LeanTween.scale(sleepPrompt, new Vector3(1, 1, 1), 0.5f).setEaseOutBack();
        allowTime = false;
    }

    public void AcceptForcedSleep()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        LeanTween.scale(forcedSleeppanel, new Vector3(0f, 0f, 0f), 0.5f).setEaseInBack().setOnComplete(DeactivateSleep);
        allowTime = true;
    }

    public void AcceptSleep()
    {
        wantToSleep = true;
        LeanTween.scale(sleepPrompt, new Vector3(0, 0, 0), 0.5f).setEaseInBack().setOnComplete(DeactivateSleep).setOnComplete(Sleep);
        allowTime = true;
    }


    public void DeclineSleep()
    {
        wantToSleep = false;
        LeanTween.scale(sleepPrompt, new Vector3(0, 0, 0), 0.5f).setEaseInBack().setOnComplete(DeactivateSleep);
        player.GetComponent<PlayerMovement>().enabled = true;
        allowTime = true;
    }

    public void DeactivateSleep()
    {    
        forcedSleepCanvas.SetActive(false);
        sleepPrompt.SetActive(false);
        sleepPromptCanvas.SetActive(false);
        
    }
}
