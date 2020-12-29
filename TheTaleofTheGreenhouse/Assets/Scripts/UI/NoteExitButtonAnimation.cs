using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteExitButtonAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject exitButton;
    public float scaleOffset;
    private Vector3 startScale;
    private bool loopLock;

    private void Start()
    {
        startScale = exitButton.transform.localScale;
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
        while (loopLock == false)
        {
            LeanTween.scale(exitButton, new Vector3(startScale.x - scaleOffset, startScale.y - scaleOffset, startScale.z), 1).setEaseInOutSine();
            yield return new WaitForSeconds(1);
            LeanTween.scale(exitButton, new Vector3(startScale.x, startScale.y, startScale.z), 1).setEaseInOutSine();
            yield return new WaitForSeconds(1);
        }
    }
}
