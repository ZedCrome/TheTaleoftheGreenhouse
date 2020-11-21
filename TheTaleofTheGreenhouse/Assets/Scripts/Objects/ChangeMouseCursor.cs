using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMouseCursor : MonoBehaviour
{

    public Texture2D defualtTexture;
    public Texture2D pickupTexture;
    public Texture2D watertTexture;
    public Texture2D manaTexture;
    public Texture2D placementTexture;
    public Texture2D compostTexture;
    public Texture2D shopTexture;

    public CursorMode cursMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(defualtTexture, hotSpot, cursMode);
    }


    private void OnMouseEnter()
    {
        if (gameObject.tag == "ManaStorage")
        {
            Cursor.SetCursor(manaTexture, hotSpot, cursMode);
        }
    }


    private void OnMouseExit()
    {
        Cursor.SetCursor(defualtTexture, hotSpot, cursMode);
    }

}
