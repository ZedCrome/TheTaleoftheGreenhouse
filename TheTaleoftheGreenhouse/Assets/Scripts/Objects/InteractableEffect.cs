using UnityEngine;

public class InteractableEffect : MonoBehaviour
{
    public bool useShader = false;

    public Material outlineActive;
    public Material outliveNotActive;
    
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
            if (useShader)
            {
                if (outlineActive != null)
                {
                    renderer.material = outlineActive;
                }
                else
                {
                    Debug.LogError("Material not set");
                }
            }
            else
            {
                renderer.material.color = mouseOverColor;
            }
        }
        else
        {
            if (useShader)
            {
                if (outliveNotActive != null)
                {
                    renderer.material = outliveNotActive;
                }
                else
                {
                    Debug.LogError("Material not set");
                }
            }
            else
            {
                renderer.material.color = standardColor;
            }
        }
    }
}
