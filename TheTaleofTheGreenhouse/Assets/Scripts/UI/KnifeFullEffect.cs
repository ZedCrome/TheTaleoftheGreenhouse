using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class KnifeFullEffect : MonoBehaviour
{
    [SerializeField] private GameObject knifeUI;
    [SerializeField] private Slider manaSlider;

    [SerializeField] private GameObject askPanel;
    [SerializeField] private GameObject askPanelSister;
    
    private bool mDoEffect;

    private Vector3 originalSize;

    public bool doEffect
    {
        get { return mDoEffect; }
        set
        {
            if (doEffect != value)
            {
                mDoEffect = value;
                
                if (value == true)
                {
                    StartCoroutine(KnifePopEffect());
                    Debug.Log("Set True");
                }
                else
                {
                    StopCoroutine(KnifePopEffect());
                    knifeUI.transform.localScale = originalSize;
                    Debug.Log("Set False");
                }
            }
        }
    }

    private void Start()
    {
        originalSize = knifeUI.transform.localScale;
    }

    private void Update()
    {
        if (askPanel.activeSelf || askPanelSister.activeSelf)
        {
            doEffect = false;
        }
        else if (manaSlider.value >= 1)
        {
            doEffect = true;
        }
        else if(manaSlider.value < 1)
        {
            doEffect = false;
        }
    }
    
    private IEnumerator KnifePopEffect()
    {
        while (doEffect)
        {
            LeanTween.scale(knifeUI, new Vector3(.9f, .9f, .9f), 1).setEaseInOutSine();
            yield return new WaitForSeconds(1);
            LeanTween.scale(knifeUI, new Vector3(0.8481499f, 0.8481499f, 0.8481499f), 1).setEaseInOutSine();
            yield return new WaitForSeconds(1);
        }
    }
}
