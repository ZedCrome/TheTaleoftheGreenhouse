using UnityEngine;

public class ObjectSlot : MonoBehaviour
{
    private SpriteRenderer renderer;

    public GameObject objectInSlot;
    private bool isFree;

    private Vector3 positionOffset;
    public enum SlotType { Table, Pot, Floor };
    private Vector3 tablePositionOffset = new Vector3(0, -0.25f, -0.7f);
    private Vector3 potPositionOffset = new Vector3(0, 0, -0.7f);
    private Vector3 floorPositionOffset = new Vector3(0, -0.25f, 0);

    [Header("Options")] 

    public bool allowPot;
    public bool allowWaterCan;

    public SlotType slotType;

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
    
    
    public bool FillSlot(GameObject newObject)
    {
        if(CheckAllowObject(newObject.tag) == false)
        {
            return false;
        }
        
        if(isFree)
        {
            objectInSlot = newObject;
            switch (slotType)
            {
                case SlotType.Table:
                    {
                        positionOffset = tablePositionOffset;
                        break;
                    }
                case SlotType.Pot:
                    {
                        positionOffset = potPositionOffset;
                        break;
                    }
                case SlotType.Floor:
                    {
                        positionOffset = floorPositionOffset;
                        break;
                    }
            }
            objectInSlot.transform.position = transform.position;
            objectInSlot.transform.position = objectInSlot.transform.position + positionOffset;


            renderer.enabled = false;
            
            isFree = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckAllowObject(string tag)
    {
        switch (tag)
        {
            case "Pot":

                if (allowWaterCan)
                    return true;
                break;
            
            case "WaterCan":

                if (allowWaterCan)
                    return true;
                break;
        }

        return false;
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
                objectInSlot.GetComponent<InteractableEffect>().Enable(true);
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
                objectInSlot.GetComponent<InteractableEffect>().Enable(true);
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
                objectInSlot.GetComponent<InteractableEffect>().Enable(false);
            }
        }

        if (objectInSlot != null)
        {
            ChangeMouseCursor.instance.inputObjectTag = objectInSlot.tag;
        }
    }

    
    private void OnMouseExit()
    {
        if (PlayerState.instance.currentInteractState == PlayerState.InteractState.@select)
        {
            if (isFree == false && objectInSlot != null)
            {
                objectInSlot.GetComponent<InteractableEffect>().Enable(false);
                PlayerInteract.instance.interactObject = null;
            }
        }

        if (PlayerState.instance.currentInteractState == PlayerState.InteractState.placement)
        {
            renderer.enabled = false;
        }

        ChangeMouseCursor.instance.inputObjectTag = "Default";
    }
}
