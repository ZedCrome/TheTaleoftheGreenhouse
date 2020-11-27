using UnityEngine;

public class BedBehavior : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {

            DayNightCycle.instance.Sleep();
        }
    }
}
