﻿using UnityEngine;

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
    private bool rightMouseButtonLock = false;
    
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
            if(Input.GetMouseButtonDown(0) && leftMouseButtonLock == false)
            {
                if(interactObject != null && inventoryItem == null && leftMouseButtonLock == false)
                {
                    inventoryItem = interactObject.GetComponent<ObjectSlot>().GetThisObject();

                    if (inventoryItem.tag == "WaterCan")
                    {
                        PlayerState.instance.ChangeHandState(PlayerState.HandState.WaterCan);
                    }

                    PlayerState.instance.ChangeInteractState(PlayerState.InteractState.placement);
                    
                    leftMouseButtonLock = true;
                    interactSound.Play();
                }
            
                if(interactObject != null && inventoryItem != null && leftMouseButtonLock == false)
                {
                    bool result = interactObject.GetComponent<ObjectSlot>().FillSlot(inventoryItem);

                    if(result)
                    {
                        inventoryItem = null;
                        PlayerState.instance.ChangeInteractState(PlayerState.InteractState.@select);
                        PlayerState.instance.ChangeHandState(PlayerState.HandState.None);
                    
                    
                        interactSound.Play();
                    }
                
                    leftMouseButtonLock = true;
                }   
            }

            if (Input.GetMouseButtonDown(1) && rightMouseButtonLock == false)
            {
                if(PlayerState.instance.currentHandState == PlayerState.HandState.WaterCan)
                {
                    if(interactObject != null)
                    {
                        if (interactObject.CompareTag("PotSlot"))
                        {
                            audioSource.PlayOneShot(wateringSound);
                            interactObject.transform.parent.GetComponent<PotBehaviour>().FillWater();
                        }
                    }
        
                    rightMouseButtonLock = true;
                }
            }
        }
        
        if(inventoryItem != null)
        {
            Vector3 newObjectPosition = new Vector3(mousePosition.x, mousePosition.y, 10);
            newObjectPosition = Camera.main.ScreenToWorldPoint(newObjectPosition);
            inventoryItem.transform.position = new Vector3(newObjectPosition.x, newObjectPosition.y, -0.7f);
      
            
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            leftMouseButtonLock = false;
        }
        if(Input.GetMouseButtonUp(1))
        {
            rightMouseButtonLock = false;
        }
    }
}