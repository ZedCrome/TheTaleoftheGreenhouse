using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;

    public AudioClip audioClip;
    public bool fire;

    public float counter = 0;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        if (counter >= 20)
        {
            fire = true;
            counter = 0;
        }
        
        
        if (fire)
        {
            animator.SetTrigger("PlayLightning");
            audioSource.PlayOneShot(audioClip);
            fire = false;
        }

        counter += Time.deltaTime;
    }
}
