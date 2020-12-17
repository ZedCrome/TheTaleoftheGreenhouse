using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBoxBehaviour : MonoBehaviour
{
    public static SellBoxBehaviour instance;
    
    public GameObject[] itemsToSell;
    public int maxNumbertoSell = 9;
    private int currentSlot;
    bool firstTimeUsing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    
    private void Start()
    {
        itemsToSell = new GameObject[maxNumbertoSell];
        firstTimeUsing = true;
    }


    private void OnMouseOver()
    {
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }

        if (PlayerInteract.instance.allowedTointeract)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                if (firstTimeUsing)
                {
                    NoteManager.instance.ActivateNote(NoteManager.NoteStates.SellBasketNote);
                    firstTimeUsing = false;
                }

                if (PlayerInteract.instance.inventoryItem != null)
                {
                    if (PlayerInteract.instance.inventoryItem.CompareTag("PlantMana") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("PlantNormal") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("Pot") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("ManaStorage"))
                    {
                        GameObject sellItem = PlayerInteract.instance.inventoryItem;
                        PlayerInteract.instance.inventoryItem = null;
                        sellItem.GetComponent<InteractableEffect>().Enable(false);

                        if (sellItem.tag == "PlantMana" || sellItem.tag == "PlantNormal")
                        {
                            if (sellItem.GetComponent<PlantStates>().currentState == PlantStates.PlantState.Cutting)
                            {                               
                                PlayerState.instance.ChangeHandState(PlayerState.HandState.None);
                            }
                            sellItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        }
                        else if (sellItem.tag == "Pot")
                        {
                            sellItem.transform.GetChild(0).gameObject.SetActive(false);                          
                            sellItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        }
                        else
                        {
                            sellItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        }

                        if (currentSlot < maxNumbertoSell)
                        {
                            for (int i = currentSlot; i < maxNumbertoSell;)
                            {
                                if (itemsToSell[i] == null)
                                {
                                    itemsToSell[i] = sellItem;
                                    sellItem.transform.parent = gameObject.transform;
                                    currentSlot++;
                                }
                                break;
                            }
                        }
                        else
                        {
                            Debug.Log("Can't sell more items today");
                        }

                        PlayerState.instance.ChangeHandState(PlayerState.HandState.None);
                        PlayerState.instance.ChangeInteractState(PlayerState.InteractState.@select);
                    }
                    else
                    {
                        GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.SellWarning);
                    }
                }
            }              
        }
    }


    public void ResetSlots()
    {
        currentSlot = 0;
    }
}
