using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeatherManager : MonoBehaviour
{
    public static WeatherManager instance;
    
    private AudioSource audioSource;
    private Animator animatorGlobal;
    private Animator[] animatorWindows;
    
    public AudioClip audioClip;
    public bool fire;

    public bool lightningActive;
    public float counter = 0;
    private float counterGoal;
    
    private void Start()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
        
        audioSource = GetComponent<AudioSource>();
        animatorGlobal = GetComponent<Animator>();
        animatorWindows = GetComponentsInChildren<Animator>();
        counterGoal = Random.Range(20f, 40f);
    }
    
    private void OnEnable()
    {
        DayNightCycle.instance.onSleep += OnSleep;
    }
    
    private void OnDisable()
    {
        DayNightCycle.instance.onSleep -= OnSleep;
    }
    
    private void Update()
    {
        if (lightningActive == false)
        {
            return;
        }
        
        if (counter >= 20)
        {
            fire = true;
            counterGoal = Random.Range(20f, 40f);
            counter = 0;
        }
        
        
        if (fire)
        {
            //animatorGlobal.SetTrigger("PlayLightning");
            foreach (Animator anim in animatorWindows)
            {
                anim.SetTrigger("PlayLightning");
            }
            
            audioSource.PlayOneShot(audioClip);
            fire = false;
        }

        counter += Time.deltaTime;
    }

    void OnSleep()
    {
        int randomSeed = Random.Range(0, 4);
        
        if (randomSeed == 0)
        {
            lightningActive = true;
        }
        else
        {
            lightningActive = false;
        }
    }
}
