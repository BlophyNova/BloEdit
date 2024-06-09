using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : ShortcutKeyEvent
{
    public override void ExeDown()
    {
        if (EventsEdit_Edit.Instance.edit_NoteEdit.gameObject.activeInHierarchy)
        {
            NoteEdit currentNoteEdit = EventsEdit_Edit.Instance.edit_NoteEdit.noteEdit;
            if (currentNoteEdit == null) return;
            EventsEdit_Edit.Instance.edit_NoteEdit.noteEdit.public_LineDiv.noteEdits.Remove(currentNoteEdit);

            currentNoteEdit.editing.Remove(currentNoteEdit.thisNote);
            EventsEdit_Edit.Instance.edit_NoteEdit.ResetAllValue();
            Destroy(currentNoteEdit.gameObject);
            ChartTools.EditNote2ChartDataNote(
    Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[int.Parse(LineNumber.Instance.thisText.text) - 1],
    Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[int.Parse(LineNumber.Instance.thisText.text) - 1].onlineNotes);

            Chart.Instance.RefreshPlayer();
            return;
        }
        if (EventsEdit_Edit.Instance.edit_EventEdit.gameObject.activeInHierarchy)
        {
            EventEdit currentEventEdit = EventsEdit_Edit.Instance.edit_EventEdit.EventEdit;
            if (currentEventEdit == null) return;

            EventsEdit_Edit.Instance.edit_EventEdit.EventEdit.eventVLine.eventsEditList.Remove(currentEventEdit);
            //eventVLine..Remove(currentEventEdit);

            currentEventEdit.editing.Remove(currentEventEdit.thisEvent);
            EventsEdit_Edit.Instance.edit_EventEdit.ResetAllValue();
            currentEventEdit.eventVLine.AddEventEdit2ChartDataEvent(true);
            Destroy(currentEventEdit.gameObject);

            Chart.Instance.Refresh();
            return;
        }
    }
}
