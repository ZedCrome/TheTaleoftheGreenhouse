using System.Security.AccessControl;
using UnityEngine;

public class ManaCatcherBehavior : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    InteractableEffect interactableEffect;
    private ParticleSystem particleSystem;
    public GameObject particelObject;

    public AudioClip storeManaSound;
    public AudioClip noManaAction;

    private AudioSource audioSource;
    private ManaCubeBehavior manaCubeBehavior;

    public Sprite emptyCatcher;
    public Sprite firstStageCatcher;
    public Sprite fullStageCatcher;

    public Material firstStageMaterial;
    public Material fullStageMaterial;
    public Material firstStageMaterialOutline;
    public Material fullStageMaterialOutline;
    private Material emptyMaterial;
    private Material emptyMaterialOutline;

    private int emptyValue = 0;
    private int fullValue = 10;

    public int currentMana;
    private int giveMana = 5;
    public float losingManaTimer = 15f;

    private bool rightMouseButtonLock = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactableEffect = GetComponent<InteractableEffect>();
        emptyMaterial = spriteRenderer.material;
        emptyMaterialOutline = interactableEffect.outlineActive;
        manaCubeBehavior = new ManaCubeBehavior();
        particleSystem = particelObject.GetComponent<ParticleSystem>();

    }

    private void Update()
    {  
        if (losingManaTimer <= 0)
        {
            var emission = particleSystem.emission;
            emission.burstCount = 1;
            particleSystem.Play();
            currentMana -= 1; 
            losingManaTimer = 15;
            GodTextManager.instance.ChangeGodTextState(GodTextManager.godTextStates.ManaCatcherLeaking);
        }

        if (currentMana > 0)
        {           
            losingManaTimer -= 1 * Time.deltaTime;           
        }

        if (interactableEffect.ActiveOutline())
        {
            spriteRenderer.material = interactableEffect.outlineActive;
        }
        else
        {
            spriteRenderer.material = interactableEffect.outliveNotActive;
        }

        if (currentMana > emptyValue && currentMana < fullValue)
        {
            spriteRenderer.sprite = firstStageCatcher;
            interactableEffect.outliveNotActive = firstStageMaterial;       
            interactableEffect.outlineActive = firstStageMaterialOutline;
        }

        else if (currentMana >= fullValue)
        {
            spriteRenderer.sprite = fullStageCatcher;
            interactableEffect.outliveNotActive = fullStageMaterial;
            interactableEffect.outlineActive = fullStageMaterialOutline;
        }

        else
        {
            spriteRenderer.sprite = emptyCatcher;
            interactableEffect.outliveNotActive = emptyMaterial;
            interactableEffect.outlineActive = emptyMaterialOutline;
        }


        if (PlayerState.instance.currentHandState == PlayerState.HandState.ManaCatcher)
        {
            if (Input.GetMouseButtonDown(1) && rightMouseButtonLock == false)
            {
                if (AllowedToDoCubeAction())
                {
                    if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.GetComponent<ManaCubeBehavior>().GetMana() == manaCubeBehavior.maxMana)
                    {
                        audioSource.PlayOneShot(noManaAction);
                    }
                    else if (currentMana > 0)
                    {
                        audioSource.PlayOneShot(storeManaSound);
                        if (currentMana >= 5)
                        {
                            PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.GetComponent<ManaCubeBehavior>().AddMana(giveMana);
                            losingManaTimer = 15;
                        }
                        else
                        {
                            PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.GetComponent<ManaCubeBehavior>().AddMana(currentMana);
                        }
                        LoseMana();
                    }
                    else
                    {
                        audioSource.PlayOneShot(noManaAction);
                    }
                }

                if (AllowedToDoPlantAction())
                {
                    if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.GetComponent<PlantStates>().lostMana)
                    {
                        audioSource.PlayOneShot(noManaAction);
                    }
                    else if (currentMana < fullValue)
                    {
                        audioSource.PlayOneShot(storeManaSound);
                        currentMana = PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.GetComponent<ManaPlantBehavior>().GiveMana(currentMana);
                        PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.GetComponent<PlantStates>().hasMana = false;
                        losingManaTimer = 15;
                    }
                    else
                    {
                        audioSource.PlayOneShot(noManaAction);
                    }
                }
                rightMouseButtonLock = true;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            rightMouseButtonLock = false;
        }
    }


    private bool AllowedToDoCubeAction()
    {
        if (PlayerInteract.instance.allowedTointeract)
        {
            if (PlayerInteract.instance.interactObject != null)
            {
                if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot != null)
                {
                    if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.CompareTag("ManaStorage"))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    private bool AllowedToDoPlantAction()
    {
        if (PlayerInteract.instance.allowedTointeract)
        {
            if (PlayerInteract.instance.interactObject != null)
            {
                if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot != null)
                {
                    if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.CompareTag("PlantMana"))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    private void LoseMana()
    {
        if (currentMana >= 5)
        {
            currentMana -= 5;
        }
        else if(currentMana > 0 && currentMana < 5)
        {
            currentMana = 0;
        }           
        else
        {
            audioSource.PlayOneShot(noManaAction);
        }
    }
}
