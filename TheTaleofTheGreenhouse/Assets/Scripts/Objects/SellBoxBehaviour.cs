using UnityEngine;

public class SellBoxBehaviour : MonoBehaviour
{
    public static SellBoxBehaviour instance;
    
    public GameObject[] itemsToSell;
    public int maxNumbertoSell = 30;
    private int currentSlot;
    bool firstTimeUsing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    
    private void Start()
    {
        itemsToSell = new GameObject[maxNumbertoSell];
        firstTimeUsing = true;
    }


    private void OnMouseOver()
    {
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }

        if (PlayerInteract.instance.allowedTointeract)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                if (firstTimeUsing)
                {
                    NoteManager.instance.ActivateNote(NoteManager.NoteStates.SellBasketNote);
                    firstTimeUsing = false;
                }

                if (PlayerInteract.instance.inventoryItem != null)
                {
                    if (PlayerInteract.instance.inventoryItem.CompareTag("PlantMana") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("PlantNormal") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("Pot") ||
                    PlayerInteract.instance.inventoryItem.CompareTag("ManaStorage"))
                    {
                        GameObject sellItem = PlayerInteract.instance.inventoryItem;  
                        
                        if (sellItem.tag == "Pot" || sellItem.tag == "ManaStorage")
                        {
                            if (Tools.GetSplitStackSize(sellItem) > 1)
                            {
                                GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.StackSellWarning);
                                return;
                            }
                            if (sellItem.tag == "ManaStorage")
                            {
                                if (sellItem.GetComponent<ManaCubeBehavior>().storedMana != 0)
                                {
                                    GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.StackSellWarning);
                                    return;
                                }
                            }

                            sellItem.transform.GetChild(0).gameObject.SetActive(false);                          
                            sellItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        }
                        if (sellItem.tag == "PlantNormal" || sellItem.tag == "PlantMana")
                        {
                            if (sellItem.GetComponent<PlantStates>().currentState == PlantStates.PlantState.Dead)
                            {
                                GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.SellDeadWarning);
                                return;
                            }
                            sellItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        }
                        else
                        {
                            sellItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        }
                     
                        PlayerInteract.instance.inventoryItem = null;
                        sellItem.GetComponent<InteractableEffect>().Enable(false);

                        if (currentSlot < maxNumbertoSell)
                        {
                            for (int i = currentSlot; i < maxNumbertoSell;)
                            {
                                if (itemsToSell[i] == null)
                                {
                                    itemsToSell[i] = sellItem;
                                    sellItem.transform.parent = gameObject.transform;
                                    sellItem.transform.localPosition = new Vector3(0, 0, 0);
                                    if (sellItem.tag == "PlantMana")
                                    {
                                        sellItem.transform.position = sellItem.transform.position
                                                                    + new Vector3(Random.Range(-0.07f, 0.175f), Random.Range(0.4f, 0.5f), -0.58f);
                                    }
                                    else
                                    {
                                        sellItem.transform.position = sellItem.transform.position
                                                                    + new Vector3(Random.Range(-0.076f, 0.2f), Random.Range(0.080f, 0.145f), -0.15f);
                                    }
                                    currentSlot++;
                                }
                                break;
                            }
                        }
                        else
                        {
                            Debug.Log("Can't sell more items today");
                        }

                        PlayerState.instance.ChangeHandState(PlayerState.HandState.None);
                        PlayerState.instance.ChangeInteractState(PlayerState.InteractState.@select);
                    }
                    else
                    {
                        Debug.Log("Tryibng to do sellwarning");
                        GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.SellWarning);
                    }
                }
            }              
        }
    }


    public void ResetSlots()
    {
        currentSlot = 0;
    }
}
