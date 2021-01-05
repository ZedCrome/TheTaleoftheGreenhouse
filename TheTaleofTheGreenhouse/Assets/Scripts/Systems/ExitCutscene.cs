using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class ExitCutscene : MonoBehaviour
{
    public TimelineAsset timeLine;
    public double currentTime;
    public double timeLineLength;

    public GameObject sliderObject;
    private Slider slider;
    private float skipCounter;
    public float skipTime = 2f;
    
    private void Start()
    {
        timeLineLength = timeLine.duration;

        slider = sliderObject.GetComponent<Slider>();

        slider.maxValue = skipTime;
        sliderObject.SetActive(false);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if(Input.anyKey)
        {
            skipCounter += Time.deltaTime;

            if (sliderObject.activeInHierarchy == false)
            {
                sliderObject.SetActive(true);
            }
        }
        else
        {
            skipCounter = 0;
        }

        slider.value = skipCounter;
        
        if (currentTime > timeLineLength || skipCounter >= skipTime)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            Debug.Log(currentScene.name);
            if (currentScene.name == "IntroCinematic")
            {
                GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
                SceneManager.LoadScene("MainScene");
            }
            else
            {
                SceneManager.LoadScene("Credits");
            }
        }
    }
}
