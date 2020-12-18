using UnityEngine;

public class SummonPlant : MonoBehaviour
{
    public ShopBehaviourBuy shopBehaviourBuy;
    public GameObject summonSpot;
    private SpriteRenderer spriteRenderer;

    private int numberOfSummonPlants;
    private int numberOfPlants;
    private int currentGold;

    private void Start()
    {
        spriteRenderer = summonSpot.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameManager.instance.currentGameState == GameManager.GameState.GameLoop)
        {
            if (summonSpot.activeSelf)
            {
                if (summonSpot.GetComponent<ObjectSlot>().objectInSlot == null)
                {
                    spriteRenderer.enabled = false;
                    if (summonSpot.transform.childCount == 0)
                    {
                        summonSpot.SetActive(false);
                    }
                }
            }  
        }
    }

    private void OnEnable()
    {
            DayNightCycle.instance.onSleep += OnSleep;
    }

    private void OnDisable()
    {       
            DayNightCycle.instance.onSleep -= OnSleep;
    }

    void OnSleep()
    {
        numberOfPlants = GameObject.FindGameObjectsWithTag("PlantNormal").Length
                         + GameObject.FindGameObjectsWithTag("PlantMana").Length;
        currentGold = shopBehaviourBuy.playerMoney;

        if (numberOfPlants == 0 && currentGold <= 10 && shopBehaviourBuy.hasBoughtSomething == false) 
        {
            summonSpot.SetActive(true);
            ActivatePlant();
        }      
        else
        {
            summonSpot.SetActive(false);
        }
    }

    void ActivatePlant()
    {
        numberOfSummonPlants++;
        GameObject newPlant = PrefabManager.instance.CreateNewObjectInstance("PlantNormal");
        newPlant.GetComponent<PlantStates>().currentState = PlantStates.PlantState.Cutting;
        summonSpot.GetComponent<ObjectSlot>().objectInSlot = newPlant;
        summonSpot.GetComponent<ObjectSlot>().FillSlot(newPlant);

        if (numberOfSummonPlants < 3)
        {
            GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.PlantOfGift);
        }
        else
        {
            GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.PlantOfGiftRoast);
        }
    }

}
