using System;
using UnityEngine;

public class BedBehavior : MonoBehaviour
{
    private InteractableEffect interactEffect;

    private void Start()
    {
        interactEffect = GetComponent<InteractableEffect>();
    }

    private void OnEnable()
    {
        PlayerState.instance.onChangeHandState += OnChangeHandState;
    }
    
    private void OnDisable()
    {
        PlayerState.instance.onChangeHandState -= OnChangeHandState;
    }

    void OnChangeHandState(PlayerState.HandState newHandstate)
    {
        if (newHandstate == PlayerState.HandState.None)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 2;
        }
    }
    
    private void OnMouseOver()
    {
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }

        if (PlayerInteract.instance.allowedTointeract)
        {
            interactEffect.Enable(true);
            
            if (Input.GetMouseButtonDown(1))
            {
                if (PlayerState.instance.currentHandState == PlayerState.HandState.None)
                {
                    DayNightCycle.instance.SleepPrompt();
                }
            }
        }
    }


    private void OnMouseEnter()
    {
        ChangeMouseCursor.instance.inputObjectTag = gameObject.tag;
    }

    private void OnMouseExit()
    {
        interactEffect.Enable(false);
        ChangeMouseCursor.instance.inputObjectTag = "Default";
    }

}
