using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShearsBehaviour : MonoBehaviour
{
    public AudioClip cutSound;
    public AudioClip alreadyCut;

    private AudioSource audioSource;
    
    private bool rightMouseButtonLock = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PlayerState.instance.currentHandState == PlayerState.HandState.Shears)
        {
            if (Input.GetMouseButtonDown(1) && rightMouseButtonLock == false)
            {
                if (AllowedToDoAction())
                {
                    if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.GetComponent<PlantStates>().CutPlant() == true)
                    {
                        PlayerInventory.instance.AddSapling();
                        audioSource.PlayOneShot(cutSound);
                    }
                    else
                    {
                        audioSource.PlayOneShot(alreadyCut);
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
            if (PlayerInventory.instance.CanCarryMoreSaplings()) 
            {
                if (PlayerInteract.instance.interactObject != null)
                {
                    if (PlayerInteract.instance.interactObject.CompareTag("PotSlot"))
                    {
                        if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot != null)
                        {
                            if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.CompareTag("PlantNormal") ||
                                PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.CompareTag("PlantMana"))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }

        return false;
    }
}
