using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract instance;
    
    private AudioSource audioSource;
    public AudioSource interactSound;
    public AudioClip wateringSound;
    
    public GameObject inventoryItem;
    public GameObject interactObject;

    public Transform reachAnchor;
    
    private string[] skipSoundTagArray;

    public float maxInteractDistance = 1.0f;
    public Vector2 maxInteractDistanceModifier;

    public bool allowedTointeract = false;
    private bool leftMouseButtonLock = false;

    private bool firstTimeUsingWatercan = true;
    private bool firstTimeUsingShears= true;
    private bool firstTimeUsingMana = true;

    void Start()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }

        skipSoundTagArray = new[] {"PlantMana", "PlantNormal", "CuttingNormal", "CuttingMana", "WaterCan", "Shears"};
        
        audioSource = GetComponent<AudioSource>();
        reachAnchor = GameObject.Find("ReachAnchor").transform;
    }
    
    
    void Update()
    {
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }
        
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        CalculateAllowedToInteract(mousePosition);
        
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
            inventoryItem.transform.position = new Vector3(newObjectPosition.x, newObjectPosition.y, -1.5f);
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            leftMouseButtonLock = false;
        }
    }

    private void CalculateAllowedToInteract( Vector2 newMousePosition)
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(newMousePosition);
        Vector2 direction = mouseWorldPosition - new Vector2(reachAnchor.position.x, reachAnchor.position.y);
        float distanceToMouse = direction.sqrMagnitude;
        Vector2 directionBorder = direction.normalized * maxInteractDistance;
        directionBorder = Vector2.Scale(directionBorder, maxInteractDistanceModifier);
        
        if (directionBorder.sqrMagnitude > distanceToMouse)
        {
            allowedTointeract = true;
            ChangeMouseCursor.instance.ChangeAlpha(true);
        }
        else
        {
            allowedTointeract = false;
            ChangeMouseCursor.instance.ChangeAlpha(false);
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
                if (firstTimeUsingWatercan)
                {
                        NoteManager.instance.ActivateNote(NoteManager.NoteStates.PlantNote);
                        firstTimeUsingWatercan = false;
                }
                break;

            case "Pot":
                PlayerState.instance.ChangeHandState(PlayerState.HandState.Pot);
                break;
            
            case "Shears":
                PlayerState.instance.ChangeHandState(PlayerState.HandState.Shears);
                if (firstTimeUsingShears)
                {
                    NoteManager.instance.ActivateNote(NoteManager.NoteStates.ShearsNote);
                    firstTimeUsingShears = false;
                }
                break;

            case "ManaCatcher":
                PlayerState.instance.ChangeHandState(PlayerState.HandState.ManaCatcher);
                if (firstTimeUsingMana)
                {
                    NoteManager.instance.ActivateNote(NoteManager.NoteStates.ManaNote);
                    firstTimeUsingMana = false;
                }
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
            if(Tools.LookForTagInArray(inventoryItem.tag, skipSoundTagArray) == false)
            {
                interactSound.Play();
            }
            
            inventoryItem = null;
            PlayerState.instance.ChangeInteractState(PlayerState.InteractState.@select);
            PlayerState.instance.ChangeHandState(PlayerState.HandState.None);
        }
        else
        {
            //Feedback sound not able to place
        }
    }
}