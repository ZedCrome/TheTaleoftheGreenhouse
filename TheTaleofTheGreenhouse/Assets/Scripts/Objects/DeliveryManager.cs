using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager instance;
    
    public GameObject shopItems;
    private float preDistance;
    
    public int spawnPot;
    public int spawnPlantMana;
    public int spawnPlantNormal;
    public int spawnManaCube;

    private List<ObjectSlot> objectSlot = new List<ObjectSlot>();

    private void Awake()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this.gameObject );
        }
    }

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.HasComponent<ObjectSlot>())
            {
                objectSlot.Add(child.GetComponent<ObjectSlot>());
            }
        }

        shopItems = GameObject.FindGameObjectWithTag("Shop");
        preDistance = PlayerInteract.instance.maxInteractDistance;
    }

    public void Delivery()
    { 
        if (spawnPot > 0)
        {
            foreach (ObjectSlot slot in objectSlot)
            {
                if (slot.objectInSlot == null)
                {
                    GameObject clone = PrefabManager.instance.CreateNewObjectInstance("Pot");
                    
                    bool result = slot.FillSlot(clone);

                    if (result == false)
                    {
                        Debug.LogError("Object seems to be blocked in ObjectSlot");
                    }
                    
                    break;
                }
            }
            
            spawnPot -= 1;
        }
        
        if (spawnPlantMana > 0)
        {
            foreach (ObjectSlot slot in objectSlot)
            {
                if (slot.objectInSlot == null)
                {
                    GameObject clone = PrefabManager.instance.CreateNewObjectInstance("PlantMana");

                    bool result = slot.FillSlot(clone);
                    clone.transform.position = clone.transform.position + new Vector3(0, 0.4f, -0.4f);

                    if (result == false)
                    {
                        Debug.LogError("Object seems to be blocked in ObjectSlot");
                    }
                    
                    break;
                }
            }
            
            spawnPlantMana -= 1;
        }


        if (spawnPlantNormal > 0)
        {
            foreach (ObjectSlot slot in objectSlot)
            {
                if (slot.objectInSlot == null)
                {
                    GameObject clone = PrefabManager.instance.CreateNewObjectInstance("PlantNormal");

                    bool result = slot.FillSlot(clone);

                    if (result == false)
                    {
                        Debug.LogError("Object seems to be blocked in ObjectSlot");
                    }
                    
                    
                    break;
                }
            }
            
            spawnPlantNormal -= 1;
        }


        if (spawnManaCube > 0)
        {
            foreach (ObjectSlot slot in objectSlot)
            {
                if (slot.objectInSlot == null)
                {
                    GameObject clone = PrefabManager.instance.CreateNewObjectInstance("ManaStorage");

                    bool result = slot.FillSlot(clone);

                    if (result == false)
                    {
                        Debug.LogError("Object seems to be blocked in ObjectSlot");
                    }

                    break;
                }
            }

            spawnManaCube -= 1;
        }
    }

    public int GetFreeSlots()
    {
        int returnValue = 0;
        
        foreach (ObjectSlot slot in objectSlot)
        {
            if (slot.objectInSlot == null)
            {
                returnValue++;
            }
        }

        return returnValue;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (PlayerState.instance.currentInteractState == PlayerState.InteractState.placement)
            {
                PlayerInteract.instance.allowedTointeract = false;             
                PlayerInteract.instance.maxInteractDistance = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (PlayerState.instance.currentInteractState == PlayerState.InteractState.placement)
            {
                PlayerInteract.instance.allowedTointeract = true;
                PlayerInteract.instance.maxInteractDistance = preDistance;
            }
        }
    }
}
