using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject buyMenu;

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
            LeanTween.scale(buyMenu, new Vector3(1, 1, 1), 0f).setOnComplete(activateShop);
        }
    }
    

    void activateShop()
    {
        buyMenu.SetActive(true);
        GameManager.instance.ChangeGameState(GameManager.GameState.ShopMenu);
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
