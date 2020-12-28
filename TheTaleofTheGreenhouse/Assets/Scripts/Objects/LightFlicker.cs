using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class LightFlicker : MonoBehaviour
{
    private Light2D light;

    public float minIntensity = 0.2f;
    public float maxIntensity = 0.4f;
    public float intensityStepModifier = 0.9f;
    public float minRange = 0.8f;
    public float maxRange = 1.0f;
    public float rangeStepModifier = 0.8f;
    
    private float currentIntensity;
    private bool intensityDirection;
    private float currentRange;
    private bool rangeDirection;

    [Header("Time Sync")] [Space(5)]
    public bool useTime;
    [SerializeField]
    private float currentTime;
    [Space(5)] 
    public float dayTimeBlockStart;
    public float dayTimeBlockEnd;
    [Space(5)] 
    public float eveningTimeBlockStart;
    public float eveningTimeBlockEnd;
    [Space(5)] 
    public bool useIntensity;
    private float intensityTimer;
    public float intensityTransitionTime = 0.1f;
    [SerializeField]
    private float currentIntensityModifier;
    public float dayIntensityModifier;
    public float eveningIntensityModifier;
    [Space(5)] 
    public bool useColor;
    public float colorTransitionTime = 0.1f;
    [SerializeField]
    private float colorTimer;
    [SerializeField]
    private Color currentColor;
    public Color dayColor;
    public Color eveningColor;
    
    private void Start()
    {
        light = GetComponent<Light2D>();
        currentColor = light.color;
        currentIntensity = Random.Range(minIntensity, maxIntensity);
        currentRange = Random.Range(minRange, maxRange);
    }

    private void Update()
    {
        if (currentIntensity < minIntensity)
        {
            intensityDirection = true;
        }
        if (currentIntensity > maxIntensity)
        {
            intensityDirection = false;
        }

        if (currentRange < minRange)
        {
            rangeDirection = true;
        }

        if (currentRange > maxRange)
        {
            rangeDirection = false;
        }

        if (intensityDirection)
        {
            currentIntensity += intensityStepModifier * Time.deltaTime;
        }
        else
        {
            currentIntensity -= intensityStepModifier * Time.deltaTime;
        }

        if (rangeDirection)
        {
            currentRange += rangeStepModifier * Time.deltaTime;
        }
        else
        {
            currentRange -= rangeStepModifier * Time.deltaTime;
        }
        
        if (useTime)
        {
            currentTime = float.Parse(DayNightCycle.instance.hourString);
            
            if (currentTime >= dayTimeBlockStart && currentTime <= dayTimeBlockEnd)
            {
                if (useIntensity)
                {
                    if (currentIntensityModifier != dayIntensityModifier)
                    {
                        intensityTimer += Time.deltaTime;
                        currentIntensityModifier =
                            Mathf.Lerp(currentIntensityModifier, dayIntensityModifier,
                                intensityTransitionTime * intensityTimer);
                    }
                    else
                    {
                        intensityTimer = 0;
                    }
                }
                
                if (useColor)
                {
                    if (currentColor != dayColor)
                    {
                        colorTimer += Time.deltaTime;
                        light.color = Color.Lerp(currentColor, dayColor, colorTransitionTime * colorTimer);
                    }
                    else
                    {
                        colorTimer = 0;
                    }
                }
            }
            
            if (currentTime >= eveningTimeBlockStart && currentTime <= eveningTimeBlockEnd)
            {
                if (useIntensity)
                {
                    if (currentIntensityModifier != dayIntensityModifier)
                    {
                        intensityTimer += Time.deltaTime;
                        currentIntensityModifier = Mathf.Lerp(currentIntensityModifier, eveningIntensityModifier,
                            intensityTransitionTime * intensityTimer);
                    }
                    else
                    {
                        intensityTimer = 0;
                    }
                }
                
                if (useColor)
                {
                    if (currentColor != eveningColor)
                    {
                        colorTimer += Time.deltaTime;
                        light.color = Color.Lerp(currentColor, eveningColor, colorTransitionTime * colorTimer);
                    }
                    else
                    {
                        colorTimer = 0;
                    }
                }
            }

            currentColor = light.color;
        }
        
        light.intensity = currentIntensity + currentIntensityModifier;
        light.pointLightOuterRadius = currentRange;
    }
}
