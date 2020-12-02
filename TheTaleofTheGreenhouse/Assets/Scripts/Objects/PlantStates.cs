using System.Security.AccessControl;
using UnityEngine;

public class PlantStates : MonoBehaviour
{
    private Sprite currentSprite;

    public Sprite deadSprite;
    public Sprite fullGrownSprite;
    public Sprite adultSprite;
    public Sprite youngSprite;
    public Sprite sproutSprite;

    SpriteRenderer spriteRenderer;

    public enum PlantState { Sprout, Young, Adult, FullGrown, Dead};
    public PlantState currentState = 0;

    private int daysWithoutWater;
    private int daysWithoutWaterLimit = 2;
    private bool isWatered;

    private bool hasMana;
    public bool lostMana;

    public bool IsWatered
    {
        get { return isWatered;}
        set
        {
            isWatered = value;
            daysWithoutWater = 0;
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }
    
    private void OnEnable()
    {
        DayNightCycle.instance.onSleep += OnSleep;
    }
    
    private void OnDisable()
    {
        DayNightCycle.instance.onSleep -= OnSleep;
    }

    public bool CutPlant()
    {
        if (currentState == PlantState.FullGrown)
        {
            currentState = PlantState.Adult;
            return true;
        }
        
        return false;
    }

    private void Update()
    {
        spriteRenderer.sprite = currentSprite;
        switch (currentState)
        {
            case PlantState.Sprout:
                {
                    currentSprite = sproutSprite;
                    break;
                }
            case PlantState.Young:
                {
                    currentSprite = youngSprite;
                    break;
                }
            case PlantState.Adult:
                {
                    currentSprite = adultSprite;
                    break;
                }
            case PlantState.FullGrown:
                {
                    currentSprite = fullGrownSprite;
                    if (gameObject.tag == "PlantMana")
                    {
                        if (lostMana == false)
                        {
                            hasMana = true;
                        }
                        else
                        {
                            hasMana = false;
                        }
                    }
                    break;
                }
            case PlantState.Dead:
                {
                    currentSprite = deadSprite;
                    hasMana = false;
                    break;
                }
        }
    }

    void OnSleep()
    {
        IsWatered = transform.parent.parent.GetComponent<PotBehaviour>().GetIsWatered();

        if (isWatered == false)
        {
            daysWithoutWater++;
        }

        switch (currentState)
        {
            case PlantState.Sprout:
            {
                if (isWatered)
                {
                    currentState = PlantState.Young;
                    transform.parent.parent.GetComponent<PotBehaviour>().EmptyWater();
                }
                else if (daysWithoutWater == daysWithoutWaterLimit)
                {
                    currentState = PlantState.Dead;
                }

                break;
            }
            case PlantState.Young:
            {
                if (isWatered)
                {
                    currentState = PlantState.Adult;
                    transform.parent.parent.GetComponent<PotBehaviour>().EmptyWater();
                }
                else if (daysWithoutWater == daysWithoutWaterLimit)
                {
                    currentState = PlantState.Dead;
                }

                break;
            }
            case PlantState.Adult:
            {
                if (isWatered)
                {
                    lostMana = false;
                    currentState = PlantState.FullGrown;
                    transform.parent.parent.GetComponent<PotBehaviour>().EmptyWater();
                }
                else if (daysWithoutWater == daysWithoutWaterLimit)
                {
                    currentState = PlantState.Dead;
                }

                break;
            }
            case PlantState.FullGrown:
            {
                transform.parent.parent.GetComponent<PotBehaviour>().EmptyWater();


                if (daysWithoutWater == daysWithoutWaterLimit)
                {
                    currentState = PlantState.Dead;
                }               
                break;
            }
        }
    }
}
