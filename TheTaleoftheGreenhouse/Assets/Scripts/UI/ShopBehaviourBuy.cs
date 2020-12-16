using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopBehaviourBuy : MonoBehaviour
{
    [Header("Other")] [Space(5)] 
    [SerializeField] private RectTransform buyMenu;
    [SerializeField] private GameObject alreadyBoughtOverlay;
    public TMP_Text totalCostText;
    [SerializeField] private TMP_Text playerMoneyText;
    public int playerMoney = 99999;
    public bool hasBoughtSomething = false;
    public int totalCost;
    
    [Header("Table")] [Space(5)]
    [SerializeField] private Button tableAdd;
    [SerializeField] private Button tableReduce;
    public TMP_Text tableAmount;
    public TMP_Text tablePriceDisplay;
    public Slider tableCurrentyBuy2;
    public Slider tableAlreadyBought2;
    [SerializeField] private int maxTables = 4;
    public int tableCost = 60;
    [SerializeField] private int maxBuyTablesAtATime = 3;
    public int tableTotalCost;
    [SerializeField] int amountOfTables;
    [SerializeField] int ownedTables;
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
    [SerializeField] private int potCost = 10;
    [SerializeField] private int maxBuyPotsAtATime = 3;
    private int potTotalCost;
    [SerializeField] int amountOfPots = 0;
    private int ownedPots = 0;
    public int currentlyBuyingPots = 0;

    [Header("Plant")] [Space(5)] 
    [SerializeField] private Button plantAdd;
    [SerializeField] private Button plantReduce;
    [SerializeField] private TMP_Text plantAmount;
    [SerializeField] private TMP_Text plantPriceDisplay;
    [SerializeField] private int maxPlants = 10;
    [SerializeField] private int plantCost = 20;
    [SerializeField] private int maxBuyPlantsAtATime = 3;
    public Slider plantCurrentBuy1;
    private int plantTotalCost;
    private int amountOfPlants = 0;
    private int ownedPlants = 4;
    public int currentlyBuyingPlants = 0;
    
    [Header("ManaPlant")] [Space(5)] 
    [SerializeField] private Button manaPlantAdd;
    [SerializeField] private Button manaPlantReduce;
    [SerializeField] private TMP_Text manaPlantAmount;
    [SerializeField] private TMP_Text manaPlantPriceDisplay;
    [SerializeField] private int maxManaPlants = 10;
    [SerializeField] private int manaPlantCost = 50;
    [SerializeField] private int maxBuyManaPlantsAtATime = 3;
    public Slider manaPlantCurrentBuy1;
    private int manaPlantTotalCost;
    private int amountOfManaPlants = 0;
    private int ownedManaPlants = 0;
    public int currentlyBuyingManaPlants = 0;

    [Header("ManaStorageItem")] [Space(5)]
    [SerializeField] private Button manaStorageItemAdd;
    [SerializeField] private Button manaStorageItemReduce;
    public TMP_Text manaStorageItemAmount;
    public TMP_Text manaStorageItemPriceDisplay;
    [SerializeField] private Slider manaStorageItemCurrentBuy1;
    [SerializeField] private Slider manaStorageItemAlreadyBought1;
    [SerializeField] private Slider manaStorageItemCurrentBuy2;
    [SerializeField] private Slider manaStorageItemAlreadyBought2;
    [SerializeField] private int maxManaStorageItems = 9;
    [SerializeField] private int manaStorageItemCost = 40;
    [SerializeField] private int maxBuyManaStorageItemsAtATime = 3;
    private int manaStorageItemTotalCost;
    private int amountOfManaStorageItems = 0;
    private int ownedManaStorageItems = 0;
    public int currentlyBuyingManaStorageItems = 0;
    
    [Header("Delivery")] [Space(5)]
    public bool hasOrderedItems;


    public void Start()
    {
        ownedTables = GameObject.FindGameObjectsWithTag("Table").Length;
    }

    
    private void OnEnable()
    {
        Motherlode.instance.addGoldCheat += AddGold;
    }
    
    
    private void OnDisable()
    {
        Motherlode.instance.addGoldCheat -= AddGold;
    }
    
    
    public void Update()
    {
        playerMoneyText.text = playerMoney.ToString(); 
        DeactivateButtons();
    }

    
    public void AddGold(int amount)
    {
        playerMoney += amount;
    }

    
    public void addTable()
    {
        if (playerMoney - (totalCost + tableCost) >= 0)
        {
            if (currentlyBuyingTables < maxBuyTablesAtATime && amountOfTables < maxTables)
            {
                amountOfTables++;
                currentlyBuyingTables++;

                if ((ownedTables - 3) <= 4)
                    tableCurrentyBuy2.value++;

                tableTotalCost += tableCost;
                totalCost += tableCost;
            }

            tablePriceDisplay.text = tableTotalCost + " Gold";
            totalCostText.text = "Price: " + totalCost + " Gold";
            tableAmount.text = currentlyBuyingTables.ToString();
        }
    }
    public void reduceTable()
    {
        if (currentlyBuyingTables > 0)
        {
            amountOfTables--;
            currentlyBuyingTables--;

            if (amountOfTables >= 0)
                tableCurrentyBuy2.value --;
            
            tableTotalCost -= tableCost;
            totalCost -= tableCost;
        }
        tablePriceDisplay.text = tableTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        tableAmount.text = currentlyBuyingTables.ToString();
    }


    public void addPot()
    {
        if (playerMoney - (totalCost + potCost) >= 0)
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
            potAmount.text = currentlyBuyingPots.ToString();
        }
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
        potAmount.text = currentlyBuyingPots.ToString();
    }


    public void addPlant()
    {
        if (playerMoney - (totalCost + plantCost) >= 0)
        {
            if (currentlyBuyingPlants < maxBuyPlantsAtATime && amountOfPlants < maxPlants)
            {
                amountOfPlants++;
                currentlyBuyingPlants++;
                plantCurrentBuy1.value++;

                plantTotalCost += plantCost;
                totalCost += plantCost;
            }

            plantPriceDisplay.text = plantTotalCost + " Gold";
            totalCostText.text = "Price: " + totalCost + " Gold";
            plantAmount.text = currentlyBuyingPlants.ToString();
        }
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
        plantAmount.text = currentlyBuyingPlants.ToString();
    }

    
    
    public void addManaPlant()
    {
        if (playerMoney - (totalCost + manaPlantCost) >= 0)
        {
            if (currentlyBuyingManaPlants < maxBuyManaPlantsAtATime && amountOfManaPlants < maxManaPlants)
            {
                amountOfManaPlants++;
                currentlyBuyingManaPlants++;
                manaPlantCurrentBuy1.value++;

                manaPlantTotalCost += manaPlantCost;
                totalCost += manaPlantCost;
            }
            manaPlantPriceDisplay.text = manaPlantTotalCost + " Gold";
            totalCostText.text = "Price: " + totalCost + " Gold";
            manaPlantAmount.text = currentlyBuyingManaPlants.ToString(); 
        }
        
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
        manaPlantAmount.text = currentlyBuyingManaPlants.ToString();
    }

    
    public void addManaStorageItem()
    {
        if (playerMoney - (totalCost + manaStorageItemCost) >= 0)
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
            manaStorageItemAmount.text = currentlyBuyingManaStorageItems.ToString();
        }
    }
    public void reduceManaStorageItem()
    {
        if (currentlyBuyingManaStorageItems > 0)
        {
            amountOfManaStorageItems--;
            currentlyBuyingManaStorageItems--;
        
            if (currentlyBuyingManaStorageItems < 4)
                manaStorageItemCurrentBuy1.value --;
        
            if (currentlyBuyingManaStorageItems >= 4)
                manaStorageItemCurrentBuy2.value --;

            manaStorageItemTotalCost -= manaStorageItemCost;
            totalCost -= manaStorageItemCost;
        }
        manaStorageItemPriceDisplay.text = manaStorageItemTotalCost + " Gold";
        totalCostText.text = "Price: " + totalCost + " Gold";
        manaStorageItemAmount.text = currentlyBuyingManaStorageItems.ToString();
    }


    public void DeactivateButtons()
    {
        
    }

    public void buy()
    {
        if (hasOrderedItems == false)
        {
            DeliveryManager.instance.spawnPot = currentlyBuyingPots;
            DeliveryManager.instance.spawnPlantMana = currentlyBuyingManaPlants;
            DeliveryManager.instance.spawnPlantNormal = currentlyBuyingPlants;
            DeliveryManager.instance.spawnManaCube = currentlyBuyingManaStorageItems;
        }

        if (currentlyBuyingTables > 0 || currentlyBuyingPots > 0 || currentlyBuyingPlants > 0 
            || currentlyBuyingManaPlants > 0 || currentlyBuyingManaStorageItems > 0)
        {
            if (amountOfTables > 0)
            {
                ownedTables += amountOfTables;
                TableManager.instance.unlockedTables = ownedTables;
            }
            alreadyBoughtOverlay.SetActive(true);
            LeanTween.scale(alreadyBoughtOverlay, new Vector3(1f, 1f, 1f), 0.1f).setEaseLinear();
            hasBoughtSomething = true;
        }
        
        currentlyBuyingTables = 0;
        tableTotalCost = 0;
        tablePriceDisplay.text = tableTotalCost + " Gold";
        tableAmount.text = currentlyBuyingTables.ToString();
        tableAlreadyBought2.value = tableCurrentyBuy2.value;
        
        currentlyBuyingPots = 0;
        potTotalCost = 0;
        potPriceDisplay.text = potTotalCost + " Gold";
        potAmount.text = currentlyBuyingPots.ToString();
        potAlreadyBought1.value = potCurrentBuy1.value;
        potAlreadyBought2.value = potCurrentBuy2.value;
        potAlreadyBought3.value = potCurrentBuy3.value;
        
        currentlyBuyingPlants = 0;
        plantTotalCost = 0;
        plantPriceDisplay.text = plantTotalCost + " Gold";
        plantAmount.text = amountOfPlants.ToString();
        
        currentlyBuyingManaPlants = 0;
        manaPlantTotalCost = 0;
        manaPlantPriceDisplay.text = manaPlantTotalCost + " Gold";
        manaPlantAmount.text = amountOfManaPlants.ToString();
        
        currentlyBuyingManaStorageItems = 0;
        manaStorageItemTotalCost = 0;
        manaStorageItemPriceDisplay.text = manaStorageItemTotalCost + " Gold";
        manaStorageItemAmount.text = currentlyBuyingManaStorageItems.ToString();
        manaStorageItemAlreadyBought1.value = manaStorageItemCurrentBuy1.value;
        manaStorageItemAlreadyBought2.value = manaStorageItemCurrentBuy2.value;
        
        hasOrderedItems = true;
        
        playerMoney = playerMoney - totalCost;
        totalCost = 0;
        totalCostText.text = "Price: " + totalCost + " Gold";
        playerMoneyText.text = playerMoney + " Gold";
    }  
    
    
    public void ClearShopOnExit()
    {
        tableTotalCost = 0;
        amountOfTables -= currentlyBuyingTables;
        currentlyBuyingTables = 0;
        tablePriceDisplay.text = "0 Gold";
        tableAmount.text = "0";
        tableCurrentyBuy2.value = tableAlreadyBought2.value;

        potTotalCost = 0;
        amountOfPots -= currentlyBuyingPots;
        currentlyBuyingPots = 0;
        potPriceDisplay.text = "0 Gold";
        potAmount.text = "0";
        potCurrentBuy1.value = potAlreadyBought1.value;
        potCurrentBuy2.value = potAlreadyBought2.value;
        potCurrentBuy3.value = potAlreadyBought3.value;
        
        plantTotalCost = 0;
        currentlyBuyingPlants = 0;
        plantPriceDisplay.text = "0 Gold";
        plantAmount.text = "0";
        plantCurrentBuy1.value = 0;

        manaPlantTotalCost = 0;
        currentlyBuyingManaPlants = 0;
        manaPlantPriceDisplay.text = "0 Gold";
        manaPlantAmount.text = "0";
        manaPlantCurrentBuy1.value = 0;
        
        manaStorageItemTotalCost = 0;
        amountOfManaStorageItems -= currentlyBuyingManaStorageItems;
        currentlyBuyingManaStorageItems = 0;
        manaStorageItemPriceDisplay.text = "0 Gold";
        manaStorageItemAmount.text = "0";
        manaStorageItemCurrentBuy1.value = manaStorageItemAlreadyBought1.value;
        manaStorageItemCurrentBuy2.value = manaStorageItemAlreadyBought2.value;
        
        totalCost = 0;
        totalCostText.text = "Price: 0 Gold";
    }
}
