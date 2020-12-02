using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    public GameObject cuttingPrefab;
    
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
    
    private void OnEnable()
    {
        PlayerState.instance.onChangeHandState += OnChangeHandState;
    }
    
    private void OnDisable()
    {
        PlayerState.instance.onChangeHandState -= OnChangeHandState;
    }

    void OnChangeHandState(PlayerState.HandState newHandState)
    {
        if (newHandState == PlayerState.HandState.Cutting)
        {
            SpawnCutting();
        }
    }

    public void SpawnCutting()
    {
        GameObject newCutting = PrefabManager.instance.CreateNewObjectInstance("Cutting");
        
        PlayerInteract.instance.inventoryItem = newCutting;
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
