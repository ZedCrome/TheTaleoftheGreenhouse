using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract instance;
    
    public GameObject inventoryItem;
    public GameObject interactObject;

    private bool leftMouseButtonLock = false;

    public float maxInteractDistance = 1.0f;
    
    void Start()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
    }
    void Update()
    {
        Vector3 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        float currentDistance = Vector3.Distance(transform.position, Camera.main.ScreenToWorldPoint(mousePosition));
        
        if(Input.GetMouseButtonDown(0) && leftMouseButtonLock == false && currentDistance <= maxInteractDistance)
        {
            if(interactObject != null && inventoryItem == null && leftMouseButtonLock == false)
            {
                inventoryItem = interactObject.GetComponent<ObjectSlot>().GetThisObject();
                PlayerState.instance.currentInteractState = PlayerState.InteractState.placement;
                
                leftMouseButtonLock = true;
            }
            
            if(interactObject != null && inventoryItem != null && leftMouseButtonLock == false)
            {
                interactObject.GetComponent<ObjectSlot>().FillSlot(inventoryItem);
                inventoryItem = null;
                PlayerState.instance.currentInteractState = PlayerState.InteractState.@select;
                
                leftMouseButtonLock = true;
            }
        }

        if(inventoryItem != null)
        {
            Vector3 newObjectPosition = new Vector3(mousePosition.x, mousePosition.y, 10);
            inventoryItem.transform.position =
                Camera.main.ScreenToWorldPoint(newObjectPosition);
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            leftMouseButtonLock = false;
        }
    }
}