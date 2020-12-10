using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostBehaviour : MonoBehaviour
{
    public GameObject lidOpen;
    public GameObject lidClosed;
    
    public string[] tagArray;

    private bool firstTimeUsing = true;
    
    private void Start()
    {
        lidOpen = transform.Find("LidOpen").gameObject;
        lidClosed = transform.Find("LidClosed").gameObject;
        
        lidOpen.SetActive(false);
        lidClosed.SetActive(true);

        tagArray = new[] {"CuttingNormal", "CuttingMana", "PlantMana", "PlantNormal"};
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

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                if (firstTimeUsing)
                {
                    NoteManager.instance.ActivateNote(NoteManager.NoteStates.CompostNote);
                    firstTimeUsing = false;
                }

                if (PlayerInteract.instance.inventoryItem != null)
                {
                    if (Tools.LookForTagInArray(PlayerInteract.instance.inventoryItem.tag, tagArray))
                    {
                        GameObject objectToDestroy = PlayerInteract.instance.inventoryItem;
                        PlayerInteract.instance.inventoryItem = null;

                        Destroy(objectToDestroy);

                        PlayerState.instance.ChangeHandState(PlayerState.HandState.None);
                        PlayerState.instance.ChangeInteractState(PlayerState.InteractState.@select);
                    }
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
