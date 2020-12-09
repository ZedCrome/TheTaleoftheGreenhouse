using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SellItems : MonoBehaviour
{
    SellBoxBehaviour sellBox;

    private int manaPlantPrice = 6;
    private int normalPlantPrice = 3;
    private int manaCubePrice = 4;
    private int potPrice = 1;

    enum PlantState { Sprout, Young, Adult, FullGrown, Dead };
    PlantState statePrice;

    private int manaSproutPrice = 
    private int normalSpritPrice;
    private int manaYoungPrice;
    private int normalYoungPrice;
    private int manaAdultPrice;
    private int normalAdultPrice;
    private int manaFullGrwonPrice;
    private int normalFullGrownPrice;
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

            PlantStates.PlantState checkPlantState = gameObjects[i].GetComponent<PlantStates>().currentState;
            switch (gameObjects[i].tag)
                {
                    case "PlantMana":
                   
                        goldBack += manaPlantPrice;
                        break;

                    case "PlantNormal":
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
