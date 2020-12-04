﻿using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public bool spawnPot;
    public bool spawnPlantMana;
    public bool spawnPlantNormal;
    public bool spawnManaCube;
    //public bool spawnPlantTable;

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
                    GameObject clone = PrefabManager.instance.CreateNewObjectInstance("Pot");

                    slot.FillSlot(clone);

                    spawnPot = false;
                    break;
                }
            }

            spawnPot = false;
        }


        if (spawnPlantMana)
        {
            foreach (ObjectSlot slot in objectSlot)
            {
                if (slot.objectInSlot == null)
                {
                    GameObject clone = PrefabManager.instance.CreateNewObjectInstance("PlantMana");

                    clone.transform.localScale = new Vector3(1, 1, 1);
                    slot.FillSlot(clone);

                    spawnPlantMana = false;
                    break;
                }
            }

            spawnPlantNormal = false;
        }


        if (spawnPlantNormal)
        {
            foreach (ObjectSlot slot in objectSlot)
            {
                if (slot.objectInSlot == null)
                {
                    GameObject clone = PrefabManager.instance.CreateNewObjectInstance("PlantNormal");

                    clone.transform.localScale = new Vector3(1, 1, 1);
                    slot.FillSlot(clone);

                    spawnPlantNormal = false;
                    break;
                }
            }

            spawnPlantNormal = false;
        }


        if (spawnManaCube)
        {
            foreach (ObjectSlot slot in objectSlot)
            {
                if (slot.objectInSlot == null)
                {
                    GameObject clone = PrefabManager.instance.CreateNewObjectInstance("ManaStorage");

                    slot.FillSlot(clone);

                    spawnPlantNormal = false;
                    break;
                }
            }

            spawnManaCube = false;
        }
    }
}
