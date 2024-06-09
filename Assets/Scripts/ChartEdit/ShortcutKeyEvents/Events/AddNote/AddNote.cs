using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNote : ShortcutKeyEvent
{
    public NoteEdit thisNote;
    public override void ExeDown()
    {
        Debug.Log("ExeAddNote");
        BeatLine nearestBeatLine = null;//最近的节拍线
        VLine nearestVerticalLine = null;//最近的垂直线
        Public_LineDiv public_Linediv = null;
        //float currentNearestDistance = 10000;
        float currentNearestBeatLine = 10000;
        float currentNearestVerticalLine = 1000;
        for (int i = 0; i < ChartPreviewEdit.Instance.noteLines.Count; i++)
        {
            if (!ChartPreviewEdit.Instance.noteLines[i].gameObject.activeInHierarchy) continue;
            foreach (BeatLine beatLine in ChartPreviewEdit.Instance.noteLines[i].beatLines)
            {
                //Input.mousePosition-beatLine.transform.position
                RectTransformUtility.ScreenPointToWorldPointInRectangle(ShortcutKeyEvents.Instance.canvas.transform as RectTransform, Input.mousePosition, null, out Vector3 beatLine_worldPosition);
                Vector3 beatLine_distance = beatLine_worldPosition - beatLine.transform.position;
                if (beatLine_distance.magnitude < currentNearestBeatLine)
                {
                    nearestBeatLine = beatLine;
                    currentNearestBeatLine = beatLine_distance.magnitude;
                    public_Linediv = ChartPreviewEdit.Instance.noteLines[i];
                }
            }



            foreach (VLines vLine in VerticalLine.Instance.vLines)
            {
                if (!vLine.gameObject.activeInHierarchy) continue;
                RectTransformUtility.ScreenPointToWorldPointInRectangle(ShortcutKeyEvents.Instance.canvas.transform as RectTransform, Input.mousePosition, null, out Vector3 verticalRightLine_worldPosition);
                Vector3 verticalRightLine_distance = verticalRightLine_worldPosition - vLine.edgeRightVerticalLine.transform.position;
                if (verticalRightLine_distance.magnitude < currentNearestVerticalLine)
                {
                    nearestVerticalLine = vLine.edgeRightVerticalLine;
                    currentNearestVerticalLine = verticalRightLine_distance.magnitude;
                }


                RectTransformUtility.ScreenPointToWorldPointInRectangle(ShortcutKeyEvents.Instance.canvas.transform as RectTransform, Input.mousePosition, null, out Vector3 verticalLeftLine_worldPosition);
                Vector3 verticalLeftLine_distance = verticalLeftLine_worldPosition - vLine.edgeLeftVerticalLine.transform.position;
                if (verticalLeftLine_distance.magnitude < currentNearestVerticalLine)
                {
                    nearestVerticalLine = vLine.edgeLeftVerticalLine;
                    currentNearestVerticalLine = verticalLeftLine_distance.magnitude;
                }

                foreach (VLine middleLine in vLine.middleLines)
                {
                    RectTransformUtility.ScreenPointToWorldPointInRectangle(ShortcutKeyEvents.Instance.canvas.transform as RectTransform, Input.mousePosition, null, out Vector3 verticalMiddleLine_worldPosition);
                    Vector3 verticalMiddleLine_distance = verticalMiddleLine_worldPosition - middleLine.transform.position;
                    if (verticalMiddleLine_distance.magnitude < currentNearestVerticalLine)
                    {
                        nearestVerticalLine = middleLine;
                        currentNearestVerticalLine = verticalMiddleLine_distance.magnitude;
                    }
                }
            }
        }
        List<Blophy.ChartEdit.Note> onlineNotes = Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[int.Parse(LineNumber.Instance.thisText.text) - 1].onlineNotes;

        NoteEdit instNote = Instantiate(thisNote, Vector2.zero, Quaternion.identity, public_Linediv.notesCanvas.transform)
            .Init(nearestBeatLine, nearestVerticalLine, public_Linediv, onlineNotes);
        //nearestBeatLine.note.Add(instNote);
        int index_noteEdits = Algorithm.BinarySearch(public_Linediv.noteEdits, m => m.thisNote.hitTime.thisStartBPM < instNote.thisNote.hitTime.thisStartBPM, false);
        public_Linediv.noteEdits.Insert(index_noteEdits, instNote);


        int index_onlineNotes = Algorithm.BinarySearch(onlineNotes, m => m.hitTime.thisStartBPM < instNote.thisNote.hitTime.thisStartBPM, false);
        onlineNotes.Insert(index_onlineNotes, instNote.thisNote);

        ChartTools.EditNote2ChartDataNote(
            Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[int.Parse(LineNumber.Instance.thisText.text) - 1],
            Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[int.Parse(LineNumber.Instance.thisText.text) - 1].onlineNotes);

        Chart.Instance.RefreshPlayer();
    }
}
