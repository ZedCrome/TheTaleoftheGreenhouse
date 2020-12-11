using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class ExitCutscene : MonoBehaviour
{
    public TimelineAsset timeLine;
    public double currentTime;
    public double timeLineLength;
    
    private void Start()
    {
        timeLineLength = timeLine.duration;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > timeLineLength)
        {
            GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);

            Scene currentScene = SceneManager.GetActiveScene();
            Debug.Log(currentScene.name);
            if (currentScene.name == "IntroCinematic")
            {
                SceneManager.LoadScene("MainScene");
            }
            else
            {
                SceneManager.LoadScene("MainMenuScene");
            }
        }
    }
}
