using System;
using TMPro;
using UnityEngine;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

[DefaultExecutionOrder(-9)]
public class DayNightCycle : MonoBehaviour
{
    
    public static DayNightCycle instance;
    public Light2D light;
    public float realSecondsPerIngameDay;

    [Header("Day-Settings")]
    [Space(20)]
    public static float day;
    public float hoursPerDay = 24f;
    public float minutesPerHour = 60f;

    public string hourString;
    public string minutesString;

    [SerializeField] Color dayColor;
    [SerializeField] Color eveningColor;
    [SerializeField] Color nightColor;
    [SerializeField] float dayIntensity = 1f;
    [SerializeField] float eveningIntensity = 0.75f;
    [SerializeField] float nightIntensity = 0f;
    [SerializeField] float transitionTime = 2;

    private bool firstMorning = true;
    private bool allowedToSleep = false;
    private bool isSleeping = false;
    
    private float timer = 0;

    [SerializeField] Transform hourHandTransform;
    [SerializeField] Transform minuteHandTransform;
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
        light = FindObjectOfType<Light2D>();
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
        minuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay * hoursPerDay);
        

        lightChange();
    }


    void lightChange()
    {
        
        
        
        if (float.Parse(hourString) > 18 && float.Parse(hourString) < 24f)
        {
            
            timer += Time.deltaTime;
            allowedToSleep = true;
            light.intensity = Mathf.Lerp(dayIntensity, eveningIntensity, transitionTime * timer);
            light.color = Color.Lerp(dayColor, eveningColor, transitionTime * timer);
            firstMorning = false;
            // if (isSleeping)
            // {
            //     realSecondsPerIngameDay *= 4;
            // }
        } 
        
        else if (float.Parse(hourString) > 6 && float.Parse(hourString) < 11f && !firstMorning)
        {
            sleepText.text = "";
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerRenderer>().enabled = true;
            timer += Time.deltaTime;
            allowedToSleep = false;
            light.intensity = Mathf.Lerp(eveningIntensity, dayIntensity, transitionTime * timer);
            light.color = Color.Lerp(eveningColor, dayColor, transitionTime * timer);
            // if(isSleeping)
            // {
            //     realSecondsPerIngameDay /= 4;
            //     isSleeping = false;
            // }
        }
        else
        {
            timer = 0f;
        }
    }
    
    public event Action onSleep;

    public void Sleep()
    {
        
        if (allowedToSleep)
        {
            sleepText.text = "Sleeping";
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerRenderer>().enabled = false;
            
            isSleeping = true;
            onSleep?.Invoke();
        }
            
    }
}
