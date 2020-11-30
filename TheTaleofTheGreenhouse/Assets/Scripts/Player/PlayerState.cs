using System;
using UnityEngine;
using UnityEngine.XR;

[DefaultExecutionOrder(-10)]
public class PlayerState : MonoBehaviour
{
    public static PlayerState instance;

    public enum InteractState
    {
        select,
        placement
    }

    public InteractState currentInteractState;

    public enum HandState
    {
        None,
        WaterCan,
        Pot
    }

    public HandState currentHandState;

    
    void Awake()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
           
        currentInteractState = InteractState.select;
        currentHandState = HandState.None;
    }

    public event Action<InteractState> onChangeInteractState;
    public void ChangeInteractState( InteractState newInteractState ) 
    {
        Debugger.instance.Log("InteractState: ", newInteractState);
        currentInteractState = newInteractState;
        onChangeInteractState?.Invoke(newInteractState);
    }
    
    public event Action<HandState> onChangeHandState;
    public void ChangeHandState( HandState newHandState ) 
    {
        currentHandState = newHandState;
        onChangeHandState?.Invoke(newHandState);
    }
}
