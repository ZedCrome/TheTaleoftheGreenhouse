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

    private float timer;

    [SerializeField] Color dayColor;
    [SerializeField] Color nightColor;
    [SerializeField] float dayIntensity = 1f;
    [SerializeField] float nightIntensity = 0.75f;
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
        if (float.Parse(hourString) > 18 && float.Parse(hourString) < 24f)
        {
            
            timer += Time.deltaTime;
            light.intensity = Mathf.Lerp(1f, nightIntensity, transitionTime * timer);
            light.color = Color.Lerp(dayColor, nightColor, transitionTime * timer);
            firstMorning = false;
        } 
        else if (float.Parse(hourString) > 5 && float.Parse(hourString) < 11f && !firstMorning)
        {
            
            timer += Time.deltaTime;
            light.intensity = Mathf.Lerp(nightIntensity, dayIntensity, transitionTime * timer);
            light.color = Color.Lerp(nightColor, dayColor, transitionTime * timer);
        }
        else
        {
            timer = 0f;
        }
    }
    
    public event Action onSleep;

    public void Sleep()
    {
        onSleep?.Invoke();
    }
}
