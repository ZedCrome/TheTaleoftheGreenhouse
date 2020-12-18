using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class MeowingCat : MonoBehaviour
{
    public AudioSource catAudioSource;

    public AudioClip[] catMeows;
       
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            catAudioSource.PlayOneShot(Tools.GetRandomSound(catMeows));
        }
    }
}
