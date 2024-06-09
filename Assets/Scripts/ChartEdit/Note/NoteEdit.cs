using Blophy.ChartEdit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteEdit : Public_Button
{
    //new Vector2(nearestVerticalLine.transform.position.x, nearestBeatLine.transform.position.y)
    public Note thisNote;
    public RectTransform rectTransform;
    public bool isRefresh = false;

    public List<Blophy.ChartEdit.Note> editing;
    public Public_LineDiv public_LineDiv;

    public Image image;
    public override void OnStart()
    {
        thisButton.onClick.AddListener(() =>
        {
            Debug.Log("EventEditButtonExe");
            EventsEdit_Edit.Instance.UpdateEditingInfo(this, true);
        });
    }
    public NoteEdit IsRefresh()
    {
        isRefresh = true;
        return this;
    }
    public virtual NoteEdit Init(BeatLine beatLine, VLine vline, Public_LineDiv public_LineDiv, List<Blophy.ChartEdit.Note> editing)
    {
        BPMTime bpmTime = new() { integer = beatLine.thisBPM.integer, denominator = beatLine.thisBPM.denominator, molecule = beatLine.thisBPM.molecule };
        return Init(bpmTime, vline.positionX, public_LineDiv, editing);
    }
    public virtual NoteEdit Init(BPMTime bpmTime, float positionX, Public_LineDiv public_LineDiv, List<Blophy.ChartEdit.Note> editing)
    {

        if (isRefresh)
        {
            //Note tempNote = thisNote;

            //thisNote = new();
            //isRefresh = false;
            //thisNote.positionX = tempNote.positionX;
            //thisNote.hitTime = tempNote.hitTime;
            //thisNote.effect = tempNote.effect;
            //thisNote.endTime = tempNote.endTime;
            //thisNote.noteType = tempNote.noteType;
            //thisNote.isClockwise = tempNote.isClockwise;

        }
        else
        {
            thisNote.positionX = positionX;
            thisNote.hitTime = new(bpmTime.integer, bpmTime.molecule, bpmTime.denominator);
            thisNote.effect = Blophy.Chart.NoteEffect.Ripple | Blophy.Chart.NoteEffect.CommonEffect;
        }
        this.editing = editing;
        this.public_LineDiv = public_LineDiv;
        HoldLengthHandle();
        float canvasLocalPositionX = (public_LineDiv.vLines.edgeRightVerticalLine.transform.localPosition - public_LineDiv.vLines.edgeLeftVerticalLine.transform.localPosition).x / 2 * thisNote.positionX;
        float canvasLocalPositionY = YScale.Instance.GetPositionYWithSecondsTime(BPMManager.Instance.GetSecondsTimeWithBPMSeconds(thisNote.hitTime.thisStartBPM));
        transform.localPosition = new Vector2(canvasLocalPositionX, canvasLocalPositionY);
        return this;
    }
    public virtual void HoldLengthHandle()
    {
        thisNote.endTime = thisNote.hitTime;
    }
}
