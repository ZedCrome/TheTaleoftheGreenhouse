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
    public PlantState currenState = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }

    private void Update()
    {
        spriteRenderer.sprite = currentSprite;
        switch (currenState)
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

    //take information of the night state
    //update renderer
}
