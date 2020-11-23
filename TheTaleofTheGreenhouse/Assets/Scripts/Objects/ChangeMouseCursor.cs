using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMouseCursor : MonoBehaviour
{
    public static ChangeMouseCursor instance;

    public Texture2D defaultTexture;
    public Texture2D pickupTexture;
    public Texture2D watertTexture;
    public Texture2D manaTexture;
    public Texture2D placementTexture;
    public Texture2D compostTexture;
    public Texture2D shopTexture;

    public CursorMode cursMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private string _objectTag;
    public string objectTag
    {
        get { return _objectTag; }
        set 
        { 
            if(value == "ManaStorage")
            {
                Cursor.SetCursor(manaTexture, hotSpot, cursMode);
            }
            if (value == "Default")
            {
                Cursor.SetCursor(defaultTexture, hotSpot, cursMode);
            }
            _objectTag = value;
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
