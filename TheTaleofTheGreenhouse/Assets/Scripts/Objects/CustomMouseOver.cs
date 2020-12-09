using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMouseOver : MonoBehaviour
{
    private InteractableEffect interactableEffect;

    private void Start()
    {
        interactableEffect = GetComponent<InteractableEffect>();
    }

    private void OnMouseOver()
    {
        interactableEffect.Enable(true);
    }

    private void OnMouseExit()
    {
        interactableEffect.Enable(false);
    }
}
