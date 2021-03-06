﻿using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    public GameObject cuttingPrefab;
    
    public int normalCuttingsInInventory;
    private int manaCuttingsInInventory;
    public int MaxCuttings = 1;
    
    void Start()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
    }
    
    private void OnEnable()
    {
        PlayerState.instance.onChangeHandState += OnChangeHandState;
    }
    
    private void OnDisable()
    {
        PlayerState.instance.onChangeHandState -= OnChangeHandState;
    }

    void OnChangeHandState(PlayerState.HandState newHandState)
    {
        if (newHandState == PlayerState.HandState.Cutting)
        {
            SpawnCutting();
        }
    }

    public void SpawnCutting()
    {
        if (normalCuttingsInInventory > 0)
        {
            GameObject newNormalCutting = PrefabManager.instance.CreateNewObjectInstance("PlantNormal");
            newNormalCutting.GetComponent<PlantStates>().currentState = PlantStates.PlantState.Cutting;
            normalCuttingsInInventory -= 1;
            PlayerInteract.instance.inventoryItem = newNormalCutting;
        }
        else if (manaCuttingsInInventory > 0)
        {
            GameObject newManaCutting = PrefabManager.instance.CreateNewObjectInstance("CuttingMana");
            manaCuttingsInInventory -= 1;
            PlayerInteract.instance.inventoryItem = newManaCutting;  
        }
    }
    
    public void AddCutting(string plantType)
    {
        if (normalCuttingsInInventory + manaCuttingsInInventory < MaxCuttings)
        {
            if (plantType == "PlantNormal")
            {
                normalCuttingsInInventory += 1;
            }
            else
            {
                manaCuttingsInInventory += 1;
            }
        }
    }

    public bool CanCarryMoreCuttings()
    {
        if (normalCuttingsInInventory + manaCuttingsInInventory == MaxCuttings)
        {
            GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.CuttingsWarning2);
            return false;
        }

        return true;
    }

    public bool GetCuttingsExist()
    {
        if (normalCuttingsInInventory + manaCuttingsInInventory > 0)
        {
            return true;
        }

        return false;
    }
}
