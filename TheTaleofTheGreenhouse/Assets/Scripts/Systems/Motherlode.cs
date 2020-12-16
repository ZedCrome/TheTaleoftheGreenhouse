﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-8)]
public class Motherlode : MonoBehaviour
{
    public static Motherlode instance;
    
    private AudioSource audioSource;
    public AudioClip giveGold;
    public AudioClip knifeMana;
    
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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (GameManager.instance.debugActive)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                AddGoldCheat(50);

                audioSource.PlayOneShot(giveGold);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                AddManaCheat(0.1f);

                audioSource.PlayOneShot(knifeMana);
            }
        }
    }

    public event Action<int> addGoldCheat;
    public void AddGoldCheat(int amount)
    {
        addGoldCheat?.Invoke(amount);
    }
    
    public event Action<float> addManaCheat;
    public void AddManaCheat(float amount)
    {
        addManaCheat?.Invoke(amount);
    }
    
}
