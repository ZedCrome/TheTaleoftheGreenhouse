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
    public Material firstStageMaterialOutline;
    public Material seccondStageMaterialOutline;
    public Material fullStageMaterialOutline;
    private Material emptyMaterial;
    private Material emptyMaterialOutline;

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
        emptyMaterialOutline = interactableEffect.outlineActive;
    }

    private void Update()
    {
        if (interactableEffect.ActiveOutline())
        {
            spriteRenderer.material = interactableEffect.outlineActive;
        }
        else
        {
            spriteRenderer.material = interactableEffect.outliveNotActive;
        }

        if (storedMana > emptyCubeValue && storedMana < seccondStageCubeValue)
        {
            spriteRenderer.sprite = firstStageCube;
            interactableEffect.outliveNotActive = firstStageMaterial;
            interactableEffect.outlineActive = firstStageMaterialOutline;
        }

        else if (storedMana > firstStageCubeValue && storedMana < fullStageCubeValue)
        {
            spriteRenderer.sprite = seccondStageCube;
            interactableEffect.outliveNotActive = seccondStageMaterial;
            interactableEffect.outlineActive = seccondStageMaterialOutline;
        }

        else if (storedMana >= fullStageCubeValue)
        {
            spriteRenderer.sprite = fullStageCube;
            interactableEffect.outliveNotActive = fullStageMaterial;
            interactableEffect.outlineActive = fullStageMaterialOutline;
            storedMana = maxMana;
        }

        else
        {
            spriteRenderer.sprite = emptyCube;
            interactableEffect.outliveNotActive = emptyMaterial;
            interactableEffect.outlineActive = emptyMaterialOutline;
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
