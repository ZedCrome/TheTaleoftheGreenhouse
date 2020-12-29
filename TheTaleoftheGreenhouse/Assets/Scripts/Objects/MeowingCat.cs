using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.UIElements;
using UnityEngine;

public class MeowingCat : MonoBehaviour
{
    public AudioSource catAudioSource;
    public AudioClip[] catMeows;

    [SerializeField] private GameObject BuyMenu;
    [SerializeField] private GameObject NotesMenu;
    [SerializeField] private GameObject TaskMenu;
       
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!NotesMenu.activeInHierarchy && !BuyMenu.activeInHierarchy && !TaskMenu.activeInHierarchy)
            {
                catAudioSource.PlayOneShot(Tools.GetRandomSound(catMeows));
            }
        }
    }
}
