using System.Security.AccessControl;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif
using UnityEngine;

public class PlantStates : MonoBehaviour
{
    private Sprite currentSprite;
    public Sprite deadSprite;

    public Sprite fullGrownSprite;
    public Sprite adultSprite;
    public Sprite youngSprite;
    public Sprite sproutSprite;

    public Sprite deadFullGrownSprite;
    public Sprite deadAdultSprite;
    public Sprite deadYoungSprite;
    public Sprite deadSproutSprite;

    SpriteRenderer spriteRenderer;

    public enum PlantState { Sprout, Young, Adult, FullGrown, Dead};
    public PlantState currentState = 0;

    public int daysWithoutWater;
    private int daysWithoutWaterLimit = 2;
    private bool isWatered;

    public bool hasMana;
    public bool lostMana;

    public bool IsWatered
    {
        get { return isWatered;}
        set
        {
            isWatered = value;
            if (value == true)
            {
                daysWithoutWater = 0;
            }
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
                    deadSprite = deadSproutSprite;
                    lostMana = true;
                    break;
                }
            case PlantState.Young:
                {
                    currentSprite = youngSprite;
                    deadSprite = deadYoungSprite;
                    lostMana = true;
                    break;
                }
            case PlantState.Adult:
                {
                    currentSprite = adultSprite;
                    deadSprite = deadAdultSprite;
                    lostMana = true;
                    break;
                }
            case PlantState.FullGrown:
                {
                    currentSprite = fullGrownSprite;
                    deadSprite = deadFullGrownSprite;
                    if (gameObject.tag == "PlantMana")
                    {
                        if (hasMana == true)
                        {
                            lostMana = false;
                        }
                        else
                        {
                            lostMana = true;
                        }

                        if(lostMana == true)
                        {
                            currentState = PlantState.Adult;
                        }
                    }
                    break;
                }
            case PlantState.Dead:
                {
                    currentSprite = deadSprite;
                    lostMana = true;
                    break;
                }
        }
    }

    void OnSleep()
    {
        if (gameObject.transform.parent.parent == null)
        {
            return;
        }
        if (gameObject.transform.parent.parent.tag == "Pot")
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
                            hasMana = true;
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
}
