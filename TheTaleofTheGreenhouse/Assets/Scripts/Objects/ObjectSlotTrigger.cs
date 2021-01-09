using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSlotTrigger : MonoBehaviour
{
    public bool ignoreHandState;
    [SerializeField] private PlayerState.HandState setHandStateTrigger;
    [SerializeField] private ObjectSlot objectSlot;
    [SerializeField] private Collider2D collider;
    private void OnEnable()
    {
        PlayerState.instance.onChangeHandState += OnChangeHandState;
    }
    
    private void OnDisable()
    {
        PlayerState.instance.onChangeHandState -= OnChangeHandState;
    }

    private void Start()
    {
        objectSlot = this.transform.parent.GetComponent<ObjectSlot>();
        collider = GetComponent<Collider2D>();

        OnChangeHandState(PlayerState.instance.currentHandState);
    }
    
    void OnChangeHandState(PlayerState.HandState state)
    {
        if (objectSlot.objectInSlot != null)
        {
            if (objectSlot.objectInSlot.CompareTag("PlantMana") || objectSlot.objectInSlot.CompareTag("PlantNormal"))
            {
                if (setHandStateTrigger == PlayerState.HandState.WaterCan)
                {
                    collider.enabled = true;
                }
                else
                {
                    collider.enabled = false;
                }
            }
            else
            {
                if (ignoreHandState == false)
                {
                    if (setHandStateTrigger == state)
                    {
                        collider.enabled = true;
                    }
                    else
                    {
                        collider.enabled = false;
                    }
                }
            }
        }
    }

    private void OnMouseOver()
    {
        objectSlot.MouseOver();
    }

    private void OnMouseExit()
    {
        objectSlot.MouseExit();
    }
}
