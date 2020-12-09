using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SellItems : MonoBehaviour
{
    SellBoxBehaviour sellBox;

    //Items price
    private int manaCubePrice = 4;
    private int potPrice = 1;
    
    //Plants price
    PlantStates.PlantState statePrice;

    private int manaPlantPrice;
    private int normalPlantPrice;

    private int manaSproutPrice = 5;
    private int normalSproutPrice = 2;
    private int manaYoungPrice = 7;
    private int normalYoungPrice = 3;
    private int manaAdultPrice = 9;
    private int normalAdultPrice = 4;
    private int manaFullGrownPrice = 11;
    private int normalFullGrownPrice = 5;
    private int deadPlantPrice = 0;

    private int goldBack;

    private void Start()
    {
        sellBox = GetComponent<SellBoxBehaviour>();
    }

    public int GetGold()
    {
       return CalculateGold(sellBox.itemsToSell);
    }

    public void SetGoldBack()
    {
        goldBack = 0;
    }

    private int CalculateGold(GameObject[] gameObjects)
    {
        for (int i = 0; 0 < sellBox.maxNumbertoSell; i++)
        {         
            if (gameObjects[i] == null)
            {
                break;
            }
            if (gameObjects[i].GetComponent<PlantStates>() != null)
            {
                statePrice = gameObjects[i].GetComponent<PlantStates>().currentState;
            }       

            switch (gameObjects[i].tag)
                {
                    case "PlantMana":
                        switch(statePrice)
                    {
                        case PlantStates.PlantState.Sprout:
                            manaPlantPrice = manaSproutPrice;
                            break;
                        case PlantStates.PlantState.Young:
                            manaPlantPrice = manaYoungPrice;
                            break;
                        case PlantStates.PlantState.Adult:
                            manaPlantPrice = manaAdultPrice;
                            break;
                        case PlantStates.PlantState.FullGrown:
                            manaPlantPrice = manaFullGrownPrice;
                            break;
                        case PlantStates.PlantState.Dead:
                            manaPlantPrice = deadPlantPrice;
                            break;
                    }
                        goldBack += manaPlantPrice;
                        break;

                    case "PlantNormal":
                    switch (statePrice)
                    {
                        case PlantStates.PlantState.Sprout:
                            normalPlantPrice = normalSproutPrice;
                            break;
                        case PlantStates.PlantState.Young:
                            normalPlantPrice = normalYoungPrice;
                            break;
                        case PlantStates.PlantState.Adult:
                            normalPlantPrice = normalAdultPrice;
                            break;
                        case PlantStates.PlantState.FullGrown:
                            normalPlantPrice = normalFullGrownPrice;
                            break;
                        case PlantStates.PlantState.Dead:
                            normalPlantPrice = deadPlantPrice;
                            break;
                    }
                    goldBack += normalPlantPrice;
                        break;

                    case "ManaStorage":
                        goldBack += manaCubePrice;
                        break;

                    case "Pot":
                        goldBack += potPrice;
                        break;
                }         
        }
        return goldBack;
    }


}
