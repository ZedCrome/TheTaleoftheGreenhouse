using System.Collections;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;
    
    [SerializeField] GameObject plantNote;
    [SerializeField] GameObject compostNote;
    [SerializeField] GameObject manaNote;
    [SerializeField] GameObject sellBasketNote;
    [SerializeField] GameObject knifeNote;
    [SerializeField] GameObject deliveryNote;
    [SerializeField] GameObject shearsNote;
    [SerializeField] AudioSource noteArrival;
    [SerializeField] AudioSource paperFolding;

    [SerializeField] GameObject exitNoteButton;
    

    private float exitButtonStartPosition;

    public enum NoteStates { PlantNote, CompostNote, ManaNote, SellBasketNote, KnifeNote, DeliveryNote, ShearsNote, None }
    public NoteStates currentNote;

    public GameObject visibleNote;

    private bool activeNote;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        currentNote = NoteStates.None;
        visibleNote = plantNote;
        exitButtonStartPosition = exitNoteButton.transform.position.y;
    }

    
    public void Update()
    {

        if (!activeNote)
        {
            switch (currentNote)
            {
                case NoteStates.None:
                    exitNoteButton.SetActive(false);
                    visibleNote.SetActive(false);
                    break;

                case NoteStates.PlantNote:
                    activeNote = true;
                    visibleNote = plantNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    noteArrival.Play();
                    LeanTween.moveY(visibleNote, Screen.height / 2.1f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/1.5f, 1.5f).setEaseOutBack();
                    break;

                case NoteStates.ManaNote:
                    activeNote = true;
                    visibleNote = manaNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    noteArrival.Play();
                    LeanTween.moveY(visibleNote, Screen.height/2.1f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/1.5f, 1.5f).setEaseOutBack();
                    break;

                case NoteStates.SellBasketNote:
                    activeNote = true;
                    visibleNote = sellBasketNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    noteArrival.Play();
                    LeanTween.moveY(visibleNote, Screen.height/2.1f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/1.5f, 1.5f).setEaseOutBack();
                    break;

                case NoteStates.KnifeNote:
                    activeNote = true;
                    visibleNote = knifeNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    noteArrival.Play();
                    LeanTween.moveY(visibleNote, Screen.height/2.1f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/1.5f, 1.5f).setEaseOutBack();
                    break;

                case NoteStates.CompostNote:
                    activeNote = true;
                    visibleNote = compostNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    noteArrival.Play();
                    LeanTween.moveY(visibleNote, Screen.height/2.1f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/1.5f, 1.5f).setEaseOutBack();
                    break;

                case NoteStates.DeliveryNote:
                    activeNote = true;
                    visibleNote = deliveryNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    noteArrival.Play();
                    LeanTween.moveY(visibleNote, Screen.height/2.1f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/1.5f, 1.5f).setEaseOutBack();
                    break;

                case NoteStates.ShearsNote:
                    activeNote = true;
                    visibleNote = shearsNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    noteArrival.Play();
                    LeanTween.moveY(visibleNote, Screen.height/2.1f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/1.5f, 1.5f).setEaseOutBack();
                    break;
            }
        }      
    }


    public void ActivateNote(NoteStates noteState)
    {
        exitNoteButton.SetActive(false);
        visibleNote.SetActive(false);
        activeNote = false;
        currentNote = noteState;
    }
    
    
    public void ExitNoteTransition()
    {            
        exitNoteButton.SetActive(false);
        LeanTween.moveY(exitNoteButton, -Screen.height/3f, 0.01f);
        LeanTween.scale(visibleNote, new Vector3(0, 0, 0), 0.5f);
        LeanTween.moveY(visibleNote, Screen.height / 2f, 0.5f).setEaseOutQuad();
        LeanTween.moveX(visibleNote, Screen.width/3.8f, 0.5f).setEaseOutQuad().setOnComplete(ExitNote);
    }

    public void ExitNote()
    {
        paperFolding.Play();
        ActivateNote(NoteStates.None);
        activeNote = false;
    }
}
