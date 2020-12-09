using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public GameObject shopItems;
    private float preDistance;
    
    public int spawnPot;
    public int spawnPlantMana;
    public int spawnPlantNormal;
    public int spawnManaCube;
    //public bool spawnPlantTable;

    private List<ObjectSlot> objectSlot = new List<ObjectSlot>();
    
    void Start()
    {
        foreach (Transform child in transform)
        {
            objectSlot.Add(child.GetComponent<ObjectSlot>());
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

                    clone.transform.localScale = new Vector3(1, 1, 1);
                    bool result = slot.FillSlot(clone);

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

                    clone.transform.localScale = new Vector3(1, 1, 1);
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
