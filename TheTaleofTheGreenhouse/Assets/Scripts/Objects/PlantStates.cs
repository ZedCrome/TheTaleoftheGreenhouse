using UnityEngine;

public class PlantStates : MonoBehaviour
{
    public Sprite deadSprite;
    public Sprite fullGrownSprite;
    public Sprite adultSprite;
    public Sprite youngSprite;
    public Sprite sproutSprite;
    private Sprite currentSprite;

    SpriteRenderer spriteRenderer;

    public enum PlantState { Sprout, Young, Adult, FullGrown, Dead};
    public PlantState currentState = 0;

    private int daysWithoutWater;
    private int daysWithoutWaterLimit = 2;
    private bool isWatered;

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
                    //Can give Cuttings OR Mana
                    currentSprite = fullGrownSprite;
                    break;
                }
            case PlantState.Dead:
                {
                    currentSprite = deadSprite;
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

    //take information of the night state
    //update renderer
}
