using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHold : AddNote
{
    public static AddHold Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<AddHold>();
        }
        else Destroy(this);
    }
    public bool isFirstTime = false;

    public bool waitForPressureAgain = false;



    Public_LineDiv findedPublic_LineDiv = null;
    NoteEdit instNote = null;
    public override void ExeDown()
    {
        Debug.Log("ExeAddHold");
        if (!isFirstTime)
        {
            isFirstTime = true;
            //第一次
            GetNearestBeatLineAndVerticalLine(
            out BeatLine firstBeatLine, out VLine firstVLine, out findedPublic_LineDiv);
            List<Blophy.ChartEdit.Note> onlineNotes = Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[int.Parse(LineNumber.Instance.thisText.text) - 1].onlineNotes;
            instNote = Instantiate(thisNote, Vector2.zero, Quaternion.identity, findedPublic_LineDiv.notesCanvas.transform)
           .Init(firstBeatLine, firstVLine, findedPublic_LineDiv, onlineNotes);
            StartCoroutine(WaitForPressureAgain(firstBeatLine, instNote));

        }
        else if (isFirstTime)
        {
            //第二次
            isFirstTime = false;
            waitForPressureAgain = true;
        }
        else {/*报错*/}
    }
    IEnumerator WaitForPressureAgain(BeatLine firstBeatLine, NoteEdit noteEdit)
    {
        BPM BPMData = null;
        while (true)
        {
            if (waitForPressureAgain)
            {
                break;
            }
            //TODO
            GetNearestBeatLineAndVerticalLine(out BeatLine againBeatLine, out VLine againVLine, out Public_LineDiv againPublic_LineDiv);
            try
            {
                BPMData = againBeatLine.thisBPM;
                float endSeconds = BPMManager.Instance.GetSecondsTimeWithBPMSeconds(BPMData.thisStartBPM);
                float startSeconds = BPMManager.Instance.GetSecondsTimeWithBPMSeconds(firstBeatLine.thisBPM.thisStartBPM);
                float delta_Canvas = YScale.Instance.GetPositionYWithSecondsTime(endSeconds)
                    - YScale.Instance.GetPositionYWithSecondsTime(startSeconds);
                noteEdit.rectTransform.sizeDelta = new(noteEdit.rectTransform.sizeDelta.x, delta_Canvas);
            }
            catch { }
            //
            yield return new WaitForEndOfFrame();
        }
        waitForPressureAgain = false;
        noteEdit.thisNote.endTime = new(BPMData.integer, BPMData.molecule, BPMData.denominator);

        if (noteEdit.thisNote.endTime.thisStartBPM - noteEdit.thisNote.hitTime.thisStartBPM <= .0001f)
        {
            Debug.LogError("哒咩哒咩，长度为0的Hold！");
            Destroy(noteEdit.gameObject);
        }
        else
        {
            EventsEdit_Edit.Instance.UpdateEditingInfo(noteEdit, true);
            AddNoteEdit2Chart();
        }
    }

    private void AddNoteEdit2Chart()
    {
        int index_noteEdits = Algorithm.BinarySearch(findedPublic_LineDiv.noteEdits, m => m.thisNote.hitTime.thisStartBPM < instNote.thisNote.hitTime.thisStartBPM, false);
        findedPublic_LineDiv.noteEdits.Insert(index_noteEdits, instNote);


        List<Blophy.ChartEdit.Note> onlineNotes = Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[int.Parse(LineNumber.Instance.thisText.text) - 1].onlineNotes;
        int index_onlineNotes = Algorithm.BinarySearch(onlineNotes, m => m.hitTime.thisStartBPM < instNote.thisNote.hitTime.thisStartBPM, false);
        onlineNotes.Insert(index_onlineNotes, instNote.thisNote);


        ChartTools.EditNote2ChartDataNote(
            Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[int.Parse(LineNumber.Instance.thisText.text) - 1],
            Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[int.Parse(LineNumber.Instance.thisText.text) - 1].onlineNotes);

        Chart.Instance.RefreshPlayer();
    }

    private static void GetNearestBeatLineAndVerticalLine(out BeatLine nearestBeatLine, out VLine nearestVerticalLine, out Public_LineDiv public_Linediv)
    {
        nearestBeatLine = null;//最近的节拍线
        nearestVerticalLine = null;//最近的垂直线
        public_Linediv = null;
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
    }
}
