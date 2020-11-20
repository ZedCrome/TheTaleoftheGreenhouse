using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuStates : MonoBehaviour
{

     [SerializeField] Canvas canvas;
     
    public void GameLoopScene()
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
