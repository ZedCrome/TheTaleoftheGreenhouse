using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class LightFlicker : MonoBehaviour
{
    private Light2D light;

    public float minIntensity = 0.1f;
    public float maxIntensity = 0.5f;
    public float stepModifier = 1.5f;
    private float currentIntensity;
    private bool direction;
    
    private void Start()
    {
        light = GetComponent<Light2D>();
        currentIntensity = Random.Range(minIntensity, maxIntensity);
    }

    private void Update()
    {
        if (currentIntensity < minIntensity)
        {
            direction = true;
        }

        if (currentIntensity > maxIntensity)
        {
            direction = false;
        }

        if (direction)
        {
            currentIntensity += stepModifier * Time.deltaTime;
        }
        else
        {
            currentIntensity -= stepModifier * Time.deltaTime;
        }
        
        light.intensity = currentIntensity;
    }
}
