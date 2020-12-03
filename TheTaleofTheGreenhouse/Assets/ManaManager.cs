using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{  
    public GameObject unlitRunes;
    public GameObject firstLitRune;
    public GameObject seccondLitRune;
    public GameObject finalLitRune;

    private float firstRuneValue = 0.27f;
    private float seccondRuneValue = 0.63f;
    private float finalRuneValue = 0.98f;
    enum RuneState { Empty, FirstRune, SeccondRune, AllRunes}
    RuneState currentRuneState;

    public float currentMana = 0;

    public Slider knifeSlider;

    private ManaCubeBehavior[] manaCubes;
    public int numberOfCubes;

    private void Start()
    {
        knifeSlider = knifeSlider.GetComponent<Slider>();
        knifeSlider.value = 0;

        numberOfCubes = GameObject.FindGameObjectsWithTag("ManaStorage").Length;
        manaCubes = new ManaCubeBehavior[9];
    }


    private void Update()
    {
        //Just testing a math values now to tweek later
        knifeSlider.value = currentMana / 70;

        switch(currentRuneState)
        {
            case RuneState.Empty:
                {
                    firstLitRune.SetActive(false);
                    seccondLitRune.SetActive(false);
                    finalLitRune.SetActive(false);
                    break;
                }
            case RuneState.FirstRune:
                {
                    firstLitRune.SetActive(true);
                    seccondLitRune.SetActive(false);
                    finalLitRune.SetActive(false);
                    break;
                }
            case RuneState.SeccondRune:
                {
                    firstLitRune.SetActive(true);
                    seccondLitRune.SetActive(true);
                    finalLitRune.SetActive(false);
                    break;
                }
            case RuneState.AllRunes:
                {
                    firstLitRune.SetActive(true);
                    seccondLitRune.SetActive(true);
                    finalLitRune.SetActive(true);
                    break;
                }
        }

        if (knifeSlider.value >= firstRuneValue && knifeSlider.value < seccondRuneValue)
        {
            currentRuneState = RuneState.FirstRune;
        }
        else if (knifeSlider.value >= seccondRuneValue && knifeSlider.value < finalRuneValue)
        {
            currentRuneState = RuneState.SeccondRune;
        }
        else if (knifeSlider.value >= finalRuneValue)
        {
            currentRuneState = RuneState.AllRunes;
        }
        else
        {
            currentRuneState = RuneState.Empty;
        }
    }


    public void AddCube()
    {
        numberOfCubes++;
    }


    public void AskToExtractMana()
    {
        //Make a ask panel
        //You wanna extract mana from all cubes? YES / NO ?
        //No go back
        //Yes do ExtractCubeMana();
    }

    public void ExtractCubeMana()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            currentMana += manaCubes[i].storedMana;
        }
    }
}
