using System;
using UnityEngine;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle instance;
    public Light2D light;
    private float realSecondsPerIngameDay = 20;

    public static float day;
    public float hoursPerDay = 24f;
    public float minutesPerHour = 60f;

    private string hourString;
    private string minutesString;

    
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
    
    
    void Update()
    {
        day += Time.deltaTime / realSecondsPerIngameDay;

        float dayNormalized = day % 1f;

        hourString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");

        minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");
        
        Debug.Log("Time: " + hourString + " : " + minutesString);
        lightChange();
    }


    void lightChange()
    {
        var lightColor = light.color;
        if (float.Parse(hourString) < 20 && float.Parse(hourString) > 6f)
        {
            if (light.intensity < 1)
            {
                light.intensity += (float) 0.1 * Time.deltaTime;
                lightColor.r += (float) 0.1;
                lightColor.g += (float) 0.1;
                lightColor.b += (float) 0.1;
                
            }
        }
        
        if (!(float.Parse(hourString) < 20 && float.Parse(hourString) > 6f))
        {
            if (light.intensity > (float) 0.75)
            {
                light.intensity -= (float)0.1 * Time.deltaTime;
                lightColor.r -= (float) 0.1;
                lightColor.g -= (float) 0.1;
                lightColor.b -= (float) 0.1;
            } 
        }
    }
}
