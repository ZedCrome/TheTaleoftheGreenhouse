using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopBehaviourBuy : MonoBehaviour
{
    [Header("Other")] [Space(5)]
    [SerializeField] private TMP_Text totalCostText;
    [SerializeField] private TMP_Text playerMoneyText;
    public int playerMoney = 99999;
    private int totalCost;
    
    [Header("Table")] [Space(5)]
    [SerializeField] private Button tableAdd;
    [SerializeField] private Button tableReduce;
    [SerializeField] private TMP_Text tableAmount;
    [SerializeField] private TMP_Text tablePriceDisplay;
    [SerializeField] private Slider tableCurrentyBuy1;
    [SerializeField] private Slider tableAlreadyBought1;
    [SerializeField] private Slider tableCurrentyBuy2;
    [SerializeField] private Slider tableAlreadyBought2;
    [SerializeField] private int maxTables = 8;
    [SerializeField] private int tableCost = 6;
    private int tableTotalCost;
    private int amountOfTables = 0;
    private int ownedTables = 0;

    [Header("Pot")] [Space(5)]
    [SerializeField] private Button potAdd;
    [SerializeField] private Button potReduce;
    [SerializeField] private TMP_Text potAmount;
    [SerializeField] private TMP_Text potPriceDisplay;
    [SerializeField] private Slider potCurrentyBuy1;
    [SerializeField] private Slider potAlreadyBought1;
    [SerializeField] private Slider potCurrentyBuy2;
    [SerializeField] private Slider potAlreadyBought2;
    [SerializeField] private int maxPots = 10;
    [SerializeField] private int potCost = 1;
    private int potTotalCost;
    private int amountOfPots = 0;
    private int ownedPots = 0;

    [Header("Plant")] [Space(5)] 
    [SerializeField] private Button plantAdd;
    [SerializeField] private Button plantReduce;
    [SerializeField] private TMP_Text plantAmount;
    [SerializeField] private TMP_Text plantPriceDisplay;
    [SerializeField] private Slider plantCurrentyBuy1;
    [SerializeField] private Slider plantAlreadyBought1;
    [SerializeField] private Slider plantCurrentyBuy2;
    [SerializeField] private Slider plantAlreadyBought2;
    [SerializeField] private int maxPlants = 10;
    [SerializeField] private int plantCost = 2;
    private int plantTotalCost;
    private int amountOfPlants = 0;
    private int ownedPlants = 0;
    
    [Header("ManaPlant")] [Space(5)] 
    [SerializeField] private Button manaPlantAdd;
    [SerializeField] private Button manaPlantReduce;
    [SerializeField] private TMP_Text manaPlantAmount;
    [SerializeField] private TMP_Text manaPlantPriceDisplay;
    [SerializeField] private Slider manaPlantCurrentyBuy1;
    [SerializeField] private Slider manaPlantAlreadyBought1;
    [SerializeField] private Slider manaPlantCurrentyBuy2;
    [SerializeField] private Slider manaPlantAlreadyBought2;
    [SerializeField] private int maxManaPlants = 10;
    [SerializeField] private int manaPlantCost = 5;
    private int manaPlantTotalCost;
    private int amountOfManaPlants = 0;
    private int ownedManaPlants = 0;

    [Header("ManaStorageItem")] [Space(5)]
    [SerializeField] private Button manaStorageItemAdd;
    [SerializeField] private Button manaStorageItemReduce;
    [SerializeField] private TMP_Text manaStorageItemAmount;
    [SerializeField] private TMP_Text manaStorageItemPriceDisplay;
    [SerializeField] private Slider manaStorageItemCurrentyBuy1;
    [SerializeField] private Slider manaStorageItemAlreadyBought1;
    [SerializeField] private Slider manaStorageItemCurrentyBuy2;
    [SerializeField] private Slider manaStorageItemAlreadyBought2;
    [SerializeField] private int maxManaStorageItems;
    [SerializeField] private int manaStorageItemCost = 4;
    private int manaStorageItemTotalCost;
    private int amountOfManaStorageItems = 0;
    private int ownedManaStorageItems = 0;


    public void addTable()
    {
        if (amountOfTables < maxTables)
        {
            amountOfTables++;
            
            if (amountOfTables <= 5)
                tableCurrentyBuy1.value += 1;
            
            if (amountOfTables > 5)
                tableCurrentyBuy2.value += 1;
            tableTotalCost += tableCost;
            totalCost += tableCost;
        }
        tablePriceDisplay.text = tableTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        tableAmount.text = amountOfTables.ToString();
    }
    public void reduceTable()
    {
        if (amountOfTables > 0)
        {
            amountOfTables--;
            
            if (amountOfTables < 5)
                tableCurrentyBuy1.value -= 1;
            
            if (amountOfTables >= 5)
                tableCurrentyBuy2.value -= 1;
            tableTotalCost -= tableCost;
            totalCost -= tableCost;
        }
        tablePriceDisplay.text = tableTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        tableAmount.text = amountOfTables.ToString();
    }


    public void addPot()
    {
        if (amountOfPots < maxPots)
        {
            amountOfPots++;
            
            if (amountOfPots <= 5)
                potCurrentyBuy1.value += 1;
            
            if (amountOfPots > 5)
                potCurrentyBuy2.value += 1;
            potTotalCost += potCost;
            totalCost += potCost;
        }

        potPriceDisplay.text = potTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        potAmount.text = amountOfPots.ToString();
    }
    public void reducePot()
    {
        if (amountOfPots > 0)
        {
            amountOfPots--;
            
            if (amountOfPots < 5)
                potCurrentyBuy1.value -= 1;
            
            if (amountOfPots >= 5)
                potCurrentyBuy2.value -= 1;
            potTotalCost -= potCost;
            totalCost -= potCost;
        }
        potPriceDisplay.text = potTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        potAmount.text = amountOfPots.ToString();
    }
    
    
    public void addPlant()
    {
        if (amountOfPlants < maxPlants)
        {
            amountOfPlants++;

            if (amountOfPlants <= 5)
                plantCurrentyBuy1.value += 1;

            if (amountOfPlants > 5)
                plantCurrentyBuy2.value += 1;
            plantTotalCost += plantCost;
            totalCost += plantCost;
        }
        plantPriceDisplay.text = plantTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        plantAmount.text = amountOfPlants.ToString();
    }
    public void reducePlant()
    {
        if (amountOfPlants > 0)
        {
            amountOfPlants--;
            if (amountOfPlants < 5)
                plantCurrentyBuy1.value -= 1;
            
            if (amountOfPlants >= 5)
                plantCurrentyBuy2.value -= 1;
            plantTotalCost -= plantCost;
            totalCost -= plantCost;
        }
        plantPriceDisplay.text = plantTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        plantAmount.text = amountOfPlants.ToString();
    }
    
    
    public void addManaPlant()
    {
        if (amountOfManaPlants < maxManaPlants)
        {
            amountOfManaPlants++;

            if (amountOfManaPlants <= 5)
                manaPlantCurrentyBuy1.value += 1;

            if (amountOfManaPlants > 5)
                manaPlantCurrentyBuy2.value += 1;
            manaPlantTotalCost += manaPlantCost;
            totalCost += manaPlantCost;
        }
        manaPlantPriceDisplay.text = manaPlantTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        manaPlantAmount.text = amountOfManaPlants.ToString();
    }
    public void reduceManaPlant()
    {
        if (amountOfManaPlants > 0)
        {
            amountOfManaPlants--;

            if (amountOfManaPlants < 5)
                manaPlantCurrentyBuy1.value -= 1;

            if (amountOfManaPlants >= 5)
                manaPlantCurrentyBuy2.value -= 1;
            manaPlantTotalCost -= manaPlantCost;
            totalCost -= manaPlantCost;
        }
        manaPlantPriceDisplay.text = manaPlantTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        manaPlantAmount.text = amountOfManaPlants.ToString();
    }

    
    public void addManaStorageItem()
    {
        if (amountOfManaStorageItems < maxManaStorageItems)
        {
            amountOfManaStorageItems++;
            
            if (amountOfManaStorageItems <= 5)
                manaStorageItemCurrentyBuy1.value += 1;

            if (amountOfManaStorageItems > 5)
                manaStorageItemCurrentyBuy2.value += 1;
            manaStorageItemTotalCost += manaStorageItemCost;
            totalCost += manaStorageItemCost;
        }
        manaStorageItemPriceDisplay.text = manaStorageItemTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        manaStorageItemAmount.text = amountOfManaStorageItems.ToString();
    }
    public void reduceManaStorageItem()
    {
        if (amountOfManaStorageItems > 0)
        {
            amountOfManaStorageItems--;

            if (amountOfManaStorageItems < 5)
                manaStorageItemCurrentyBuy1.value -= 1;

            if (amountOfManaStorageItems >= 5)
                manaStorageItemCurrentyBuy2.value -= 1;
            manaStorageItemTotalCost -= manaStorageItemCost;
            totalCost -= manaStorageItemCost;
        }

        manaStorageItemPriceDisplay.text = manaStorageItemTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        manaStorageItemAmount.text = amountOfManaStorageItems.ToString();
    }

    public void buy()
    {
        Debug.Log(playerMoney);
        playerMoney = playerMoney - totalCost;
        playerMoneyText.text = "Purse: " + playerMoney;
    }
}
