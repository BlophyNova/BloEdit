using Blophy.ChartEdit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEdit : NoteEdit
{
    public override NoteEdit Init(BeatLine beatLine, VLine vline, Public_LineDiv public_LineDiv, List<Blophy.ChartEdit.Note> editing)
    {
        base.Init(beatLine, vline, public_LineDiv, editing);
        thisNote.noteType = Blophy.Chart.NoteType.Tap;
        EventsEdit_Edit.Instance.UpdateEditingInfo(this, true);
        return this;
    }
}
