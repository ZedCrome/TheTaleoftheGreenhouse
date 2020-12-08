using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackIndex : MonoBehaviour
{
    [SerializeField]
    private int indexNumber;

    public int IndexNumber
    {
        get { return indexNumber; }
        set { indexNumber = value; }
    }
}
