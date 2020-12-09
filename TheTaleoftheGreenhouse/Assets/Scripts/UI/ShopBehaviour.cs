using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject buyMenu;
    [SerializeField] private RectTransform buyContent;
    [SerializeField] private RectTransform buyContentBackground;
    public void Update()
    {
        if (buyMenu.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                buyMenu.SetActive(false);
                GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
            }
        }
    }
    
    
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            buyMenu.SetActive(true);
            GameManager.instance.ChangeGameState(GameManager.GameState.ShopMenu);
            LeanTween.scale(buyContent, new Vector3(1, 1, 1), 0.25f).setOnComplete(TweenBuyContentBackGroundFadeIn);
        }
    }
    
    
    public void exit()
    {
        LeanTween.alpha(buyContentBackground, 0f, 0.25f).setOnComplete(TweenBuyContentFadeOut);
    }

    
    void closeShop()
    {
        buyMenu.SetActive(false);
        GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
    }

    
    private void TweenBuyContentBackGroundFadeIn()
    {
        LeanTween.alpha(buyContentBackground, 0.8f, 0.5f);
    }
    private void TweenBuyContentFadeOut()
    {
        LeanTween.scale(buyContent, new Vector3(0f, 0f, 0f), 0.25f).setOnComplete(closeShop);
    }

    
    private void OnMouseEnter()
    {
        ChangeMouseCursor.instance.inputObjectTag = gameObject.tag;
    }

    
    private void OnMouseExit()
    {
        ChangeMouseCursor.instance.inputObjectTag = "Default";
    }
}
