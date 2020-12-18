using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowingCat : MonoBehaviour
{

    public AudioSource catMeowing;


    private void OnMouseOver()
    {
        if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.Mouse1))
        {
            catMeowing.Play();
        }
    }
}
