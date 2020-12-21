using System;
using System.Collections;
using UnityEngine;

public class removeStartInfo : MonoBehaviour
{
    public GameObject beginnerInfo;
    public bool exitInfo;
    private void Start()
    {
        StartCoroutine(removeInfo());
    }

    public IEnumerator removeInfo()
    {
        while (exitInfo == false)
        {
            LeanTween.scale(beginnerInfo, new Vector3(1.05f, 1.05f, 1.05f), 1).setEaseInOutSine();
            yield return new WaitForSeconds(1);
            LeanTween.scale(beginnerInfo, new Vector3(1f, 1f, 1f), 1).setEaseInOutSine();
            yield return new WaitForSeconds(1);
        }
    }


    public void ExitInfo()
    {
        LeanTween.moveX(beginnerInfo, Screen.width * 3f, 3f).setEaseLinear();
    }

}
