using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState instance;

    public enum InteractState
    {
        select,
        tool,
        placement
    }

    public InteractState currentInteractState;
    
    void Start()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
        
        currentInteractState = InteractState.select;
    }
}
