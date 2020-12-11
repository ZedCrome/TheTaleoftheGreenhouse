using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuStates : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
        SceneManager.LoadScene("IntroCinematic");
    }

    
    public void LeavePauseMenu()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
    }


    public void OptionsMenu()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.Options);
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
        Screen.fullScreen = !Screen.fullScreen;
    }
    
    public void MasterVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }
}
