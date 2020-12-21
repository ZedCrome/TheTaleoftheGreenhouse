﻿using System;
using UnityEngine;
using TMPro;

public class Earnings : MonoBehaviour
{
     [SerializeField] GameObject earnings;
    private ShopBehaviourBuy shopBehaviourBuy;
    private removeStartInfo _removeStartInfo;
    private SellItems sellItems;
    private int gainedGold;
    [SerializeField] private TMP_Text soldValue;
    [SerializeField] private TMP_Text gold;
    
    private void Start()
    {
        shopBehaviourBuy = FindObjectOfType<ShopBehaviourBuy>();
        sellItems = FindObjectOfType<SellItems>();
        _removeStartInfo = FindObjectOfType<removeStartInfo>();
    }

    
    public void ActivateNotification()
    {
        
        if (_removeStartInfo.beginnerInfo.activeInHierarchy)
        {
            _removeStartInfo.ExitInfo();
        }

        LeanTween.moveX(earnings, Screen.width * 0.91f, 0.5f).setEaseLinear();
    }

    
    public void DeactivateNotification()
    {
        LeanTween.moveX(earnings, Screen.width * 1.5f, 2f).setEaseLinear();
    }
    
    
    public void EarningsNotification()
    {
        gainedGold = sellItems.GetGold() / 2;

        soldValue.text = gainedGold.ToString();
        gold.text = shopBehaviourBuy.playerMoney.ToString();
    }
    
    
    
    
}