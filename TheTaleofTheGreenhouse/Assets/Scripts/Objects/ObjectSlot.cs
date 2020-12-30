using UnityEngine;

public class ObjectSlot : MonoBehaviour
{
    private SpriteRenderer renderer;
    private AudioSource audioSource;
    
    public GameObject objectInSlot;
    private bool isFree;
    
    private Vector3 positionOffset;
    public enum SlotType { Table, Pot, Floor, ManaCube, Delivery };
    private Vector3 tablePositionOffset = new Vector3(0, 0.35f, 0);
    private Vector3 potPositionOffset = new Vector3(0, 0, -0.8f);
    private Vector3 manaCubePositionOffset = new Vector3(0, 0.2f, -0.8f);
    private Vector3 floorPositionOffset = new Vector3(0, -0.25f, 0);
    private Vector3 deliveryPositionOffset = new Vector3(0, 0, -0.8f);

    [Header("Sounds")] 
    public AudioClip[] waterCanSuccess;
    public AudioClip[] plantSuccess;
    public AudioClip[] plantFail;
    public AudioClip[] shearsSuccess;
    private string[] plantTagArray;
    
    [Header("Options")]

    public bool blockPot;
    public bool blockWaterCan;
    public bool blockCutting;

    public SlotType slotType;

    public GameObject objectToPut;

    public int maxStackAmount = 3;
    
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
     
        plantTagArray = new[] {"PlantMana", "PlantNormal"};
        
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

        maxStackAmount = 3;
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

        int totalStack;
        
        if (this.transform.parent.gameObject.HasComponent<StackIndex>())
        {
            totalStack = this.transform.parent.gameObject.GetComponent<StackIndex>().indexNumber +
                         Tools.GetSplitStackSize(newObject);
        }
        else
        {
            totalStack = Tools.GetSplitStackSize(newObject);
        }
        
        if (totalStack > maxStackAmount)
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
                        blockWaterCan = false;
                        break;
                    }
                case SlotType.Pot:
                    {
                        positionOffset = potPositionOffset;
                        //blockWaterCan = true;
                        break;
                    }
                case SlotType.ManaCube:
                    {
                        positionOffset = manaCubePositionOffset;
                        blockWaterCan = false;
                        break;
                    }
                case SlotType.Floor:
                    {
                        positionOffset = floorPositionOffset;
                        blockWaterCan = false;
                        break;
                    }
                case SlotType.Delivery:
                    {
                        positionOffset = deliveryPositionOffset;
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
                objectInSlot.transform.parent = transform;
            }

            if (gameObject.CompareTag("DeliverySlot") == false)
            {
                if (Tools.LookForTagInArray(newObject.tag, plantTagArray))
                {
                    audioSource.PlayOneShot(Tools.GetRandomSound(plantSuccess));
                }
                else if (newObject.CompareTag("WaterCan"))
                {
                    audioSource.PlayOneShot(Tools.GetRandomSound(waterCanSuccess));
                }
                else if (newObject.CompareTag("Shears"))
                {
                    audioSource.PlayOneShot(Tools.GetRandomSound(shearsSuccess));
                }
            }
            
            objectInSlot.GetComponent<StackIndex>().indexNumber = Tools.GetStackNumber(objectInSlot);
            
            renderer.enabled = false;
            
            isFree = false;
            return true;
        }
        else
        {
            if (gameObject.CompareTag("DeliverySlot") == false)
            {
                if (Tools.LookForTagInArray(newObject.tag, plantTagArray))
                {
                    audioSource.PlayOneShot(Tools.GetRandomSound(plantFail));    
                }
                else if (newObject.CompareTag("WaterCan"))
                {
                    // PLAY FAIL SOUND FOR WATER CAN
                }
            }
            
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
            
            case "CuttingMana":

                if (blockCutting)
                    return true;
                break;
            
            case "CuttingNormal":

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

        else if (state == PlayerState.HandState.Plant)
        {
            if (slotType == SlotType.Table)
            {
                this.gameObject.layer = 2;
            }
        }

        else if (state == PlayerState.HandState.Cutting)
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
    
    private void OnMouseOver()
    {
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }

        if (PlayerInteract.instance.allowedTointeract)
        {
            if (PlayerState.instance.currentInteractState == PlayerState.InteractState.@select)
            {
                if (isFree == false && objectInSlot != null)
                {
                    objectInSlot.GetComponent<InteractableEffect>().Enable(true);
                    PlayerInteract.instance.interactObject = this.gameObject;
                }
                else if (isFree == true && objectInSlot != null)
                {
                    renderer.enabled = true;
                }
            }
            
            if (PlayerState.instance.currentInteractState == PlayerState.InteractState.placement)
            {
                if (objectInSlot != null && PlayerState.instance.currentHandState != PlayerState.HandState.None)
                {
                    objectInSlot.GetComponent<InteractableEffect>().Enable(true);
                }
                
                PlayerInteract.instance.interactObject = this.gameObject;
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
            PlayerInteract.instance.interactObject = null;
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
