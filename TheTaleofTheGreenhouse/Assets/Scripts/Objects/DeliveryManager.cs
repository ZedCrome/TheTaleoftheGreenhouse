using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public GameObject potPrefab;
    public GameObject plantPrefab;
    
    public bool spawnPot;
    public bool spawnPlant;
    
    private List<ObjectSlot> objectSlot = new List<ObjectSlot>();
    
    void Start()
    {
        foreach (Transform child in transform)
        {
            objectSlot.Add(child.GetComponent<ObjectSlot>());
        }
    }
    
    void Update()
    {
        if (spawnPot)
        {
            foreach (ObjectSlot slot in objectSlot)
            {
                if (slot.objectInSlot == null)
                {
                    GameObject clone = Instantiate(potPrefab);
                    
                    slot.FillSlot(clone);

                    spawnPot = false;
                    break;
                }
            }

            spawnPot = false;
        }
        
        
    }
}
