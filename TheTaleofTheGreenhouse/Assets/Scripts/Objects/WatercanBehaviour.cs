using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatercanBehaviour : MonoBehaviour
{
    public AudioClip wateringSound;
    public AudioClip alreadyWatered;

    private AudioSource audioSource;
    
    private bool rightMouseButtonLock = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PlayerState.instance.currentHandState == PlayerState.HandState.WaterCan)
        {
            if (Input.GetMouseButtonDown(1) && rightMouseButtonLock == false)
            {
                if (AllowedToDoAction())
                {
                    if (PlayerInteract.instance.interactObject.transform.parent.GetComponent<PotBehaviour>().GetIsWatered())
                    {
                        audioSource.PlayOneShot(alreadyWatered);
                    }
                    else
                    {
                        audioSource.PlayOneShot(wateringSound);
                        PlayerInteract.instance.interactObject.transform.parent.GetComponent<PotBehaviour>().FillWater();                       
                    }
                    ObjectSlot currentPotSlot = PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>();
                    if (currentPotSlot.objectInSlot.GetComponent<PlantStates>().currentState == PlantStates.PlantState.Dead)
                    {
                        GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.WaterDeadPlant);
                    }

                }

                rightMouseButtonLock = true;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            rightMouseButtonLock = false;
        }
    }

    private bool AllowedToDoAction()
    {
        if (PlayerInteract.instance.allowedTointeract)
        {
            if (PlayerInteract.instance.interactObject != null)
            {
                if (PlayerInteract.instance.interactObject.CompareTag("PotSlot"))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
