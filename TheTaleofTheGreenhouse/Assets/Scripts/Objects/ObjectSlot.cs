using UnityEngine;

public class ObjectSlot : MonoBehaviour
{
    private SpriteRenderer renderer;

    public GameObject objectInSlot;
    private bool isFree;

    private Vector3 positionOffset;
    public enum SlotType { Table, Pot, Floor };
    private Vector3 tablePositionOffset = new Vector3(0, -0.25f, -0.7f);
    private Vector3 potPositionOffset = new Vector3(0, 0, -0.8f);
    private Vector3 floorPositionOffset = new Vector3(0, -0.25f, 0);

    [Header("Options")]

    public bool blockPot = true;
    public bool blockWaterCan;
    public bool blockCutting;

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
    
    private void OnEnable()
    {
        PlayerState.instance.onChangeInteractState += OnChangeInteractiveState;
        PlayerState.instance.onChangeHandState += OnChangeHandState;
    }
    
    private void OnDisable()
    {
        PlayerState.instance.onChangeInteractState -= OnChangeInteractiveState;
        PlayerState.instance.onChangeHandState -= OnChangeHandState;
    }
    
    
    public bool FillSlot(GameObject newObject)
    {
        if(CheckBlockObject(newObject.tag))
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
                        blockWaterCan = true;
                        break;
                    }
                case SlotType.Pot:
                    {
                        positionOffset = potPositionOffset;
                        blockWaterCan = false;
                        break;
                    }
                case SlotType.Floor:
                    {
                        positionOffset = floorPositionOffset;
                        blockWaterCan = true;
                        break;
                    }
            }
            objectInSlot.transform.position = transform.position;
            objectInSlot.transform.position = objectInSlot.transform.position + positionOffset;

            if (slotType == SlotType.Pot)
            {
                objectInSlot.transform.parent = transform;
            }
            //Else code is for Table only.
            //May create bugs for floor. Import table and floor transforms later.
            else
            {
                objectInSlot.transform.parent = transform.parent;
            }

            renderer.enabled = false;
            
            isFree = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckBlockObject(string tag)
    {
        switch (tag)
        {
            case "Pot":

                if (blockPot)
                    return true;
                break;
            
            case "WaterCan":

                if (blockWaterCan)
                    return true;
                break;
            
            case "Cutting":

                if (blockCutting)
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

    void OnChangeInteractiveState(PlayerState.InteractState state)
    {
        
    }
    
    void OnChangeHandState(PlayerState.HandState state)
    {
        if (state == PlayerState.HandState.WaterCan && objectInSlot != null)
        {
            if (slotType == SlotType.Table)
            {
                this.gameObject.layer = 2;
            }          
        }

        else if(state == PlayerState.HandState.Pot && objectInSlot != null)
        {
            if (slotType == SlotType.Table)
            {
                this.gameObject.layer = 2;
            }
        }

        else
        {
            if (slotType == SlotType.Table)
            {
                this.gameObject.layer = 0;
            }
        }
    }
    
    void OnMouseEnter()
    {
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }
        
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
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }
        
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
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }
        
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

        if (objectInSlot != null)
        {
            if (objectInSlot.HasComponent<InteractableEffect>())
            {
                objectInSlot.GetComponent<InteractableEffect>().Enable(false);
            }
        }
    }
}
