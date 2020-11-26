using UnityEngine;

public class PotBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool isWatered = false;
    
    public void FillWater()
    {
        isWatered = true;
        Debug.Log("Watered");
    }

    public bool GetWatered()
    {
        return isWatered;
    }

    public void EmptyWater()
    {
        isWatered = false;
    }
}

 
