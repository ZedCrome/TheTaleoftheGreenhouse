using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShearsBehaviour : MonoBehaviour
{
    public AudioClip[] cutSound;
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
                        PlayerInventory.instance.AddCutting(PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.tag);
                        audioSource.PlayOneShot(Tools.GetRandomSound(cutSound));
                    }
                    else
                    {
                        GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.CuttingsWarning);
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
            if (PlayerInventory.instance.CanCarryMoreCuttings()) 
            {
                if (PlayerInteract.instance.interactObject != null)
                {
                    if (PlayerInteract.instance.interactObject.CompareTag("PotSlot"))
                    {
                        if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot != null)
                        {
                            if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.CompareTag("PlantNormal"))
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
