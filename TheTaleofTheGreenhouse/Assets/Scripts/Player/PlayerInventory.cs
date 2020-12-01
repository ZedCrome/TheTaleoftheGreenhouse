using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    private GameObject item;
    private bool saplingInInventory;
    
    void Start()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
    }

    public void AddSapling()
    {
        if (saplingInInventory == false)
        {
            saplingInInventory = true;
        }
    }

    public bool CanCarryMoreSaplings()
    {
        if (saplingInInventory == true)
        {
            return false;
        }

        return true;
    }
}
