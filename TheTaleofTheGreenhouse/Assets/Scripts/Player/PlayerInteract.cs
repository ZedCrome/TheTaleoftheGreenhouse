using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private AudioSource audioSource;
    
    public static PlayerInteract instance;
    public AudioSource interactSound;
    public AudioClip wateringSound;
    
    public GameObject inventoryItem;
    public GameObject interactObject;

    public float maxInteractDistance = 1.0f;

    public bool allowedTointeract = false;
    private bool leftMouseButtonLock = false;

    void Start()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }

        audioSource = GetComponent<AudioSource>();
    }
    
    
    void Update()
    {
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }
        
        Vector3 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        float currentDistance = Vector3.Distance(transform.position, Camera.main.ScreenToWorldPoint(mousePosition));

        if (currentDistance <= maxInteractDistance)
        {
            allowedTointeract = true;
        }
        else
        {
            allowedTointeract = false;            
        }


        if (allowedTointeract)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (PlayerState.instance.currentInteractState == PlayerState.InteractState.@select && leftMouseButtonLock == false)
                {
                    if (interactObject != null && inventoryItem == null)
                    {
                        PickUp(interactObject.GetComponent<ObjectSlot>().GetThisObject());
                    }
                    
                    leftMouseButtonLock = true;
                }
                else if (PlayerState.instance.currentInteractState == PlayerState.InteractState.placement && leftMouseButtonLock == false)
                {
                    if (interactObject != null && inventoryItem != null)
                    {
                        Place(inventoryItem);
                    }
                    
                    leftMouseButtonLock = true;
                }
            }
        }
        
        // Follow cursor, removed later?
        if(inventoryItem != null)
        {
            Vector3 newObjectPosition = new Vector3(mousePosition.x, mousePosition.y, 10);
            newObjectPosition = Camera.main.ScreenToWorldPoint(newObjectPosition);
            inventoryItem.transform.position = new Vector3(newObjectPosition.x, newObjectPosition.y, -1.4f);
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            leftMouseButtonLock = false;
        }
    }

    private void PickUp(GameObject pickedObject)
    {
        if (pickedObject == null)
        {
            return;
        }
        
        inventoryItem = pickedObject;
        
        switch (pickedObject.tag)
        {
            case "WaterCan":           
                PlayerState.instance.ChangeHandState(PlayerState.HandState.WaterCan);
                break;

            case "Pot":
                PlayerState.instance.ChangeHandState(PlayerState.HandState.Pot);
                break;
            
            case "Shears":
                PlayerState.instance.ChangeHandState(PlayerState.HandState.Shears);
                break;

            case "ManaCatcher":
                PlayerState.instance.ChangeHandState(PlayerState.HandState.ManaCatcher);
                break;

            case "PlantNormal":
                PlayerState.instance.ChangeHandState(PlayerState.HandState.Plant);
                break;

            case "PlantMana":
                PlayerState.instance.ChangeHandState(PlayerState.HandState.Plant);
                break;

            default:               
                PlayerState.instance.ChangeHandState(PlayerState.HandState.None);
                break;
        }
        
        PlayerState.instance.ChangeInteractState(PlayerState.InteractState.placement);
        interactSound.Play();
    }

    private void Place(GameObject placeObject)
    {
        bool result = interactObject.GetComponent<ObjectSlot>().FillSlot(placeObject);

        if(result)
        {
            inventoryItem = null;
            PlayerState.instance.ChangeInteractState(PlayerState.InteractState.@select);
            PlayerState.instance.ChangeHandState(PlayerState.HandState.None);
            
            interactSound.Play();
        }
        else
        {
            //Feedback sound not able to place
        }
    }
}