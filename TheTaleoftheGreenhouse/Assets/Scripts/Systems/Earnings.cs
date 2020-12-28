using System;
using System.Collections;
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

    
    public IEnumerator ActivateNotification()
    {
        
        if (_removeStartInfo.beginnerInfo.activeInHierarchy)
        {
            _removeStartInfo.ExitInfo();
        }
        LeanTween.moveX(earnings, Screen.width * 0.91f, 0.5f).setEaseLinear();
        yield return new WaitForSeconds(12);
        DeactivateNotification();
    }

    
    public void DeactivateNotification()
    {
        LeanTween.moveX(earnings, Screen.width * 3f, 2f).setEaseLinear();
    }
    
    
    public void EarningsNotification()
    {
        gainedGold = sellItems.GetGold() / 3;

        soldValue.text = gainedGold.ToString();
        gold.text = shopBehaviourBuy.playerMoney.ToString();
    }
}
