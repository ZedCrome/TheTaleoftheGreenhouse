using UnityEngine;

public class ManaCubeBehavior : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    InteractableEffect interactableEffect;

    public Sprite emptyCube;
    public Sprite firstStageCube;
    public Sprite seccondStageCube;
    public Sprite fullStageCube;

    public Material firstStageMaterial;
    public Material seccondStageMaterial;
    public Material fullStageMaterial;
    private Material emptyMaterial;

    private int emptyCubeValue = 0;
    private int firstStageCubeValue = 5;
    private int seccondStageCubeValue = 10;
    private int fullStageCubeValue = 15;

    public int storedMana = 0;
    public int maxMana = 15;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactableEffect = GetComponent<InteractableEffect>();
        emptyMaterial = spriteRenderer.material;
    }

    private void Update()
    {    
        if (storedMana > emptyCubeValue && storedMana < seccondStageCubeValue)
        {
            spriteRenderer.sprite = firstStageCube;
            interactableEffect.outliveNotActive = firstStageMaterial;
        }

        else if (storedMana > firstStageCubeValue && storedMana < fullStageCubeValue)
        {
            spriteRenderer.sprite = seccondStageCube;
            interactableEffect.outliveNotActive = seccondStageMaterial;
        }

        else if (storedMana >= fullStageCubeValue)
        {
            spriteRenderer.sprite = fullStageCube;
            interactableEffect.outliveNotActive = fullStageMaterial;
            storedMana = maxMana;
        }

        else
        {
            spriteRenderer.sprite = emptyCube;
            interactableEffect.outliveNotActive = emptyMaterial;
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
