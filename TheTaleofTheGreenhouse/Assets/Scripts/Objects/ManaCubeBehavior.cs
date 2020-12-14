using UnityEngine;

public class ManaCubeBehavior : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    public Sprite emptyCube;
    public Sprite firstStageCube;
    public Sprite seccondStageCube;
    public Sprite fullStageCube;

    public Material firstStageMaterial;
    public Material seccondStageMaterial;
    public Material fullStageMaterial;

    private int emptyCubeValue = 0;
    private int firstStageCubeValue = 5;
    private int seccondStageCubeValue = 10;
    private int fullStageCubeValue = 15;

    public int storedMana = 0;
    public int maxMana = 15;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {    
        if (storedMana > emptyCubeValue && storedMana < seccondStageCubeValue)
        {
            spriteRenderer.sprite = firstStageCube;
        }

        else if (storedMana > firstStageCubeValue && storedMana < fullStageCubeValue)
        {
            spriteRenderer.sprite = seccondStageCube;
        }

        else if (storedMana >= fullStageCubeValue)
        {
            spriteRenderer.sprite = fullStageCube;
            storedMana = maxMana;
        }

        else
        {
            spriteRenderer.sprite = emptyCube;
        }
    }

    public void AddMana(int mana)
    {
        storedMana += mana;
    }

    public int GetMana()
    {       
        return storedMana;
    }

    public void EmptyMana()
    {
        storedMana = 0;
        spriteRenderer.sprite = emptyCube;
    }
}
