using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CosmeticProgress))]
public class TableManager : MonoBehaviour
{
    public static TableManager instance;

    private CosmeticProgress cosmeticProgress;
    
    public int unlockedTables = 4;
    
    public GameObject[] table;

    private void Awake()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        cosmeticProgress = GetComponent<CosmeticProgress>();
    }

    private void OnEnable()
    {
        DayNightCycle.instance.onSleep += OnSleep;
    }
    
    private void OnDisable()
    {
        DayNightCycle.instance.onSleep -= OnSleep;
    }

    void OnSleep()
    {
        for (int i = 0; i < table.Length; i++)
        {
            if (i <= unlockedTables - 1)
            {
                if (table[i].activeSelf == false)
                {
                    cosmeticProgress.AddCosmetic();
                }
                
                table[i].SetActive(true);
            }
            else
            {
                table[i].SetActive(false);
            }
        }

        if (unlockedTables >= table.Length)
        {
            cosmeticProgress.AddCosmetic();
        }
    }
}
