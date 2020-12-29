using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteExitButtonAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        throw new NotImplementedException();
    }

    private IEnumerator AnimateButton()
    {
        while (exitInfo == false)
        {
            LeanTween.scale(beginnerInfo, new Vector3(1.05f, 1.05f, 1.05f), 1).setEaseInOutSine();
            yield return new WaitForSeconds(1);
            LeanTween.scale(beginnerInfo, new Vector3(1f, 1f, 1f), 1).setEaseInOutSine();
            yield return new WaitForSeconds(1);
        }
    }
}
