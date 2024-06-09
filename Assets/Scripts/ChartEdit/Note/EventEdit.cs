using Blophy.ChartEdit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventEdit : Public_Button
{

    public RectTransform rectTransform;
    public Blophy.ChartEdit.Event thisEvent;
    public bool isRefresh = false;

    public EventVLine eventVLine;
    public Image image;


    public List<Blophy.ChartEdit.Event> editing;
    public Public_LineDiv public_LineDiv;
    public override void OnStart()
    {
        thisButton.onClick.AddListener(() =>
        {
            Debug.Log("EventEditButtonExe");
            EventsEdit_Edit.Instance.UpdateEditingInfo(this, true);
        });
    }

    public EventEdit Init(BeatLine beatLine, VLine vline, Public_LineDiv public_LineDiv, List<Blophy.ChartEdit.Event> editing)
    {
        eventVLine = (EventVLine)vline;
        BPMTime bpmTime = new() { integer = beatLine.thisBPM.integer, denominator = beatLine.thisBPM.denominator, molecule = beatLine.thisBPM.molecule };
        return Init(bpmTime, vline.positionX, public_LineDiv, editing);
    }
    public void RefreshThisEventsList() => eventVLine.AddEventEdit2ChartDataEvent(true);
    public EventEdit IsRefresh()
    {
        isRefresh = true;
        return this;
    }
    public EventEdit Init(BPMTime bpmTime, VLine vline, Public_LineDiv public_LineDiv, List<Blophy.ChartEdit.Event> editing)
    {
        eventVLine = (EventVLine)vline;
        return Init(bpmTime, vline.positionX, public_LineDiv, editing);
    }
    public virtual EventEdit Init(BPMTime bpmTime, float positionX, Public_LineDiv public_LineDiv, List<Blophy.ChartEdit.Event> editing)
    {
        //Blophy.ChartEdit.Event tempEvent = thisEvent;

        //thisEvent = new();
        if (isRefresh)
        {
            isRefresh = false;
            //thisNote.positionX = tempNote.positionX;
            //thisNote.hitTime = tempNote.hitTime;
            //thisNote.effect = tempNote.effect;
            //thisNote.endTime = tempNote.endTime;
            //thisEvent.startTime = tempEvent.startTime;
            //thisEvent.endTime = tempEvent.endTime;
            //thisEvent.startValue = tempEvent.startValue;
            //thisEvent.endValue = tempEvent.endValue;
            //thisEvent.curve = tempEvent.curve;
            //thisEvent.isSelected = tempEvent.isSelected;
            if (thisEvent.isSelected)
            {
                image.color = Color.white;
            }
            float endSeconds = BPMManager.Instance.GetSecondsTimeWithBPMSeconds(thisEvent.endTime.thisStartBPM);
            float startSeconds = BPMManager.Instance.GetSecondsTimeWithBPMSeconds(thisEvent.startTime.thisStartBPM);
            float delta_Canvas = YScale.Instance.GetPositionYWithSecondsTime(endSeconds)
                - YScale.Instance.GetPositionYWithSecondsTime(startSeconds);
            rectTransform.sizeDelta = new(rectTransform.sizeDelta.x, delta_Canvas);
        }
        else
        {
            //thisNote.positionX = positionX;
            //thisNote.hitTime = new(bpmTime.integer, bpmTime.molecule, bpmTime.denominator);
            //thisNote.effect = Blophy.Chart.NoteEffect.Ripple & Blophy.Chart.NoteEffect.CommonEffect;
            thisEvent.startTime = bpmTime;
            Public_AnimationCurveEaseEnum.keyValuePairs.TryGetValue(1, out AnimationCurve curve);
            thisEvent.curve = curve;
            HoldLengthHandle();
        }

        this.editing = editing;
        this.public_LineDiv = public_LineDiv;
        float canvasLocalPositionX = (public_LineDiv.vLines.edgeRightVerticalLine.transform.localPosition - public_LineDiv.vLines.edgeLeftVerticalLine.transform.localPosition).x / 2 * positionX;
        float canvasLocalPositionY = YScale.Instance.GetPositionYWithSecondsTime(BPMManager.Instance.GetSecondsTimeWithBPMSeconds(thisEvent.startTime.thisStartBPM));
        transform.localPosition = new Vector2(canvasLocalPositionX, canvasLocalPositionY);
        return this;
    }
    public virtual void HoldLengthHandle()
    {
        thisEvent.endTime = thisEvent.startTime;
    }
}
