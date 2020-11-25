using System;
using UnityEngine;

[DefaultExecutionOrder(-10)]
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
    
    void Awake()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
        
        currentInteractState = InteractState.select;
    }
    
    public event Action<InteractState> onChangeInteractState;
    public void ChangeInteractState( InteractState newInteractState ) 
    {
        currentInteractState = newInteractState;
        onChangeInteractState?.Invoke(newInteractState);
    }
}
