using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrowelBehaviour : MonoBehaviour
{
    // private bool rightMouseButtonLock = false;
    //
    //     private void Update()
    // {
    //     if (PlayerState.instance.currentHandState == PlayerState.HandState.Trowel)
    //     {
    //         if (Input.GetMouseButtonDown(1) && rightMouseButtonLock == false)
    //         {
    //             if (AllowedToDoAction())
    //             {
    //                 PlayerInteract.instance.PickUp(PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().GetThisObject());
    //             }
    //
    //             rightMouseButtonLock = true;
    //         }
    //     }
    //     else if (PlayerInventory.instance.trowelEquiped != null)
    //     {
    //         
    //     }
    //
    //     if (Input.GetMouseButtonUp(1))
    //     {
    //         rightMouseButtonLock = false;
    //     }
    // }
    //
    // private bool AllowedToDoAction()
    // {
    //     if (PlayerInteract.instance.allowedTointeract)
    //     {
    //         if (PlayerInventory.instance.CanCarryMoreCuttings()) 
    //         {
    //             if (PlayerInteract.instance.interactObject != null)
    //             {
    //                 if (PlayerInteract.instance.interactObject.CompareTag("PotSlot"))
    //                 {
    //                     if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot != null)
    //                     {
    //                         if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.CompareTag("PlantNormal") ||
    //                             PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.CompareTag("PlantMana"))
    //                         {
    //                             return true;
    //                         }
    //                     }
    //                 }
    //             }
    //         }
    //     }
    //
    //     return false;
    // }
}
