using UnityEngine;

public class InteractableEffect : MonoBehaviour
{
    public bool useShader = false;

    public Material outlineActive;
    public Material outliveNotActive;
    public Material manaOutlineActive;
    public Material manaNoOutlineDead;
    
    private SpriteRenderer renderer;
    
    private Color mouseOverColor;
    private Color standardColor;

    private bool isEnable;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        if (useShader == false)
        {
            mouseOverColor = Color.green;
            standardColor = renderer.material.color;
        }
    }

    
    public void Enable(bool isEnabled)
    {
        if (isEnabled)
        {
            if (useShader)
            {
                if (outlineActive != null)
                {
                    if (gameObject.tag == "PlantMana" && gameObject.GetComponent<PlantStates>().lostMana == false)
                    {
                        renderer.material = manaOutlineActive;
                    }
                    else
                    {
                        renderer.material = outlineActive;
                    }
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
                if (gameObject.CompareTag("PlantMana") && gameObject.GetComponent<PlantStates>().currentState == PlantStates.PlantState.Dead)
                {
                    renderer.material = manaNoOutlineDead;
                }
                else if (outliveNotActive != null)
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

        isEnable = isEnabled;
    }

    public bool ActiveOutline()
    {
        return isEnable;
    }
}
