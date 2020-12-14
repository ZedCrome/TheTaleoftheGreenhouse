using System.Security.AccessControl;
using UnityEngine;

public class ManaCatcherBehavior : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    InteractableEffect interactableEffect;

    public AudioClip storeManaSound;
    public AudioClip noManaAction;

    private AudioSource audioSource;
    private ManaCubeBehavior manaCubeBehavior;

    public Sprite emptyCatcher;
    public Sprite firstStageCatcher;
    public Sprite fullStageCatcher;

    public Material firstStageMaterial;
    public Material fullStageMaterial;
    private Material emptyMaterial;

    private int emptyValue = 0;
    private int fullValue = 10;

    public int currentMana;
    private int giveMana = 5;

    private bool rightMouseButtonLock = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactableEffect = GetComponent<InteractableEffect>();
        emptyMaterial = spriteRenderer.material;
        manaCubeBehavior = new ManaCubeBehavior();
    }

    private void Update()
    {
        if (currentMana > emptyValue && currentMana < fullValue)
        {
            spriteRenderer.sprite = firstStageCatcher;
            interactableEffect.outliveNotActive = firstStageMaterial;
        }

        else if (currentMana >= fullValue)
        {
            spriteRenderer.sprite = fullStageCatcher;
            interactableEffect.outliveNotActive = fullStageMaterial;
        }

        else
        {
            spriteRenderer.sprite = emptyCatcher;
            interactableEffect.outliveNotActive = emptyMaterial;
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
                        PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.GetComponent<ManaCubeBehavior>().AddMana(giveMana);
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

    //Only works on ManaStorage right now.
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
        else
        {
            audioSource.PlayOneShot(noManaAction);
        }
    }


}
