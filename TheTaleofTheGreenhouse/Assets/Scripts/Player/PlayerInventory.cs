using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    
    public GameObject inventoryItem;
    
    void Start()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
    }
}
