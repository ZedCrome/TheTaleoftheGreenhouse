using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCatcherBehavior : MonoBehaviour
{
    public AudioClip storeManaSound;
    public AudioClip alreadyFull;

    private AudioSource audioSource;
    private ManaCubeBehavior manaCubeBehavior;

    public int currentMana;

    private bool rightMouseButtonLock = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PlayerState.instance.currentHandState == PlayerState.HandState.ManaCatcher)
        {
            if (Input.GetMouseButtonDown(1) && rightMouseButtonLock == false)
            {
                if (AllowedToDoAction())
                {
                    //Make a new cool Action to be able to move around mana
                    if (PlayerInteract.instance.interactObject.transform.GetComponent<ManaCubeBehavior>().GetMana() == manaCubeBehavior.maxMana)
                    {
                        Debug.Log("Dont add mana cuz the cube is full");
                        audioSource.PlayOneShot(alreadyFull);
                    }
                    else
                    {
                        Debug.Log("Trying ti add mana to mana cube");
                        audioSource.PlayOneShot(storeManaSound);
                        PlayerInteract.instance.interactObject.transform.GetComponent<ManaCubeBehavior>().AddMana(currentMana);
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
    private bool AllowedToDoAction()
    {
        if (PlayerInteract.instance.allowedTointeract)
        {
            if (PlayerInteract.instance.interactObject != null)
            {
                if (PlayerInteract.instance.interactObject.CompareTag("ManaStorage"))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
