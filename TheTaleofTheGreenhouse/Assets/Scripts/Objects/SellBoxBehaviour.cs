using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBoxBehaviour : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public GameObject[] itemsToSell;
    public int maxNumbertoSell = 9;
   
    private Color standardColor;
    private Color interactableColor;


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
                    PlayerInteract.instance.inventoryItem.CompareTag("PlantNormal"))
                {
                    GameObject sellItem = PlayerInteract.instance.inventoryItem;
                    PlayerInteract.instance.inventoryItem = null;
                    sellItem.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);

                    for (int i = 0; i < maxNumbertoSell; i++)
                    {
                        if (itemsToSell[i] == null)
                        {
                            itemsToSell[i] = sellItem;
                        }
                        break;
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
