using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuStates : MonoBehaviour
{
    
    public void GameLoopScene()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
    }
}
