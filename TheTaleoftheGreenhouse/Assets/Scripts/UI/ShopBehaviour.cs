﻿using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject buyMenu;

    
    
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            LeanTween.scale(buyMenu, new Vector3(1, 1, 1), 0.5f).setOnComplete(activateShop);
        }
    }
    

    void activateShop()
    {
        buyMenu.SetActive(true);
    }


    private void OnMouseEnter()
    {
        ChangeMouseCursor.instance.inputObjectTag = gameObject.tag;
    }

    private void OnMouseExit()
    {
        ChangeMouseCursor.instance.inputObjectTag = "Default";
    }
}