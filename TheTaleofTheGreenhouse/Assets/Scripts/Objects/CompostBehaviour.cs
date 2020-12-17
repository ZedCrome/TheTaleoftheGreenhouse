using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostBehaviour : MonoBehaviour
{
    public GameObject lidOpen;
    public GameObject lidClosed;
    
    public string[] tagArray;

    public InteractableEffect interactEffect;

    private bool firstTimeUsing = true;
    
    private void Start()
    {
        lidOpen = transform.Find("LidOpen").gameObject;
        lidClosed = transform.Find("LidClosed").gameObject;
        interactEffect = transform.parent.GetComponent<InteractableEffect>();
        
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
            interactEffect.Enable(true);
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
                    else
                    {
                        GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.CompostWarning);
                    }
                }
 
            }
        }
        else
        {

            interactEffect.Enable(false);
            lidOpen.SetActive(false);
            lidClosed.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        interactEffect.Enable(false);
        lidOpen.SetActive(false);
        lidClosed.SetActive(true);
    }
}
