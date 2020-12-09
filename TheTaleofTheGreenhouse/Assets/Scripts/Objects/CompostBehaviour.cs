using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostBehaviour : MonoBehaviour
{
    public GameObject lidOpen;
    public GameObject lidClosed;

    private void Start()
    {
        lidOpen = transform.Find("LidOpen").gameObject;
        lidClosed = transform.Find("LidClosed").gameObject;
        
        lidOpen.SetActive(false);
        lidClosed.SetActive(true);
    }

    private void OnMouseOver()
    {
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }
        
        if (PlayerInteract.instance.allowedTointeract)
        {
            lidOpen.SetActive(true);
            lidClosed.SetActive(false);

            if (Input.GetMouseButtonDown(1))
            {
                if (PlayerInteract.instance.inventoryItem.CompareTag("CuttingNormal") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("CuttingMana") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("PlantMana") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("PlantNormal"))
                {
                    GameObject objectToDestroy = PlayerInteract.instance.inventoryItem;
                    PlayerInteract.instance.inventoryItem = null;

                    Destroy(objectToDestroy);
                    
                    PlayerState.instance.ChangeHandState(PlayerState.HandState.None);
                    PlayerState.instance.ChangeInteractState(PlayerState.InteractState.@select);
                }
            }
        }
        else
        {
            lidOpen.SetActive(false);
            lidClosed.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        lidOpen.SetActive(false);
        lidClosed.SetActive(true);
    }
}
