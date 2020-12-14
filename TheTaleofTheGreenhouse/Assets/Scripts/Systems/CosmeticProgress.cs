using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticProgress : MonoBehaviour
{
    public GameObject[] cosmeticList;

    [SerializeField]
    private int currentProgress;
    
    private void Start()
    {
        cosmeticList = GameObject.FindGameObjectsWithTag("Cosmetic");

        foreach (var cosmetic in cosmeticList)
        {
            cosmetic.SetActive(false);
        }
        
        currentProgress = 0;
    }
    
    public void AddCosmetic()
    {
        currentProgress++;
        
        for (int i = 0; i < cosmeticList.Length; i++)
        {
            if (i <= currentProgress - 1)
            {
                cosmeticList[i].SetActive(true);
            }
            else
            {
                cosmeticList[i].SetActive(false);
            }
        }
    }
}
