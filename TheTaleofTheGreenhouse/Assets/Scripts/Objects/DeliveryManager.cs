using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public GameObject shopItems;
    
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
    }
    
    
    void Update()
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
            
            spawnPlantNormal -= 1;
        }
    }
}
