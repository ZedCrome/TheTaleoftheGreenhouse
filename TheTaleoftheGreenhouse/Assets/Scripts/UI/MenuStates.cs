using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuStates : MonoBehaviour
{

    public void StartGame()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
        SceneManager.LoadScene("MainScene");
    }

    
    public void LeavePauseMenu()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
    }

    
    public void ExitGame()
    {
        Application.Quit();
    }

}
