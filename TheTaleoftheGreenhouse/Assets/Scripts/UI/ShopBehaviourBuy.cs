using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class ShopBehaviourBuy : MonoBehaviour
{
    [Header("Other")] [Space(5)] 
    [SerializeField] private RectTransform buyMenu;
    [SerializeField] private GameObject alreadyBoughtOverlay;
    [SerializeField] private AudioSource buySound;
    public TMP_Text totalCostText;
    [SerializeField] private TMP_Text playerMoneyText;
    public int playerMoney = 20;
    public bool hasBoughtSomething = false;
    public int totalCost;
    private int currentlySpendingGold;
    
    [Header("Table")] [Space(5)]
    [SerializeField] private Button tableAdd;
    [SerializeField] private Button tableReduce;
    [SerializeField] TMP_Text tableAmount;
    [SerializeField] TMP_Text tablePriceDisplay;
    [SerializeField] Slider tableCurrentyBuy2;
    [SerializeField] Slider tableAlreadyBought2;
    [SerializeField] GameObject tableOutOfStock;
    [SerializeField] GameObject tableCantAfford;
    [SerializeField] private int maxTables = 7;
    [SerializeField] int tableCost = 60;
    [SerializeField] private int maxBuyTablesAtATime = 3; 
    private int tableTotalCost;
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
    [SerializeField] GameObject potOutOfStock;
    [SerializeField] GameObject potCantAfford;
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
    [SerializeField] GameObject plantOutOfStock;
    [SerializeField] GameObject plantCantAfford;
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
    [SerializeField] GameObject manaPlantOutOfStock;
    [SerializeField] GameObject manaPlantCantAfford;
    [SerializeField] private int maxManaPlants = 10;
    public int manaPlantCost = 50;
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
    [SerializeField] GameObject manaStorageItemOutOfStock;
    [SerializeField] GameObject manaStorageItemCantAfford;
    [SerializeField] private int maxManaStorageItems = 9;
    [SerializeField] private int manaStorageItemCost = 40;
    [SerializeField] private int maxBuyManaStorageItemsAtATime = 3;
    private int manaStorageItemTotalCost;
    public int amountOfManaStorageItems = 0;
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
    
    
    public void FixedUpdate()
    {
        playerMoneyText.text = playerMoney.ToString();
        TogglePurchaseOfItems();
    }

    
    public void AddGold(int amount)
    {
        playerMoney += amount;
    }

    
    public void addTable()
    {
        if (playerMoney - (totalCost + tableCost) >= 0)
        {
            if (currentlyBuyingTables < maxBuyTablesAtATime && (ownedTables + currentlyBuyingTables) < maxTables)
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
        if (TotalAmountToBuy() >= DeliveryManager.instance.GetFreeSlots())
        {
            GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.DeliveryFull);
            return;
        }
        
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
        if (TotalAmountToBuy() >= DeliveryManager.instance.GetFreeSlots())
        {
            GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.DeliveryFull);
            return;
        }
        
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
        if (TotalAmountToBuy() >= DeliveryManager.instance.GetFreeSlots())
        {
            GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.DeliveryFull);
            return;
        }
        
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
        if (TotalAmountToBuy() >= DeliveryManager.instance.GetFreeSlots())
        {
            GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.DeliveryFull);
            return;
        }
        
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

    private int TotalAmountToBuy()
    {
        int returnValue = 0;

        returnValue = currentlyBuyingPlants
                      + currentlyBuyingPots
                      + currentlyBuyingManaPlants
                      + currentlyBuyingManaStorageItems;
        
        return returnValue;
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
            buySound.Play();
            if (amountOfTables > 0)
            {
                ownedTables += amountOfTables;
                TableManager.instance.unlockedTables = ownedTables;
                amountOfTables = 0;
            }
            alreadyBoughtOverlay.SetActive(true);
            LeanTween.scale(alreadyBoughtOverlay, new Vector3(1f, 1f, 1f), 0.1f).setEaseLinear();
            hasBoughtSomething = true;
        }
        
        currentlyBuyingTables = 0;
        tableTotalCost = 0;
        tablePriceDisplay.text = tableTotalCost + " Gold";
        tableAmount.text = "0";
        tableAlreadyBought2.value = tableCurrentyBuy2.value;
        if (maxTables <= ownedTables)
        {
            tableOutOfStock.SetActive(true);
            tableAdd.enabled = false;
            tableReduce.enabled = false;
        }
        
        currentlyBuyingPots = 0;
        potTotalCost = 0;
        potPriceDisplay.text = potTotalCost + " Gold";
        potAmount.text = "0";
        // potAlreadyBought1.value = potCurrentBuy1.value;
        // potAlreadyBought2.value = potCurrentBuy2.value;
        // potAlreadyBought3.value = potCurrentBuy3.value;
        potCurrentBuy1.value = 0;
        potCurrentBuy2.value = 0;
        potCurrentBuy3.value = 0;
        
        currentlyBuyingPlants = 0;
        plantTotalCost = 0;
        plantPriceDisplay.text = plantTotalCost + " Gold";
        plantAmount.text = "0";
        
        currentlyBuyingManaPlants = 0;
        manaPlantTotalCost = 0;
        manaPlantPriceDisplay.text = manaPlantTotalCost + " Gold";
        manaPlantAmount.text = "0";
        
        currentlyBuyingManaStorageItems = 0;
        manaStorageItemTotalCost = 0;
        manaStorageItemPriceDisplay.text = manaStorageItemTotalCost + " Gold";
        manaStorageItemAmount.text = "0";
        // manaStorageItemAlreadyBought1.value = manaStorageItemCurrentBuy1.value;
        // manaStorageItemAlreadyBought2.value = manaStorageItemCurrentBuy2.value;
        manaStorageItemCurrentBuy1.value = 0;
        manaStorageItemCurrentBuy2.value = 0;
        
        
        hasOrderedItems = true;
        
        playerMoney = playerMoney - totalCost;
        totalCost = 0;
        totalCostText.text = "Price: " + totalCost + " Gold";
        playerMoneyText.text = playerMoney.ToString();
    }

    public void TogglePurchaseOfItems()
    {
        TableInfo();
        PotInfo();
        PlantInfo();
        ManaPlantInfo();
        ManaStorageItemInfo();
    }


    public void TableInfo()
    {
        bool full = false;
        if (maxTables <= ownedTables + currentlyBuyingTables)
        {
            tableOutOfStock.SetActive(true);
            full = true;
        }
        else if (maxTables > ownedTables + currentlyBuyingTables)
        {
            tableOutOfStock.SetActive(false);
            full = false;
        }

        if (!full)
        {
            if (playerMoney < totalCost + tableCost)
            {
                tableCantAfford.SetActive(true);
            }
            else if (playerMoney >= tableCost)
            {
                tableCantAfford.SetActive(false);
            }
        }
    }

    
    public void PotInfo()
    {
        if (maxPots <= amountOfPots)
        {
            potOutOfStock.SetActive(true);
        }
        else if (playerMoney < totalCost + potCost)
        {
            potCantAfford.SetActive(true);
        }
        else if (playerMoney >= potCost)
        {
            potCantAfford.SetActive(false);
        }
        else if (maxPots > amountOfPots)
        {
            potOutOfStock.SetActive(false);
        }
    }


    public void PlantInfo()
    {
        if (maxPlants <= amountOfPlants)
        {
            plantOutOfStock.SetActive(true);
        }
        else if (playerMoney < totalCost + plantCost)
        {
            plantCantAfford.SetActive(true);
        }
        else if (playerMoney >= plantCost)
        {
            plantCantAfford.SetActive(false);
        }
        else if (maxPlants > amountOfPlants)
        {
            plantOutOfStock.SetActive(false);
        }
    }


    public void ManaPlantInfo()
    {
        if (TableManager.instance.unlockedTables < 5)
        {
            SetManaPlantOutOfStock(true);
        }
        else
        {
            if (maxManaPlants <= amountOfManaPlants)
            {
                manaPlantOutOfStock.SetActive(true);
            }
            else if (playerMoney < totalCost + manaPlantCost)
            {
                manaPlantCantAfford.SetActive(true);
            }
            else if (playerMoney >= manaPlantCost)
            {
                manaPlantCantAfford.SetActive(false);
            }
            
            if (maxManaPlants > amountOfManaPlants)
            {
                SetManaPlantOutOfStock(false);
            }
        }
    }

    void SetManaPlantOutOfStock(bool setValue)
    {
        if (setValue == true)
        {
            manaPlantOutOfStock.SetActive(true);
            manaPlantAdd.enabled = false;
            manaPlantReduce.enabled = false;
        }
        else
        {
            manaPlantOutOfStock.SetActive(false);
            manaPlantAdd.enabled = true;
            manaPlantReduce.enabled = true;
        }
    }
    
    public void ManaStorageItemInfo()
    {
        if (TableManager.instance.unlockedTables < 5)
        {
            SetManaStorageOutOfStock(true);
        }
        else
        {
            if (maxManaStorageItems <= amountOfManaStorageItems)
            {
                manaStorageItemOutOfStock.SetActive(true);
            }
            else if (playerMoney < totalCost + manaStorageItemCost)
            {
                manaStorageItemCantAfford.SetActive(true);
            }
            else if (playerMoney >= manaStorageItemCost) 
            {
                manaStorageItemCantAfford.SetActive(false);
            }
            
            if (maxManaStorageItems > amountOfManaStorageItems)
            {
                SetManaStorageOutOfStock(false);
            }
        }
    }

    void SetManaStorageOutOfStock(bool setValue)
    {
        if (setValue)
        {
            manaStorageItemOutOfStock.SetActive(true);
            manaStorageItemAdd.enabled = false;
            manaStorageItemReduce.enabled = false;
        }
        else
        {
            manaStorageItemOutOfStock.SetActive(false);
            manaStorageItemAdd.enabled = true;
            manaStorageItemReduce.enabled = true;
        }
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
