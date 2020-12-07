using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBoxBehaviour : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Color standardColor;
    private Color interactableColor;

    public GameObject[] itemsToSell;
    public int maxNumbertoSell = 9;
    private int currentSlot;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        standardColor = spriteRenderer.material.color;
        interactableColor = Color.grey;

        itemsToSell = new GameObject[maxNumbertoSell];
    }


    private void OnMouseOver()
    {
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }

        if (PlayerInteract.instance.allowedTointeract)
        {
            spriteRenderer.color = interactableColor;

            if (Input.GetMouseButtonDown(0))
            {
                if (PlayerInteract.instance.inventoryItem.CompareTag("Cutting") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("PlantMana") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("PlantNormal") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("Pot") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("ManaStorage"))
                {
                    GameObject sellItem = PlayerInteract.instance.inventoryItem;
                    PlayerInteract.instance.inventoryItem = null;
                    if (sellItem.tag == "PlantMana" || sellItem.tag == "PlantNormal")
                    {
                        sellItem.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
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
            }
        }
        else
        {
            spriteRenderer.color = standardColor;
        }
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = standardColor;
    }
}
