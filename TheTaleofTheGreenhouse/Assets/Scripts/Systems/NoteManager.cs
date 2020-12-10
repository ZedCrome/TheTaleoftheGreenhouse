using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    [SerializeField] GameObject exitNoteButton;

    public enum NoteStates { PlantNote, CompostNote, ManaNote, SellBasketNote, KnifeNote, DeliveryNote, ShearsNote, None }
    public NoteStates currentNote;

    public GameObject visibleNote;

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
    }

    public void Update()
    {
        switch(currentNote)
        {
            case NoteStates.None:
                exitNoteButton.SetActive(false);
                visibleNote.SetActive(false);
                break;

            case NoteStates.PlantNote:
                visibleNote = plantNote;
                exitNoteButton.SetActive(true);
                visibleNote.SetActive(true);
                break;

            case NoteStates.ManaNote:
                visibleNote = manaNote;
                exitNoteButton.SetActive(true);
                visibleNote.SetActive(true);
                break;

            case NoteStates.SellBasketNote:
                visibleNote = sellBasketNote;
                exitNoteButton.SetActive(true);
                visibleNote.SetActive(true);
                break;

            case NoteStates.KnifeNote:
                visibleNote = knifeNote;
                exitNoteButton.SetActive(true);
                visibleNote.SetActive(true);
                break;

            case NoteStates.CompostNote:
                visibleNote = compostNote;
                exitNoteButton.SetActive(true);
                visibleNote.SetActive(true); 
                break;

            case NoteStates.DeliveryNote:
                visibleNote = deliveryNote;
                exitNoteButton.SetActive(true);
                visibleNote.SetActive(true);
                break;

            case NoteStates.ShearsNote:
                visibleNote = shearsNote;
                exitNoteButton.SetActive(true);
                visibleNote.SetActive(true);
                break;
        }
    }

    public void ActivateNote(NoteStates noteState)
    {
        exitNoteButton.SetActive(false);
        visibleNote.SetActive(false);
        currentNote = noteState;
    }


    public void ExitNote()
    {
        ActivateNote(NoteStates.None);
    }
}
