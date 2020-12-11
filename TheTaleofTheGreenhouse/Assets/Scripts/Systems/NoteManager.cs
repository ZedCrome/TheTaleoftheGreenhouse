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
                    LeanTween.moveY(visibleNote, 350f, 1f);
                    LeanTween.moveY(exitNoteButton, 180f, 1f);
                    break;

                case NoteStates.ManaNote:
                    activeNote = true;
                    visibleNote = manaNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    LeanTween.moveY(visibleNote, 350f, 1f);
                    LeanTween.moveY(exitNoteButton, 180f, 1f);
                    break;

                case NoteStates.SellBasketNote:
                    activeNote = true;
                    visibleNote = sellBasketNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    LeanTween.moveY(visibleNote, 350f, 1f);
                    LeanTween.moveY(exitNoteButton, 180f, 1f);
                    break;

                case NoteStates.KnifeNote:
                    activeNote = true;
                    visibleNote = knifeNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    LeanTween.moveY(visibleNote, 350f, 1f);
                    LeanTween.moveY(exitNoteButton, 180f, 1f);
                    break;

                case NoteStates.CompostNote:
                    activeNote = true;
                    visibleNote = compostNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    LeanTween.moveY(visibleNote, 350f, 1f);
                    LeanTween.moveY(exitNoteButton, 180f, 1f);
                    break;

                case NoteStates.DeliveryNote:
                    activeNote = true;
                    visibleNote = deliveryNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    LeanTween.moveY(visibleNote, 350f, 1f);
                    LeanTween.moveY(exitNoteButton, 180f, 1f);
                    break;

                case NoteStates.ShearsNote:
                    activeNote = true;
                    visibleNote = shearsNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    LeanTween.moveY(visibleNote, 350f, 1f);
                    LeanTween.moveY(exitNoteButton, 180f, 1f);
                    break;
            }
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
        activeNote = false;
    }


    public void MoveNote()
    {
        LeanTween.moveY(exitNoteButton, -700f, 1f);
        LeanTween.moveY(visibleNote, -1000f, 1f).setOnComplete(ExitNote);
    }
}
