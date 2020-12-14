using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager instance;

    public int unlockedTables;
    
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
            if (i <= unlockedTables)
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
