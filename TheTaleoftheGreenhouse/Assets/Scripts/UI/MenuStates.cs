using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuStates : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.Intro);
    }

    
    public void LeavePauseMenu()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
    }


    public void OptionsMenu()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.Options);
    }

    public void ControlsMenu()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.ControlsMenu);
    }


    public void OpenStartMenuScene()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.Menu);
        SceneManager.LoadScene("MainMenuScene");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        GameManager.instance.ChangeGameState(GameManager.instance.previousGameState);
    }


    public void ToggleFullscreen()
    {
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
        }
        
        if (Screen.fullScreen == false)
        {
            Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.ExclusiveFullScreen);
        }
        
    }
    
    public void MasterVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }
}
