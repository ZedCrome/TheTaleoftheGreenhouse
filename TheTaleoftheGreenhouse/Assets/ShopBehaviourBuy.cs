using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopBehaviourBuy : MonoBehaviour
{
    [Header("Table")] [Space(5)]
    [SerializeField] private Button tableAdd;
    [SerializeField] private Button tableReduce;
    [SerializeField] private TMP_Text tableAmount;
    [SerializeField] private int maxTables = 8;
    [SerializeField] private Slider tablePrimarySlider1;
    [SerializeField] private Slider tableSecondarySlider1;
    [SerializeField] private Slider tablePrimarySlider2;
    [SerializeField] private Slider tableSecondarySlider2;
    private int amountOfTables = 0;
    private int ownedTables = 0;

    [Header("Pot")] [Space(5)]
    [SerializeField] private Button potAdd;
    [SerializeField] private Button potReduce;
    [SerializeField] private TMP_Text potAmount;
    [SerializeField] private Slider potPrimarySlider1;
    [SerializeField] private Slider potSecondarySlider1;
    [SerializeField] private Slider potPrimarySlider2;
    [SerializeField] private Slider potSecondarySlider2;
    [SerializeField] private int maxPots = 10;
    private int amountOfPots = 0;
    private int ownedPots = 0;

    [Header("Plant")] [Space(5)] 
    [SerializeField] private Button plantAdd;
    [SerializeField] private Button plantReduce;
    [SerializeField] private TMP_Text plantAmount;
    [SerializeField] private Slider plantPrimarySlider1;
    [SerializeField] private Slider plantSecondarySlider1;
    [SerializeField] private Slider plantPrimarySlider2;
    [SerializeField] private Slider plantSecondarySlider2;
    [SerializeField] private int maxPlants = 10;
    private int amountOfPlants = 0;
    private int ownedPlants = 0;
    
    [Header("ManaPlant")] [Space(5)] 
    [SerializeField] private Button manaPlantAdd;
    [SerializeField] private Button manaPlantReduce;
    [SerializeField] private TMP_Text manaPlantAmount;
    [SerializeField] private Slider manaPlantPrimarySlider1;
    [SerializeField] private Slider manaPlantSecondarySlider1;
    [SerializeField] private Slider manaPlantPrimarySlider2;
    [SerializeField] private Slider manaPlantSecondarySlider2;
    [SerializeField] private int maxmanaPlants = 10;
    private int amountOfmanaPlants = 0;
    private int ownedManaPlants = 0;

    [Header("ManaStorageItem")] [Space(5)]
    [SerializeField] private Button manaStorageItemAdd;
    [SerializeField] private Button manaStorageItemReduce;
    [SerializeField] private TMP_Text manaStorageItemAmount;
    [SerializeField] private Slider manaStorageItemPrimarySlider1;
    [SerializeField] private Slider manaStorageItemSecondarySlider1;
    [SerializeField] private Slider manaStorageItemPrimarySlider2;
    [SerializeField] private Slider manaStorageItemSecondarySlider2;
    [SerializeField] private int maxManaStorageItems;
    private int amountOfManaStorageItems = 0;
    private int ownedManaStorageItems = 0;

    
    public void addTable()
    {
        if (amountOfTables < maxTables)
        {
            amountOfTables++;
        }
        tableAmount.text = amountOfTables.ToString();
    }
    public void reduceTable()
    {
        if (amountOfTables > 0)
        {
            amountOfTables--;
        }
        tableAmount.text = amountOfTables.ToString();
    }


    public void addPot()
    {
        if (amountOfPots < maxPots)
        {
            amountOfPots++;
        }
        potAmount.text = amountOfPots.ToString();
    }
    public void reducePot()
    {
        if (amountOfPots > 0)
        {
            amountOfPots--;
        }
        potAmount.text = amountOfPots.ToString();
    }
    
    
    public void addPlant()
    {
        if (amountOfPlants < maxPlants)
        {
            amountOfPlants++;
        }
        plantAmount.text = amountOfPlants.ToString();
    }
    public void reducePlant()
    {
        if (amountOfPlants > 0)
        {
            amountOfPlants--;
        }
        plantAmount.text = amountOfPlants.ToString();
    }
    
    
    public void addManaPlant()
    {
        if (amountOfmanaPlants < maxmanaPlants)
        {
            amountOfmanaPlants++;
        }
        manaPlantAmount.text = amountOfmanaPlants.ToString();
    }
    public void reduceManaPlant()
    {
        if (amountOfmanaPlants > 0)
        {
            amountOfmanaPlants--;
        }
        manaPlantAmount.text = amountOfmanaPlants.ToString();
    }

    
    public void addManaStorageItem()
    {
        if (amountOfManaStorageItems < maxManaStorageItems)
        {
            amountOfManaStorageItems++;
        }
        manaStorageItemAmount.text = amountOfManaStorageItems.ToString();
    }
    public void reduceManaStorageItem()
    {
        if (amountOfManaStorageItems > 0)
        {
            amountOfManaStorageItems--;
        }
        manaStorageItemAmount.text = amountOfManaStorageItems.ToString();
    }

    public void buy()
    {
        
    }
}
