using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    [Header("SHOP")] [Space(5)]
    [SerializeField] private GameObject buyMenu;
    [SerializeField] private RectTransform buyContent;
    [SerializeField] private RectTransform buyContentBackground;
    bool firstTimeUsing = true;

    [Header("NOTES")] [Space(5)] 
    [SerializeField] private GameObject notesMenu;
    [SerializeField] private RectTransform notesContent;
    [SerializeField] private RectTransform noteContentBackground;

    private DayNightCycle dayNightCycle;

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
            if (firstTimeUsing)
            {
                NoteManager.instance.ActivateNote(NoteManager.NoteStates.DeliveryNote);
                firstTimeUsing = false;
            }
            
            buyMenu.SetActive(true);
            GameManager.instance.ChangeGameState(GameManager.GameState.ShopMenu);
            LeanTween.scale(buyContent, new Vector3(1, 1, 1), 0.5f).setEaseOutBack().setOnComplete(TweenBuyContentBackGroundFadeIn);
            LeanTween.scale(notesContent, new Vector3(1f, 1f, 1f), 0.5f).setEaseInBack();
        }
    }
    
    
    public void ExitShop()
    {
        LeanTween.alpha(buyContentBackground, 0f, 0.15f);
        LeanTween.alpha(noteContentBackground, 0f, 0.15f).setOnComplete(TweenBuyContentFadeOut);
    }

    
    public void ActivateNoteMenu()
    {
        notesMenu.SetActive(true);
        buyMenu.SetActive(false);
    }

    
    public void ActivateShopMenu()
    {
        notesMenu.SetActive(false);
        buyMenu.SetActive(true);
    }
    
    
    private void TweenBuyContentBackGroundFadeIn()
    {
        LeanTween.alpha(buyContentBackground, 0.8f, 0.5f);
        LeanTween.alpha(noteContentBackground, 0.8f, 0.5f);
    }
    
    
    private void TweenBuyContentFadeOut()
    {
        LeanTween.scale(buyContent, new Vector3(0f, 0f, 0f), 0.5f).setEaseInBack();
        LeanTween.scale(notesContent, new Vector3(0f, 0f, 0f), 0.5f).setEaseInBack().setOnComplete(CloseShop);
    }

    
    void CloseShop()
    {
        buyMenu.SetActive(false);
        notesMenu.SetActive(false);
        GameManager.instance.ChangeGameState(GameManager.GameState.GameLoop);
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
