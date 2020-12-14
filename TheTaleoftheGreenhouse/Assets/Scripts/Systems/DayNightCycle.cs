using System;
using System.Collections;
using UnityEngine;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

[DefaultExecutionOrder(-9)]
public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private AudioSource deliverySound;
    [SerializeField] GameObject nightCanvas;
    [SerializeField] GameObject sleepPromptCanvas;
    [SerializeField] GameObject forcedSleepCanvas;
    [SerializeField] GameObject alreadyBoughtCanvas;
    [SerializeField] GameObject shopBuyMenu;
    private GameObject buyMenuCanvas;
    private GameObject deliveryManager;
    private GameObject sellBox;
    public GameObject sleepPrompt;
    public GameObject forcedSleeppanel;
    public RectTransform nightPanel;
    public static DayNightCycle instance;
    public float realSecondsPerIngameDay;
    public float nightFadeDuration;
    public Light2D light;
    public int TimeAccelerator = 24;
    
    public static float day;
    public float hoursPerDay = 24f;
    public float minutesPerHour = 60f;

    public string hourString;

    [SerializeField] Color dayColor;
    [SerializeField] Color eveningColor;
    [SerializeField] float dayIntensity = 1f;
    [SerializeField] float eveningIntensity = 0.75f;
    [SerializeField] float transitionTime = 2;
    private float currentIntensity;

    private bool firstMorning = true;
    private bool allowTime = true;
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
        if (allowTime)
        {
            if (!shopBuyMenu.activeInHierarchy)
            {
                CalculateTime();
            }
        }
        lightChange();
    }

    
    void CalculateTime()
    {
        day += Time.deltaTime / realSecondsPerIngameDay;
        float dayNormalized = day % 1f;
        
        hourString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");

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
                player.GetComponent<PlayerRenderer>().enabled = false;
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
        
        if (forcedSleep)
        {
            forcedSleepCanvas.SetActive(true);
            LeanTween.scale(forcedSleeppanel, new Vector3(1f, 1f, 1f), 0.5f);
            allowTime = false;
        }


        if (sellBox.GetComponent<SellBoxBehaviour>().maxNumbertoSell > 0)
        {
            buyMenuCanvas.GetComponent<ShopBehaviourBuy>().
            playerMoney+= sellBox.GetComponent<SellItems>().GetGold();
            sellBox.GetComponent<SellBoxBehaviour>().ResetSlots();
            for (int i = 0; i < sellBox.GetComponent<SellBoxBehaviour>().maxNumbertoSell; i++)
            {
                if (sellBox.GetComponent<SellBoxBehaviour>().itemsToSell[i] != null)
                {
                    Destroy(sellBox.GetComponent<SellBoxBehaviour>().itemsToSell[i]);
                    sellBox.GetComponent<SellBoxBehaviour>().itemsToSell[i] = null;
                    sellBox.GetComponent<SellItems>().SetGoldBack();
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

        if (!forcedSleep)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerRenderer>().enabled = true;
        }
        
        forcedSleep = false;
        
        yield return null;
    }

    public void SleepPrompt()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerRenderer>().enabled = false;
        sleepPromptCanvas.SetActive(true);
        sleepPrompt.SetActive(true);
        LeanTween.scale(sleepPrompt, new Vector3(1, 1, 1), 0.5f).setEaseOutBack();
        allowTime = false;
    }

    public void AcceptForcedSleep()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerRenderer>().enabled = true;
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
        player.GetComponent<PlayerRenderer>().enabled = true;
        allowTime = true;
    }

    public void DeactivateSleep()
    {    
        forcedSleepCanvas.SetActive(false);
        sleepPrompt.SetActive(false);
        sleepPromptCanvas.SetActive(false);
        
    }
}
