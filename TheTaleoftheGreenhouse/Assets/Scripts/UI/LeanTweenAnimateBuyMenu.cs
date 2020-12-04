﻿using UnityEngine;

public class LeanTweenAnimateBuyMenu : MonoBehaviour
{
    public RectTransform buyMenu;
    public void exit()
    {
        
        LeanTween.scale(buyMenu, new Vector3(0, 0, 0), 0.25f).setOnComplete(closeShop);
    }

    void closeShop()
    {
        gameObject.SetActive(false);
        GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
    }
}