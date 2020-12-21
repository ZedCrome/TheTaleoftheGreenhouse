using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopbehaviourNotes : MonoBehaviour
{
    [Header("Buttons")] [Space(5)]
    [SerializeField] private Button shear;
    [SerializeField] private Button wateringCan;
    [SerializeField] private Button compost;
    [SerializeField] private Button sellBasket;
    [SerializeField] private Button delivery;
    [SerializeField] private Button normalPlant;
    [SerializeField] private Button manaPlant;
    [SerializeField] private Button manaCatcher;
    [SerializeField] private Button manaStorageItem;
    [SerializeField] private Button manaUI;
    [SerializeField] private Button Stacking;
    [SerializeField] private Button SellValues;

    [Header("Notes")] [Space(5)]
    [SerializeField] private GameObject shearNote;
    [SerializeField] private GameObject wateringCanNote;
    [SerializeField] private GameObject compostNote;
    [SerializeField] private GameObject sellBasketNote;
    [SerializeField] private GameObject deliveryNote;
    [SerializeField] private GameObject normalPlantNote;
    [SerializeField] private GameObject manaPlantNote;
    [SerializeField] private GameObject manaCatcherNote;
    [SerializeField] private GameObject manaStorageItemNote;
    [SerializeField] private GameObject manaUINote;
    [SerializeField] private GameObject StackingNote;
    [SerializeField] private GameObject SellValuesNote;

    [Header("Other")] [Space(5)] 
    [SerializeField] GameObject previousNote;

    public void Start()
    {
        previousNote = shearNote;
    }

    public void Knife()
    {
        previousNote.SetActive(false);
        previousNote = shearNote;
        shearNote.SetActive(true);
    }

    public void WaterCan()
    {
        previousNote.SetActive(false);
        previousNote = wateringCanNote;
        wateringCanNote.SetActive(true);
    }

    public void Compost()
    {
        previousNote.SetActive(false);
        previousNote = compostNote;
        compostNote.SetActive(true);
    }

    public void SellBasket()
    {
        previousNote.SetActive(false);
        previousNote = sellBasketNote;
        sellBasketNote.SetActive(true);
    }

    public void Delivery()
    {
        previousNote.SetActive(false);
        previousNote = deliveryNote;
        deliveryNote.SetActive(true);
    }

    public void NormalPlant()
    {
        previousNote.SetActive(false);
        previousNote = normalPlantNote;
        normalPlantNote.SetActive(true);
    }

    public void ManaPlant()
    {
        previousNote.SetActive(false);
        previousNote = manaPlantNote;
        manaPlantNote.SetActive(true);
    }

    public void ManaCatcher()
    {
        previousNote.SetActive(false);
        previousNote = manaCatcherNote;
        manaCatcherNote.SetActive(true);
    }

    public void ManaStorageItem()
    {
        previousNote.SetActive(false);
        previousNote = manaStorageItemNote;
        manaStorageItemNote.SetActive(true);
    }

    public void ManaUI()
    {
        previousNote.SetActive(false);
        previousNote = manaUINote;
        manaUINote.SetActive(true);
    }

    public void ItemStack()
    {
        previousNote.SetActive(false);
        previousNote = StackingNote;
        StackingNote.SetActive(true);
    }

    public void SellingValues()
    {
        previousNote.SetActive(false);
        previousNote = SellValuesNote;
        SellValuesNote.SetActive(true);
    }
}
