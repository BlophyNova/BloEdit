using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsEdit_Edit : MonoBehaviourSingleton<EventsEdit_Edit>
{
    public Edit_NoteEdit edit_NoteEdit;
    public Edit_EventEdit edit_EventEdit;
    public void UpdateEditingInfo(NoteEdit noteEdit, bool changeSelectStateYesOrNo)
    {
        edit_EventEdit.gameObject.SetActive(false);
        edit_NoteEdit.gameObject.SetActive(true);
        edit_NoteEdit.InitThisNoteEdit(noteEdit, changeSelectStateYesOrNo);
    }
    public void UpdateEditingInfo(EventEdit eventEdit, bool changeSelectStateYesOrNo)
    {
        edit_EventEdit.gameObject.SetActive(true);
        edit_NoteEdit.gameObject.SetActive(false);
        edit_EventEdit.InitThisEventEdit(eventEdit, changeSelectStateYesOrNo);
    }
}
