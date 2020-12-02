using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    private GameObject item;
    private int cuttingsInInventory;
    public int MaxCuttings = 2;
    
    void Start()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
    }

    public void AddCutting()
    {
        if (cuttingsInInventory < MaxCuttings)
        {
            cuttingsInInventory += 1;
        }
    }

    public bool CanCarryMoreCuttings()
    {
        if (cuttingsInInventory == MaxCuttings)
        {
            return false;
        }

        return true;
    }

    public bool GetCuttingsExist()
    {
        if (cuttingsInInventory > 0)
        {
            return true;
        }

        return false;
    }
}
