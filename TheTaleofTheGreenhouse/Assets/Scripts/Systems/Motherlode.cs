using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motherlode : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip giveGold;
    
    public ShopBehaviourBuy shopBehaviourBuy;

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
                shopBehaviourBuy.AddGold(50);

                audioSource.PlayOneShot(giveGold);
            }
        }
    }
}
