using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManaManager : MonoBehaviour
{  
    public GameObject unlitRunes;
    public GameObject firstLitRune;
    public GameObject seccondLitRune;
    public GameObject finalLitRune;

    public GameObject askPanel;
    public GameObject askSaveSisterPanel;

    private float firstRuneValue = 0.27f;
    private float seccondRuneValue = 0.63f;
    private float finalRuneValue = 0.98f;

    public AudioClip extractMana;
    public AudioClip noManaToCollect;
    public AudioClip clickSound;

    private AudioSource audioSource;

    enum RuneState { Empty, FirstRune, SeccondRune, AllRunes}
    RuneState currentRuneState;

    public float currentMana = 0;
    private float collectingMana;

    public Slider knifeSlider;

    private ManaCubeBehavior[] manaCubes;
    public int numberOfCubes;

    private GameObject[] manaCubesFound;

    private bool firstTimeUsing = true;

    private void Start()
    {
        knifeSlider = knifeSlider.GetComponent<Slider>();
        knifeSlider.value = 0;

        audioSource = GetComponent<AudioSource>();

        manaCubes = new ManaCubeBehavior[9];
    }

    private void OnEnable()
    {
        Motherlode.instance.addManaCheat += AddMana;
    }
    
    private void OnDisable()
    {
        Motherlode.instance.addManaCheat -= AddMana;
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
        if (firstTimeUsing)
        {
            NoteManager.instance.ActivateNote(NoteManager.NoteStates.KnifeNote);
            firstTimeUsing = false;
        }

        if (knifeSlider.value >= finalRuneValue)
        {
            askSaveSisterPanel.SetActive(true);
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            askPanel.SetActive(true);
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void ExitAskPanel()
    {
        askPanel.SetActive(false);
        audioSource.PlayOneShot(clickSound);
    }

    public void ExitSaveSisterAskPanel()
    {
        askSaveSisterPanel.SetActive(false);
        audioSource.PlayOneShot(clickSound);
    }

    public void ExtractCubeMana()
    {
        numberOfCubes = GameObject.FindGameObjectsWithTag("ManaStorage").Length;
        manaCubesFound = GameObject.FindGameObjectsWithTag("ManaStorage");

        for (int i = 0; i < numberOfCubes; i++)
        {
            manaCubes[i] = manaCubesFound[i].GetComponent<ManaCubeBehavior>();
            currentMana += manaCubes[i].storedMana;
            collectingMana += manaCubes[i].storedMana; ;
            manaCubesFound[i].GetComponent<ManaCubeBehavior>().storedMana = 0;
        }

        if (collectingMana <= 0)
        {
            
            audioSource.PlayOneShot(noManaToCollect);
        }
        else
        {
            audioSource.PlayOneShot(extractMana);
        }
        collectingMana = 0;
        askPanel.SetActive(false);
    }

    public void AddMana(float newMana)
    {
        currentMana += newMana;
        Debug.Log("Added mana: " + newMana + " | CurrentMana: " + currentMana);
    }
    
    public void SaveSister()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.Victory);
    }
}
