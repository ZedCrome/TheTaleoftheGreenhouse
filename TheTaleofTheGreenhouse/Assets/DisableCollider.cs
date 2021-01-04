using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollider : MonoBehaviour
{
    ObjectSlot objetslot;
    Collider2D collider;
    GameObject savedPot;
    bool savedItem;

    private void Start()
    {
        objetslot = GetComponent<ObjectSlot>();
        collider = GetComponent<Collider2D>();
    }


    private void Update()
    {
        if (objetslot.objectInSlot != null)
        {
            collider.enabled = true;

            if (objetslot.objectInSlot.CompareTag("Pot"))
            {         
                if (!savedItem)
                {
                    savedPot = objetslot.objectInSlot;
                    savedPot.GetComponentInChildren<Collider2D>().enabled = false;
                    savedItem = true;                
                }
           
            }
           
        }
       
        else
        {
            collider.enabled = false;

            if(savedItem)
            {
                savedPot.GetComponentInChildren<Collider2D>().enabled = true;
                savedPot = null;
                savedItem = false;
            }

        }
    }
}
