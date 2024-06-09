using Blophy.ChartEdit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldEdit : NoteEdit
{
    public override NoteEdit Init(BeatLine beatLine, VLine vline, Public_LineDiv public_LineDiv, List<Blophy.ChartEdit.Note> editing)
    {
        base.Init(beatLine, vline, public_LineDiv, editing);
        thisNote.noteType = Blophy.Chart.NoteType.Hold;
        return this;
    }
    public override void HoldLengthHandle()
    {
        if (thisNote.endTime == null)
        {
            base.HoldLengthHandle();
        }
        float BPMData = thisNote.endTime.thisStartBPM;
        float endSeconds = BPMManager.Instance.GetSecondsTimeWithBPMSeconds(BPMData);
        float startSeconds = BPMManager.Instance.GetSecondsTimeWithBPMSeconds(thisNote.hitTime.thisStartBPM);
        float delta_Canvas = YScale.Instance.GetPositionYWithSecondsTime(endSeconds)
            - YScale.Instance.GetPositionYWithSecondsTime(startSeconds);
        rectTransform.sizeDelta = new(rectTransform.sizeDelta.x, delta_Canvas);
    }
    /*
     * public virtual NoteEdit Init(BeatLine beatLine, VLine vline, Public_LineDiv public_LineDiv)
    {
        BPMTime bpmTime = new() { integer = beatLine.thisBPM.integer, denominator = beatLine.thisBPM.denominator, molecule = beatLine.thisBPM.molecule };
        return Init(bpmTime, vline.positionX, public_LineDiv);
    }
    public virtual NoteEdit Init(BPMTime bpmTime, float positionX, Public_LineDiv public_LineDiv)
    {
        thisNote = new();
        thisNote.positionX = positionX;
        thisNote.hitTime = new(bpmTime.integer, bpmTime.molecule, bpmTime.denominator);
        thisNote.effect = Blophy.Chart.NoteEffect.Ripple & Blophy.Chart.NoteEffect.CommonEffect;
        thisNote.endTime = thisNote.hitTime;
        //thisNote.noteType = Blophy.Chart.NoteType.Tap;
        ///*//*new Vector2(nearestVerticalLine.transform.position.x, nearestBeatLine.transform.position.y)*//*/
        //float canvasLocalPositionX = (vline.vLines.edgeRightVerticalLine.transform.localPosition - vline.vLines.edgeLeftVerticalLine.transform.localPosition).x / 2 * thisNote.positionX;
        float canvasLocalPositionX = (public_LineDiv.vLines.edgeRightVerticalLine.transform.localPosition - public_LineDiv.vLines.edgeLeftVerticalLine.transform.localPosition).x / 2 * thisNote.positionX;
        float canvasLocalPositionY = YScale.Instance.GetPositionYWithSecondsTime(BPMManager.Instance.GetSecondsTimeWithBPMSeconds(thisNote.hitTime.thisStartBPM));
        transform.localPosition = new Vector2(canvasLocalPositionX, canvasLocalPositionY);
        return this;
    }
     */
}
