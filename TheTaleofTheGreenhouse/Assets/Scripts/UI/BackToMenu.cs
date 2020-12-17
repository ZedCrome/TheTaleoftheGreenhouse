using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void LoadMenuScene()
    {
        GameManager.instance.currentGameState = GameManager.GameState.Menu;
        SceneManager.LoadScene("MainMenuScene");
    }
}
