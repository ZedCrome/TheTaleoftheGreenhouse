using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{  
    public GameObject unlitRunes;
    public GameObject firstLitRune;
    public GameObject seccondLitRune;
    public GameObject finalLitRune;

    public GameObject askPanel;

    private float firstRuneValue = 0.27f;
    private float seccondRuneValue = 0.63f;
    private float finalRuneValue = 0.98f;
    enum RuneState { Empty, FirstRune, SeccondRune, AllRunes}
    RuneState currentRuneState;

    public float currentMana = 0;

    public Slider knifeSlider;

    private ManaCubeBehavior[] manaCubes;
    public int numberOfCubes;

    private GameObject[] manaCubesFound;

    private void Start()
    {
        knifeSlider = knifeSlider.GetComponent<Slider>();
        knifeSlider.value = 0;

        manaCubes = new ManaCubeBehavior[9];
    }


    private void Update()
    {
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

    public void AskToExtractMana()
    {
        askPanel.SetActive(true);
    }

    public void ExitAskPanel()
    {
        askPanel.SetActive(false);
    }

    public void ExtractCubeMana()
    {
        numberOfCubes = GameObject.FindGameObjectsWithTag("ManaStorage").Length;
        manaCubesFound = GameObject.FindGameObjectsWithTag("ManaStorage");

        for (int i = 0; i < numberOfCubes; i++)
        {
            manaCubes[i] = manaCubesFound[i].GetComponent<ManaCubeBehavior>();
            currentMana += manaCubes[i].storedMana;
            manaCubesFound[i].GetComponent<ManaCubeBehavior>().storedMana = 0;
        }

        askPanel.SetActive(false);
    }
}
