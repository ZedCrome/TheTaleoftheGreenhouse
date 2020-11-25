using UnityEngine;

public class ChangeMouseCursor : MonoBehaviour
{
    public static ChangeMouseCursor instance;

    public Texture2D defaultTexture;
    public Texture2D pickupTexture;
    public Texture2D waterTexture;
    public Texture2D manaTexture;
    public Texture2D placementTexture;
    public Texture2D compostTexture;
    public Texture2D shopTexture;

    public CursorMode cursMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private string objectTag;
    
    public string inputObjectTag
    {
        get { return objectTag; }
        set 
        {
            if (value == "Default")
            {
                Cursor.SetCursor(defaultTexture, hotSpot, cursMode);
            }
            if (value == "PickUpable")
            {
                Cursor.SetCursor(pickupTexture, hotSpot, cursMode);
            }
            if (value == "Waterable")
            {
                Cursor.SetCursor(waterTexture, hotSpot, cursMode);
            }
            if (value == "ManaStorage")
            {
                Cursor.SetCursor(manaTexture, hotSpot, cursMode);
            }
            if (value == "Placeable")
            {
                Cursor.SetCursor(placementTexture, hotSpot, cursMode);
            }
            if (value == "Compost")
            {
                Cursor.SetCursor(compostTexture, hotSpot, cursMode);
            }
            if (value == "Shop")
            {
                Cursor.SetCursor(shopTexture, hotSpot, cursMode);
            }

            objectTag = value;
        }
    }

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    
    void Start()
    {
        Cursor.SetCursor(defaultTexture, hotSpot, cursMode);
    }
}
