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
    
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
        
        if (objectInSlot == null)
        {
            isFree = true;
        }
    }
    
    public void FillSlot(GameObject newObject)
    {
        if(isFree)
        {
            objectInSlot = newObject;
            objectInSlot.transform.position = transform.position;
            
            isFree = false;
        }
    }

    public GameObject GetThisObject()
    {
        GameObject returnObject = objectInSlot;
        objectInSlot = null;
        
        return returnObject;
    }
    
    void OnMouseEnter()
    {
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
