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

    public Sprite emptyCatcher;
    public Sprite firstStageCatcher;
    public Sprite fullStageCatcher;

    private bool rightMouseButtonLock = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        manaCubeBehavior = new ManaCubeBehavior();
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
                    if (PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.GetComponent<ManaCubeBehavior>().GetMana() == manaCubeBehavior.maxMana)
                    {
                        //audioSource.PlayOneShot(alreadyFull);
                    }
                    else
                    {
                        //audioSource.PlayOneShot(storeManaSound);
                        PlayerInteract.instance.interactObject.GetComponent<ObjectSlot>().objectInSlot.GetComponent<ManaCubeBehavior>().AddMana(currentMana);
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
}
