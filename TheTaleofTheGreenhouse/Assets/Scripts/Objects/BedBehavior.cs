using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedBehavior : MonoBehaviour
{
    private InteractableEffect interactEffect;

    private void Start()
    {
        interactEffect = GetComponent<InteractableEffect>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            
            DayNightCycle.instance.Sleep();
        }
    }

    private void OnMouseExit()
    {
        
    }
}
