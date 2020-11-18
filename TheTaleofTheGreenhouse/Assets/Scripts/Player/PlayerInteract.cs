using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract instance;
    
    public GameObject inventoryItem;
    public GameObject mouseOverItem;
    
    private RaycastHit2D hit;
    
    void Start()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
    }
    void Update()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (Input.GetMouseButtonDown(0))
        {
            if (mouseOverItem != null)
            {
                inventoryItem = mouseOverItem;
                inventoryItem.SetActive(false);
                mouseOverItem = null;
            }
        }
    }

    void FixedUpdate()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        
        if(hit.collider != null && hit.transform.CompareTag("Interactable"))
        {
            Debug.Log(hit.collider.name);

            hit.transform.GetComponent<MouseOver>();
        }
    }
}