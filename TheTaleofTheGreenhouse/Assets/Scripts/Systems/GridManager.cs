using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GridLayout gridLayout;
    public Vector3Int cellPosition;
    void Start()
    {
        //gridLayout = GetComponent<GridLayout>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

        cellPosition = gridLayout.WorldToCell(Camera.main.ScreenToWorldPoint(mousePosition));
    }
}
