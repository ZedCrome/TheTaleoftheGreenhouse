using System;
using UnityEngine;

public class PotBehaviour : MonoBehaviour
{
    public bool isWatered = false;

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
        Debug.Log("Watered");
    }

    public bool GetWatered()
    {
        return isWatered;
    }

    public void EmptyWater()
    {
        isWatered = false;
        renderer.sprite = potDry;
    }
}

 
