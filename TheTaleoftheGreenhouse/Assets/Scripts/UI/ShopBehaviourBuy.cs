using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopBehaviourBuy : MonoBehaviour
{
    [Header("Other")] [Space(5)] 
    [SerializeField] private RectTransform buyMenu;
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
    [SerializeField] private int maxBuyTablesAtATime = 3;
    private int tableTotalCost;
    private int amountOfTables;
    private int ownedTables = 0;
    public int currentlyBuyingTables = 0;

    [Header("Pot")] [Space(5)]
    [SerializeField] private Button potAdd;
    [SerializeField] private Button potReduce;
    [SerializeField] private TMP_Text potAmount;
    [SerializeField] private TMP_Text potPriceDisplay;
    [SerializeField] private Slider potCurrentBuy1;
    [SerializeField] private Slider potAlreadyBought1;
    [SerializeField] private Slider potCurrentBuy2;
    [SerializeField] private Slider potAlreadyBought2;
    [SerializeField] private Slider potCurrentBuy3;
    [SerializeField] private Slider potAlreadyBought3;
    [SerializeField] private int maxPots = 10;
    [SerializeField] private int potCost = 1;
    [SerializeField] private int maxBuyPotsAtATime = 3;
    private int potTotalCost;
    private int amountOfPots = 0;
    private int ownedPots = 0;
    public int currentlyBuyingPots = 0;

    [Header("Plant")] [Space(5)] 
    [SerializeField] private Button plantAdd;
    [SerializeField] private Button plantReduce;
    [SerializeField] private TMP_Text plantAmount;
    [SerializeField] private TMP_Text plantPriceDisplay;
    public Slider plantCurrentBuy1;
    [SerializeField] private int maxPlants = 10;
    [SerializeField] private int plantCost = 2;
    [SerializeField] private int maxBuyPlantsAtATime = 3;
    private int plantTotalCost;
    private int amountOfPlants = 0;
    private int ownedPlants = 0;
    public int currentlyBuyingPlants;
    
    [Header("ManaPlant")] [Space(5)] 
    [SerializeField] private Button manaPlantAdd;
    [SerializeField] private Button manaPlantReduce;
    [SerializeField] private TMP_Text manaPlantAmount;
    [SerializeField] private TMP_Text manaPlantPriceDisplay;
    public Slider manaPlantCurrentBuy1;
    [SerializeField] private int maxManaPlants = 10;
    [SerializeField] private int manaPlantCost = 5;
    [SerializeField] private int maxBuyManaPlantsAtATime = 3;
    private int manaPlantTotalCost;
    private int amountOfManaPlants = 0;
    private int ownedManaPlants = 0;
    public int currentlyBuyingManaPlants;

    [Header("ManaStorageItem")] [Space(5)]
    [SerializeField] private Button manaStorageItemAdd;
    [SerializeField] private Button manaStorageItemReduce;
    [SerializeField] private TMP_Text manaStorageItemAmount;
    [SerializeField] private TMP_Text manaStorageItemPriceDisplay;
    [SerializeField] private Slider manaStorageItemCurrentBuy1;
    [SerializeField] private Slider manaStorageItemAlreadyBought1;
    [SerializeField] private Slider manaStorageItemCurrentBuy2;
    [SerializeField] private Slider manaStorageItemAlreadyBought2;
    [SerializeField] private int maxManaStorageItems = 9;
    [SerializeField] private int manaStorageItemCost = 4;
    [SerializeField] private int maxBuyManaStorageItemsAtATime = 3;
    private int manaStorageItemTotalCost;
    private int amountOfManaStorageItems = 0;
    private int ownedManaStorageItems = 0;
    public int currentlyBuyingManaStorageItems = 0;

    public GameObject delivery;

    public void addTable()
    {
        if (currentlyBuyingTables < maxBuyTablesAtATime && amountOfTables < maxTables)
        {
            amountOfTables++;
            currentlyBuyingTables++;
            
            if (amountOfTables <= 5)
                tableCurrentyBuy1.value++;
            
            if (amountOfTables > 5)
                tableCurrentyBuy2.value++;
            
            tableTotalCost += tableCost;
            totalCost += tableCost;
        }
        tablePriceDisplay.text = tableTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        tableAmount.text = amountOfTables.ToString();
    }
    public void reduceTable()
    {
        if (currentlyBuyingTables > 0)
        {
            amountOfTables--;
            currentlyBuyingTables--;

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
        if (currentlyBuyingPots < maxBuyPotsAtATime && amountOfPots < maxPots)
        {
            amountOfPots++;
            currentlyBuyingPots++;
            
            if (amountOfPots <= 5)
                potCurrentBuy1.value++;

            if (amountOfPots > 5 && amountOfPots <= 11)
                potCurrentBuy2.value++;
            
            if (amountOfPots > 11)
                potCurrentBuy3.value++;
            
            potTotalCost += potCost;
            totalCost += potCost;
        }
        potPriceDisplay.text = potTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        potAmount.text = amountOfPots.ToString();
    }
    public void reducePot()
    {
        if (currentlyBuyingPots > 0)
        {
            amountOfPots--;
            currentlyBuyingPots--;
            
            if (amountOfPots < 5)
                potCurrentBuy1.value -= 1;
            
            if (amountOfPots >= 5 && amountOfPots < 11)
                potCurrentBuy2.value -= 1;

            if (amountOfPots >= 11)
                potCurrentBuy3.value -= 1;
            
            potTotalCost -= potCost;
            totalCost -= potCost;
        }
        potPriceDisplay.text = potTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        potAmount.text = amountOfPots.ToString();
    }


    public void addPlant()
    {
        if (currentlyBuyingPlants < maxBuyPlantsAtATime && amountOfPlants < maxPlants) {

            amountOfPlants++;
            currentlyBuyingPlants++;
            plantCurrentBuy1.value++;

            plantTotalCost += plantCost;
            totalCost += plantCost;
        }

    plantPriceDisplay.text = plantTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        plantAmount.text = amountOfPlants.ToString();
    }
    public void reducePlant()
    {
        if (currentlyBuyingPlants > 0)
        {
            amountOfPlants--;
            currentlyBuyingPlants--;
            plantCurrentBuy1.value--;
            
            plantTotalCost -= plantCost;
            totalCost -= plantCost;
        }
        plantPriceDisplay.text = plantTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        plantAmount.text = amountOfPlants.ToString();
    }
    
    
    public void addManaPlant()
    {
        if (currentlyBuyingManaPlants < maxBuyManaStorageItemsAtATime && amountOfManaPlants < maxManaPlants)
        {
            amountOfManaPlants++;
            currentlyBuyingManaPlants++;
            manaPlantCurrentBuy1.value++;

            manaPlantTotalCost += manaPlantCost;
            totalCost += manaPlantCost;
        }
        manaPlantPriceDisplay.text = manaPlantTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        manaPlantAmount.text = amountOfManaPlants.ToString();
    }
    public void reduceManaPlant()
    {
        if (currentlyBuyingManaPlants > 0)
        {
            amountOfManaPlants--;
            currentlyBuyingManaPlants--;
            manaPlantCurrentBuy1.value--;
            
            manaPlantTotalCost -= manaPlantCost;
            totalCost -= manaPlantCost;
        }
        manaPlantPriceDisplay.text = manaPlantTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        manaPlantAmount.text = amountOfManaPlants.ToString();
    }

    
    public void addManaStorageItem()
    {
        if (currentlyBuyingManaStorageItems < maxBuyManaStorageItemsAtATime && amountOfManaStorageItems < maxManaStorageItems)
        {
            amountOfManaStorageItems++;
            currentlyBuyingManaStorageItems++;
            
            if (amountOfManaStorageItems <= 4)
                manaStorageItemCurrentBuy1.value++;
            
            if (amountOfManaStorageItems > 4)
                manaStorageItemCurrentBuy2.value++;
            
            manaStorageItemTotalCost += manaStorageItemCost;
            totalCost += manaStorageItemCost;
        }
        manaStorageItemPriceDisplay.text = manaStorageItemTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        manaStorageItemAmount.text = amountOfManaStorageItems.ToString();
    }
    public void reduceManaStorageItem()
    {
        if (currentlyBuyingManaStorageItems > 0)
        {
            amountOfManaStorageItems--;
            currentlyBuyingManaStorageItems--;
            
            if (amountOfManaStorageItems < 4)
                manaStorageItemCurrentBuy1.value -= 1;
            
            if (amountOfManaStorageItems >= 4)
                manaStorageItemCurrentBuy2.value -= 1;

            manaStorageItemTotalCost -= manaStorageItemCost;
            totalCost -= manaStorageItemCost;
        }
        manaStorageItemPriceDisplay.text = manaStorageItemTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        manaStorageItemAmount.text = amountOfManaStorageItems.ToString();
    }

    public void buy()
    {
        delivery = GameObject.FindGameObjectWithTag("Delivery");
        delivery.GetComponent<DeliveryManager>().spawnPot = currentlyBuyingPots;
        delivery.GetComponent<DeliveryManager>().spawnPlantMana = currentlyBuyingManaPlants;
        delivery.GetComponent<DeliveryManager>().spawnPlantNormal = currentlyBuyingPlants;
        delivery.GetComponent<DeliveryManager>().spawnManaCube = currentlyBuyingManaStorageItems;

        tableAlreadyBought1.value = tableCurrentyBuy1.value; 
        tableAlreadyBought2.value = tableCurrentyBuy2.value;

        potAlreadyBought1.value = potCurrentBuy1.value;
        potAlreadyBought2.value = potCurrentBuy2.value;
        potAlreadyBought3.value = potCurrentBuy3.value;

        manaStorageItemAlreadyBought1.value = manaStorageItemCurrentBuy1.value;
        manaStorageItemAlreadyBought2.value = manaStorageItemCurrentBuy2.value;
        
        playerMoney = playerMoney - totalCost;
        totalCost = 0;
        totalCostText.text = "Price: " + totalCost + " Gold";
        playerMoneyText.text = "Purse: " + playerMoney + " Gold";
    }

    
}
