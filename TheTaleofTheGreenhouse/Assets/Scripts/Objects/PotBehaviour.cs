using UnityEngine;

public class PotBehaviour : MonoBehaviour
{
    private bool isWatered = false;
    
    public void FillWater()
    {
        isWatered = true;
    }

    public bool GetWatered()
    {
        return isWatered;
    }
}

 
