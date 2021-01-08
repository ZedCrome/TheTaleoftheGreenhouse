using System;
using UnityEngine;

public class ChangeMouseCursor : MonoBehaviour
{
    public static ChangeMouseCursor instance;

    public Texture2D defaultTexture;
    public Texture2D defaultTextureTransparent;
    public Texture2D pickupTexture;
    public Texture2D pickupTextureTransparent;
    public Texture2D waterTexture;
    public Texture2D waterTextureTransparent;
    public Texture2D manaTexture;
    public Texture2D manaTextureTransparent;
    public Texture2D placementTexture;
    public Texture2D placementTextureTransparent;
    public Texture2D compostTexture;
    public Texture2D compostTextureTransparent;
    public Texture2D shopTexture;
    public Texture2D shopTextureTransparent;
    public Texture2D bedTexture;
    public Texture2D bedTextureTransparent;


    public CursorMode cursMode;
    public Vector2 hotSpot = Vector2.zero;

    private string objectTag;
    private string lastObjectTag;
    private bool lastCanInteract;
    private bool canInteract;
    
    public string inputObjectTag
    {
        get { return objectTag; }
        set 
        {
            // if (value == "Default")
            // {
            //     Cursor.SetCursor(defaultTexture, hotSpot, cursMode);
            // }
            // if (value == "PickUpable")
            // {
            //     Cursor.SetCursor(pickupTexture, hotSpot, cursMode);
            // }
            // if (value == "WaterCan")
            // {
            //     Cursor.SetCursor(waterTexture, hotSpot, cursMode);
            // }
            // if (value == "ManaStorage")
            // {
            //     Cursor.SetCursor(manaTexture, hotSpot, cursMode);
            // }
            // if (value == "Placeable")
            // {
            //     Cursor.SetCursor(placementTexture, hotSpot, cursMode);
            // }
            // if (value == "Compost")
            // {
            //     Cursor.SetCursor(compostTexture, hotSpot, cursMode);
            // }
            // if (value == "Shop")
            // {
            //     Cursor.SetCursor(shopTexture, hotSpot, cursMode);
            // }
            // if (value == "Bed")
            // {
            //     Cursor.SetCursor(bedTexture, hotSpot, cursMode);
            // }
            
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
        #if UNITY_STANDALONE_OSX
            cursMode = CursorMode.ForceSoftware;
        #endif
        
        #if UNITY_STANDALONE_WIN
            cursMode= CursorMode.Auto;
        #endif
        
        Cursor.SetCursor(defaultTexture, hotSpot, cursMode);
    }

    private void Update()
    {
        if (lastCanInteract != canInteract || lastObjectTag != objectTag)
        {
            if (objectTag == "Default")
            {
                if (canInteract)
                {
                    Cursor.SetCursor(defaultTexture, hotSpot, cursMode);
                }
                else
                {
                    Cursor.SetCursor(defaultTextureTransparent, hotSpot, cursMode);
                }
            }
            if (objectTag == "PickUpable")
            {
                if (canInteract)
                {
                    Cursor.SetCursor(pickupTexture, hotSpot, cursMode);
                }
                else
                {
                    Cursor.SetCursor(pickupTextureTransparent, hotSpot, cursMode);
                }
            }
            if (objectTag == "WaterCan")
            {
                if (canInteract)
                {
                    Cursor.SetCursor(waterTexture, hotSpot, cursMode);
                }
                else
                {
                    Cursor.SetCursor(waterTextureTransparent, hotSpot, cursMode);
                }
            }
            if (objectTag == "ManaStorage")
            {
                if (canInteract)
                {
                    Cursor.SetCursor(manaTexture, hotSpot, cursMode);
                }
                else
                {
                    Cursor.SetCursor(manaTextureTransparent, hotSpot, cursMode);
                }
            }
            if (objectTag == "Placeable")
            {
                if (canInteract)
                {
                    Cursor.SetCursor(placementTexture, hotSpot, cursMode);
                }
                else
                {
                    Cursor.SetCursor(placementTextureTransparent, hotSpot, cursMode);
                }
            }
            if (objectTag == "Compost")
            {
                if (canInteract)
                {
                    Cursor.SetCursor(compostTexture, hotSpot, cursMode);
                }
                else
                {
                    Cursor.SetCursor(compostTextureTransparent, hotSpot, cursMode);
                }
            }
            if (objectTag == "Shop")
            {
                if (canInteract)
                {
                    Cursor.SetCursor(shopTexture, hotSpot, cursMode);
                }
                else
                {
                    Cursor.SetCursor(shopTextureTransparent, hotSpot, cursMode);
                }
            }
            if (objectTag == "Bed")
            {
                if (canInteract)
                {
                    Cursor.SetCursor(bedTexture, hotSpot, cursMode);
                }
                else
                {
                    Cursor.SetCursor(bedTextureTransparent, hotSpot, cursMode);
                }
            }
        
            lastCanInteract = canInteract;
            lastObjectTag = objectTag;
        }
    }

    public void ChangeAlpha(bool setCanInteract)
    {
        canInteract = setCanInteract;
    }
}
