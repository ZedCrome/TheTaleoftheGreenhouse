using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBehaviour : MonoBehaviour
{

    public AudioClip[] plantCutting;
    public AudioClip alreadyPlantInPot;
    
    private bool rightMouseButtonLock = false;
    
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
                        GameObject newPlantObject;
                        if (this.gameObject.CompareTag("CuttingNormal"))
                        {
                            newPlantObject = PrefabManager.instance.CreateNewObjectInstance("PlantNormal");
                            newPlantObject.GetComponent<PlantStates>().currentState = PlantStates.PlantState.Cutting;
                        }
                        else
                        {
                            newPlantObject = PrefabManager.instance.CreateNewObjectInstance("PlantMana");
                        }

                        if (newPlantObject != null)
                        {
                            objectToDestroy = PlayerInteract.instance.inventoryItem;
                            PlayerInteract.instance.inventoryItem = null;
                            Destroy(objectToDestroy);
                            newPlantObject.transform.localScale = new Vector3(1,1,1);
                            PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().FillSlot(newPlantObject);
                        
                            PlayerState.instance.ChangeHandState(PlayerState.HandState.None);
                        }
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
