using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    private SpriteRenderer renderer;
    
    private Color mouseOverColor;
    private Color standardColor;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        mouseOverColor = Color.red;
        standardColor = renderer.material.color;

        if (!gameObject.CompareTag("Interactable"))
        {
            gameObject.tag = "Interactable";
        }
    }

    void OnMouseEnter()
    {
        if (PlayerState.instance.currentInteractState == PlayerState.InteractState.select)
        {
            renderer.material.color = mouseOverColor;
            PlayerInteract.instance.mouseOverItem = this.gameObject;
        }
    }

    private void OnMouseExit()
    {
        if (PlayerState.instance.currentInteractState == PlayerState.InteractState.select)
        {
            renderer.material.color = standardColor;
            PlayerInteract.instance.mouseOverItem = null;
        }
    }
}
