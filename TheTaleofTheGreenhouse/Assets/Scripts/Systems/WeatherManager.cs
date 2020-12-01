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
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (fire)
        {
            animator.SetTrigger("PlayLightning");
            audioSource.PlayOneShot(audioClip);
            fire = false;
        }   
    }
}
