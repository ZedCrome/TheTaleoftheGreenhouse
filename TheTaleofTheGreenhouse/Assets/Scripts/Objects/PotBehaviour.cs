using UnityEngine;

public class PotBehaviour : MonoBehaviour
{
    [SerializeField]
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

 
