using System;
using UnityEngine;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

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

    [SerializeField] Transform hourHandTransform;
    [SerializeField] Transform minuteHandTransform;
    

    
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
        float timer = 0;
        if (float.Parse(hourString) > 18 && float.Parse(hourString) < 24f)
        {
            
            timer += Time.deltaTime;
            light.intensity = Mathf.Lerp(dayIntensity, eveningIntensity, transitionTime * timer);
            light.color = Color.Lerp(dayColor, eveningColor, transitionTime * timer);
            firstMorning = false;
        } 
        else if (float.Parse(hourString) > 5 && float.Parse(hourString) < 11f && !firstMorning)
        {
            
            timer += Time.deltaTime;
            light.intensity = Mathf.Lerp(eveningIntensity, dayIntensity, transitionTime * timer);
            light.color = Color.Lerp(eveningColor, dayColor, transitionTime * timer);
        }
        else
        {
            timer = 0f;
        }
    }
    
    public event Action onSleep;

    public void Sleep()
    {
        float timer = 0;
        if (float.Parse(hourString) > 22 && float.Parse(hourString) < 2)
        {
            timer += Time.deltaTime;
            light.intensity = Mathf.Lerp(eveningIntensity, nightIntensity, transitionTime * 3 * timer);
            light.color = Color.Lerp(eveningColor, nightColor, transitionTime * 3 * timer);
        }
        else if (float.Parse(hourString) > 6 && float.Parse(hourString) < 11)
        {
            timer += Time.deltaTime;
            light.intensity = Mathf.Lerp(nightIntensity, dayIntensity, transitionTime * 3 * timer);
            light.color = Color.Lerp(nightColor, dayColor, transitionTime * 3 * timer);
        }
        else
        {
            timer = 0f;
        }
        
        onSleep?.Invoke();
    }
}
