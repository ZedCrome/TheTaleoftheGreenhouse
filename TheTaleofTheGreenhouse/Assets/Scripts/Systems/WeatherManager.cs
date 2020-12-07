using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeatherManager : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animatorGlobal;
    private Animator[] animatorWindows;
    
    public AudioClip audioClip;
    public bool fire;

    public float counter = 0;
    private float counterGoal;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animatorGlobal = GetComponent<Animator>();
        animatorWindows = GetComponentsInChildren<Animator>();
        counterGoal = Random.Range(20f, 40f);
    }
    private void Update()
    {

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
}
