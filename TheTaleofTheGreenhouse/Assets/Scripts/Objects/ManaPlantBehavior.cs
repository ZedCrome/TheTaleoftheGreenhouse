using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPlantBehavior : MonoBehaviour
{
    private int giveMana = 5;
    public int GiveMana(int mana)
    {
        return mana + giveMana;
    }

}
