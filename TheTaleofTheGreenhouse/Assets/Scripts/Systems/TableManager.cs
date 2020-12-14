using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager instance;

    public int unlockedTables = 4;
    
    public GameObject[] table;

    private void Awake()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
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
                table[i].SetActive(true);
            }
            else
            {
                table[i].SetActive(false);
            }
        }
    }
}
