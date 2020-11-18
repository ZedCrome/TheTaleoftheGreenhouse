using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPot : MonoBehaviour
{
    //ItemSpot should be based on the object the mouse interacts with
    public TestTableItemSpot itemSpot;

    Vector3 position;

    private void Start()
    {
        position = transform.position;
    }

    void OnMouseDown()
    {
        itemSpot.PlacedItem(TestTableItemSpot.GameItems.Pot);
        Debug.Log("Trying to move object");
        gameObject.transform.parent = itemSpot.transform;
        gameObject.transform.position = itemSpot.transform.position;
    }
}
