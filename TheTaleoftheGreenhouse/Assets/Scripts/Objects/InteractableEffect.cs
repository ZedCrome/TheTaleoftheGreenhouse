using UnityEngine;

public class InteractableEffect : MonoBehaviour
{
    private SpriteRenderer renderer;
    
    private Color mouseOverColor;
    private Color standardColor;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        
        mouseOverColor = Color.green;
        standardColor = renderer.material.color;
    }

    
    public void Enable(bool isEnabled)
    {
        if (isEnabled)
        {
            renderer.material.color = mouseOverColor;
        }
        else
        {
            renderer.material.color = standardColor;
        }
    }
}
