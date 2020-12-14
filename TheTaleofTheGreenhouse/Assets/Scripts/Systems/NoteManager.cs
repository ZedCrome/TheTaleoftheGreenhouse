using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
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
    [SerializeField] AudioSource noteArrival;

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
                    LeanTween.moveY(visibleNote, Screen.height / 3.75f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/2.35f, 1.5f).setEaseOutBack();
                    break;

                case NoteStates.ManaNote:
                    activeNote = true;
                    visibleNote = manaNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    LeanTween.moveY(visibleNote, Screen.height/3.75f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/2.35f, 1.5f).setEaseOutBack();
                    break;

                case NoteStates.SellBasketNote:
                    activeNote = true;
                    visibleNote = sellBasketNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    LeanTween.moveY(visibleNote, Screen.height/3.75f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/2.35f, 1.5f).setEaseOutBack();
                    break;

                case NoteStates.KnifeNote:
                    activeNote = true;
                    visibleNote = knifeNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    LeanTween.moveY(visibleNote, Screen.height/3.75f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/2.35f, 1.5f).setEaseOutBack();
                    break;

                case NoteStates.CompostNote:
                    activeNote = true;
                    visibleNote = compostNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    LeanTween.moveY(visibleNote, Screen.height/3.75f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/2.35f, 1.5f).setEaseOutBack();
                    break;

                case NoteStates.DeliveryNote:
                    activeNote = true;
                    visibleNote = deliveryNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    LeanTween.moveY(visibleNote, Screen.height/3.75f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/2.35f, 1.5f).setEaseOutBack();
                    break;

                case NoteStates.ShearsNote:
                    activeNote = true;
                    visibleNote = shearsNote;
                    exitNoteButton.SetActive(true);
                    visibleNote.SetActive(true);
                    LeanTween.moveY(visibleNote, Screen.height/3.75f, 1.5f).setEaseOutBack();
                    LeanTween.moveY(exitNoteButton, Screen.height/2.35f, 1.5f).setEaseOutBack();
                    break;
            }
        }      
    }


    public void ActivateNote(NoteStates noteState)
    {
        exitNoteButton.SetActive(false);
        visibleNote.SetActive(false);
        LeanTween.moveY(exitNoteButton, -Screen.height/3f, 0.01f);
        noteArrival.Play();
        activeNote = false;
        currentNote = noteState;
    }

    

    public void ExitNote()
    {            
        ActivateNote(NoteStates.None);
        activeNote = false;
    }


    public void MoveNote()
    {
        LeanTween.moveY(exitNoteButton, -Screen.height/3f, 1f);
        LeanTween.moveY(visibleNote, -Screen.height/2f, 1f).setOnComplete(ExitNote);
    }
}
