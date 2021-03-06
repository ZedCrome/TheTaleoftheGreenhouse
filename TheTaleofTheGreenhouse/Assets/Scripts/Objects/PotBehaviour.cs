﻿using System;
using UnityEngine;

public class PotBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool isWatered = false;

    private SpriteRenderer renderer;

    public Sprite potDry;
    public Sprite potWet;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        if (isWatered)
        {
            renderer.sprite = potWet;
        }
        else
        {
            renderer.sprite = potDry;
        }
    }

    public void FillWater()
    {
        isWatered = true;
        renderer.sprite = potWet;
    }

    public bool GetIsWatered()
    {
        return isWatered;
    }

    public void EmptyWater()
    {
        isWatered = false;
        renderer.sprite = potDry;
    }
}

 
