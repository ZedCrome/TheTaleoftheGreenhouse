using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    void OnChangeInteractiveState(PlayerState.InteractState newState)
    {
        if (newState == PlayerState.InteractState.@select)
        {
            Debug.Log("Interact state is Select");
        }
        
        if (newState == PlayerState.InteractState.placement)
        {
            Debug.Log("Interact state is Placement");
        }
    }

    private void OnEnable()
    {
        PlayerState.instance.onChangeInteractState += OnChangeInteractiveState;
    }
    
    private void OnDisable()
    {
        PlayerState.instance.onChangeInteractState -= OnChangeInteractiveState;
    }
}
