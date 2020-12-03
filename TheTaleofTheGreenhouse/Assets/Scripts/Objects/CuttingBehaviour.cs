using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBehaviour : MonoBehaviour
{

    public AudioClip[] plantCutting;
    public AudioClip alreadyPlantInPot;

    private AudioSource audioSource;
    
    private bool rightMouseButtonLock = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PlayerState.instance.currentHandState == PlayerState.HandState.Cutting)
        {
            if (Input.GetMouseButtonDown(1) && rightMouseButtonLock == false)
            {
                if (AllowedToDoAction())
                {
                    if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot == null)
                    {
                        GameObject objectToDestroy;

                        GameObject newPlantObject = PrefabManager.instance.CreateNewObjectInstance("PlantNormal");

                        if (newPlantObject != null)
                        {
                            objectToDestroy = PlayerInteract.instance.inventoryItem;
                            PlayerInteract.instance.inventoryItem = null;
                            Destroy(objectToDestroy);
                            PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().FillSlot(newPlantObject);
                        
                            PlayerState.instance.currentHandState = PlayerState.HandState.None;
                            audioSource.PlayOneShot(Tools.GetRandomSound(plantCutting));
                        }
                    }
                    else
                    {
                        audioSource.PlayOneShot(alreadyPlantInPot);
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
