using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SellItems : MonoBehaviour
{
    SellBoxBehaviour sellBox;
    enum PriceTag { ManaPlant, NormalPlant, ManaCube, Cuttings};
    private PriceTag price;

    private int manaPlantPrice = 6;
    private int normalPlantPrice = 3;
    private int manaCubePrice = 4;
    private int potPrice = 1;
    
    private int goldBack;

    private void Start()
    {
        sellBox = GetComponent<SellBoxBehaviour>();
    }

    public int GetGold()
    {
       return CalculateGold(sellBox.itemsToSell);
    }

    private int CalculateGold(GameObject[] gameObjects)
    {
        for (int i = 0; 0 < sellBox.maxNumbertoSell; i++)
        {    
            if(gameObjects[i] == null)
            {
                break;
            }
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
