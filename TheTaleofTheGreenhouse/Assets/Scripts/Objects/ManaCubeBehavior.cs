using UnityEngine;

public class ManaCubeBehavior : MonoBehaviour
{
    public AudioClip storeManaSound;
    public AudioClip alreadyFull;

    public int incomingMana;
    private int storedMana = 0;
    private int maxMana = 5;

    //Make sure user have the mana catcher on it.
    //Check how much mana the mana catcher have

    public void AddMana(int mana)
    {
        incomingMana = mana;
        storedMana += incomingMana;
    }


}
