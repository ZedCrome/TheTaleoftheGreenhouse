using UnityEngine;

public class LeanTweenAnimateBuyMenu : MonoBehaviour
{
    public void exit()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f).setOnComplete(closeShop);
    }

    void closeShop()
    {
        gameObject.SetActive(false);
        GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
    }
}
