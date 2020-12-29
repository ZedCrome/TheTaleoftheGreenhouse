using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteExitButtonAnimation : MonoBehaviour
{
    private GameObject exitButton;

    private void Start()
    {
        exitButton = this.gameObject;
    }

    private void OnEnable()
    {
        StartCoroutine(AnimateButton());
    }

    private void OnDisable()
    {
        StopCoroutine(AnimateButton());
    }

    private IEnumerator AnimateButton()
    {
        while (exitButton.activeSelf == true)
        {
            yield return new WaitForSeconds(1);
        }
    }
}
