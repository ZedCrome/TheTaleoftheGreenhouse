using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSlot : MonoBehaviour
{
    private SpriteRenderer renderer;

    public GameObject objectInSlot;
    private bool isFree;
    
    [Header("Options")] 
    public bool allowPot;

    public GameObject objectToPut;    
    
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
        
        if (objectInSlot == null)
        {
            isFree = true;
        }
        
        // Temporary solution to fill item slots
        if (objectToPut != null)
        {
            FillSlot(objectToPut);
        }
    }
    
    public void FillSlot(GameObject newObject)
    {
        if(isFree)
        {
            objectInSlot = newObject;
            objectInSlot.transform.position = transform.position;
            
            renderer.enabled = false;
            
            isFree = false;
        }
    }

    public GameObject GetThisObject()
    {
        GameObject returnObject = objectInSlot;
        objectInSlot = null;
        
        isFree = true;
        
        return returnObject;
    }
    
    void OnMouseEnter()
    {
        if (PlayerInteract.instance.allowedTointeract == false)
        {
            return;
        }
        
        if (PlayerState.instance.currentInteractState == PlayerState.InteractState.@select)
        {
            if (isFree == false && objectInSlot != null)
            {
                objectInSlot.GetComponent<HoverEffect>().Enable(true);
                PlayerInteract.instance.interactObject = this.gameObject;
            }
        }
        
        if (PlayerState.instance.currentInteractState == PlayerState.InteractState.placement)
        {
            PlayerInteract.instance.interactObject = this.gameObject;
            renderer.enabled = true;
        }
    }

    private void OnMouseOver()
    {
        if (PlayerInteract.instance.allowedTointeract)
        {
            if (isFree == false && objectInSlot != null)
            {
                objectInSlot.GetComponent<HoverEffect>().Enable(true);
            } 
            else if (isFree == true && objectInSlot != null)
            {
                renderer.enabled = true;
            }
        }
        else
        {
            renderer.enabled = false;
            if (isFree == false && objectInSlot != null)
            {
                objectInSlot.GetComponent<HoverEffect>().Enable(false);
            }
        }
    }

    private void OnMouseExit()
    {
        if (PlayerState.instance.currentInteractState == PlayerState.InteractState.@select)
        {
            if (isFree == false && objectInSlot != null)
            {
                objectInSlot.GetComponent<HoverEffect>().Enable(false);
                PlayerInteract.instance.interactObject = null;
            }
        }
        
        if (PlayerState.instance.currentInteractState == PlayerState.InteractState.placement)
        {
            renderer.enabled = false;
        }
    }
}
