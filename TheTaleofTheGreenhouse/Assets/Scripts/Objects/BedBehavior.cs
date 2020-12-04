using UnityEngine;

public class BedBehavior : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(1) && PlayerInteract.instance.allowedTointeract)
        {

            DayNightCycle.instance.Sleep();
        }
    }


    private void OnMouseEnter()
    {
        ChangeMouseCursor.instance.inputObjectTag = gameObject.tag;
    }

    private void OnMouseExit()
    {
        ChangeMouseCursor.instance.inputObjectTag = "Default";
    }

}
