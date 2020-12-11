using System;
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
    
    
    private void Start()
    {
        light = GetComponent<Light2D>();
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
        
        light.intensity = currentIntensity;
        light.pointLightOuterRadius = currentRange;
    }
}
