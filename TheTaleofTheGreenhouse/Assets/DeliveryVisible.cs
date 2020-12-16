using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DeliveryVisible : MonoBehaviour
{
    DeliveryManager deliveryManager;

    [SerializeField] GameObject boxRightBack;
    [SerializeField] GameObject boxRightFront;
    [SerializeField] GameObject boxLeftBack;
    [SerializeField] GameObject boxLeftFront;

    private enum NumberOfBoxes { OneBox, TwoBox, ThreeBox, FourBox, None};
    NumberOfBoxes numberOfBoxes;

    private List<ObjectSlot> objectSlot = new List<ObjectSlot>();

    private int numberOfItems;

    //Itemspot 1-2 is in boxRightBack
    //Itemspot 3-6 is in boxRightFront
    //Itemspot 7-8 is in boxLeftBack
    //Itemspot 9-12 is in boxLeftFront

    private void Start()
    {
        deliveryManager = GetComponent<DeliveryManager>();
        foreach (Transform child in transform)
        {
            objectSlot.Add(child.GetComponent<ObjectSlot>());
        }
    }


    private void Update()
    {

        switch (numberOfBoxes)
        {
            case NumberOfBoxes.OneBox:
                boxRightBack.SetActive(true);
                boxRightFront.SetActive(false);
                boxLeftBack.SetActive(false);
                boxLeftFront.SetActive(false);
                break;
            case NumberOfBoxes.TwoBox:
                boxRightBack.SetActive(true);
                boxRightFront.SetActive(true);
                boxLeftBack.SetActive(false);
                boxLeftFront.SetActive(false);
                break;
            case NumberOfBoxes.ThreeBox:
                boxRightBack.SetActive(true);
                boxRightFront.SetActive(true);
                boxLeftBack.SetActive(true);
                boxLeftFront.SetActive(false);
                break;
            case NumberOfBoxes.FourBox:
                boxRightBack.SetActive(true);
                boxRightFront.SetActive(true);
                boxLeftBack.SetActive(true);
                boxLeftFront.SetActive(true);
                break;
            case NumberOfBoxes.None:
                boxRightBack.SetActive(false);
                boxRightFront.SetActive(false);
                boxLeftBack.SetActive(false);
                boxLeftFront.SetActive(false);
                break;
        }
    }


    private void ChangeBoxStates(NumberOfBoxes state)
    {
        numberOfBoxes = state;
    }
}
