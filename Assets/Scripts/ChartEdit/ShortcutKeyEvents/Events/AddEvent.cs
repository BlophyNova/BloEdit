using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddEvent : ShortcutKeyEvent
{
    public EventEdit thisEvent;

    public bool isFirstTime = false;

    public bool waitForPressureAgain = false;



    public Public_LineDiv findedPublic_LineDiv = null;
    public EventEdit instEvent = null;
    public override void ExeDown()
    {
        Debug.Log("ExeAddEvent");
        if (!isFirstTime)
        {
            isFirstTime = true;
            //第一次
            GetNearestBeatLineAndVerticalLine(
            out BeatLine firstBeatLine, out VLine firstVLine, out findedPublic_LineDiv);
            EventVLine eventVLine = (EventVLine)firstVLine;
            instEvent = Instantiate(thisEvent, Vector2.zero, Quaternion.identity, findedPublic_LineDiv.notesCanvas.transform)
           .Init(firstBeatLine, eventVLine, findedPublic_LineDiv, eventVLine.events);
            StartCoroutine(WaitForPressureAgain(firstBeatLine, instEvent, eventVLine));

        }
        else if (isFirstTime)
        {
            //第二次
            isFirstTime = false;
            waitForPressureAgain = true;
        }
        else {/*报错*/}
    }
    IEnumerator WaitForPressureAgain(BeatLine firstBeatLine, EventEdit instEvent, EventVLine eventVLine)
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
                instEvent.rectTransform.sizeDelta = new(instEvent.rectTransform.sizeDelta.x, delta_Canvas);
            }
            catch { }
            //
            yield return new WaitForEndOfFrame();
        }
        waitForPressureAgain = false;
        instEvent.thisEvent.endTime = new(BPMData.integer, BPMData.molecule, BPMData.denominator);
        if (instEvent.thisEvent.endTime.thisStartBPM - instEvent.thisEvent.startTime.thisStartBPM <= .0001f)
        {
            Debug.LogError("哒咩哒咩，长度为0的Event！");
            Destroy(instEvent.gameObject);
        }
        else
        {
            //AddNoteEdit2Chart(eventVLine);
            eventVLine.AddEventEdit2ChartDataEvent(true, instEvent);
            EventsEdit_Edit.Instance.UpdateEditingInfo(instEvent, true);
            if (instEvent.eventVLine.eventType == EventType.scaleX)
            {
                EventEdit sync = Instantiate(instEvent, Vector2.zero, Quaternion.identity, findedPublic_LineDiv.notesCanvas.transform);
                //.Init(firstBeatLine, ChartPreviewEdit.Instance.eventLines., findedPublic_LineDiv, eventVLine.events);
                for (int i = 0; i < ChartPreviewEdit.Instance.eventLines.Count; i++)
                {
                    Public_LineDiv eventLines = ChartPreviewEdit.Instance.eventLines[i];
                    for (int j = 0; j < eventLines.vLines.middleLines.Count; j++)
                    {
                        EventVLine eventV = (EventVLine)eventLines.vLines.middleLines[j];
                        switch (eventV.eventType)
                        {
                            //case EventType.centerX:
                            //    break;
                            //case EventType.centerY:
                            //    break;
                            //case EventType.moveX:
                            //    break;
                            //case EventType.moveY:
                            //    break;
                            //case EventType.scaleX:
                            //    break;
                            case EventType.scaleY:
                                sync.Init(firstBeatLine, eventV, findedPublic_LineDiv, eventV.events);
                                sync.thisEvent.endTime = instEvent.thisEvent.endTime;
                                sync.thisEvent.isSelected = false;
                                eventV.AddEventEdit2ChartDataEvent(true, sync);
                                break;
                        }
                    }
                }
            }
            else if (instEvent.eventVLine.eventType == EventType.scaleY)
            {
                EventEdit sync = Instantiate(instEvent, Vector2.zero, Quaternion.identity, findedPublic_LineDiv.notesCanvas.transform);
                //.Init(firstBeatLine, ChartPreviewEdit.Instance.eventLines., findedPublic_LineDiv, eventVLine.events);
                for (int i = 0; i < ChartPreviewEdit.Instance.eventLines.Count; i++)
                {
                    Public_LineDiv eventLines = ChartPreviewEdit.Instance.eventLines[i];
                    for (int j = 0; j < eventLines.vLines.middleLines.Count; j++)
                    {
                        EventVLine eventV = (EventVLine)eventLines.vLines.middleLines[j];
                        switch (eventV.eventType)
                        {
                            case EventType.scaleX:
                                sync.Init(firstBeatLine, eventV, findedPublic_LineDiv, eventV.events);
                                sync.thisEvent.endTime = instEvent.thisEvent.endTime;
                                sync.thisEvent.isSelected = false;
                                eventV.AddEventEdit2ChartDataEvent(true, sync);
                                break;
                        }
                    }
                }
            }
            else if (instEvent.eventVLine.eventType == EventType.centerX)
            {
                EventEdit sync = Instantiate(instEvent, Vector2.zero, Quaternion.identity, findedPublic_LineDiv.notesCanvas.transform);
                //.Init(firstBeatLine, ChartPreviewEdit.Instance.eventLines., findedPublic_LineDiv, eventVLine.events);
                for (int i = 0; i < ChartPreviewEdit.Instance.eventLines.Count; i++)
                {
                    Public_LineDiv eventLines = ChartPreviewEdit.Instance.eventLines[i];
                    for (int j = 0; j < eventLines.vLines.middleLines.Count; j++)
                    {
                        EventVLine eventV = (EventVLine)eventLines.vLines.middleLines[j];
                        switch (eventV.eventType)
                        {
                            case EventType.centerY:
                                sync.Init(firstBeatLine, eventV, findedPublic_LineDiv, eventV.events);
                                sync.thisEvent.endTime = instEvent.thisEvent.endTime;
                                sync.thisEvent.isSelected = false;
                                eventV.AddEventEdit2ChartDataEvent(true, sync);
                                break;
                        }
                    }
                }
            }
            else if (instEvent.eventVLine.eventType == EventType.centerY)
            {
                EventEdit sync = Instantiate(instEvent, Vector2.zero, Quaternion.identity, findedPublic_LineDiv.notesCanvas.transform);
                //.Init(firstBeatLine, ChartPreviewEdit.Instance.eventLines., findedPublic_LineDiv, eventVLine.events);
                for (int i = 0; i < ChartPreviewEdit.Instance.eventLines.Count; i++)
                {
                    Public_LineDiv eventLines = ChartPreviewEdit.Instance.eventLines[i];
                    for (int j = 0; j < eventLines.vLines.middleLines.Count; j++)
                    {
                        EventVLine eventV = (EventVLine)eventLines.vLines.middleLines[j];
                        switch (eventV.eventType)
                        {
                            case EventType.centerX:
                                sync.Init(firstBeatLine, eventV, findedPublic_LineDiv, eventV.events);
                                sync.thisEvent.endTime = instEvent.thisEvent.endTime;
                                sync.thisEvent.isSelected = false;
                                eventV.AddEventEdit2ChartDataEvent(true, sync);
                                break;
                        }
                    }
                }
            }
            else if (instEvent.eventVLine.eventType == EventType.moveY)
            {
                EventEdit sync = Instantiate(instEvent, Vector2.zero, Quaternion.identity, findedPublic_LineDiv.notesCanvas.transform);
                //.Init(firstBeatLine, ChartPreviewEdit.Instance.eventLines., findedPublic_LineDiv, eventVLine.events);
                for (int i = 0; i < ChartPreviewEdit.Instance.eventLines.Count; i++)
                {
                    Public_LineDiv eventLines = ChartPreviewEdit.Instance.eventLines[i];
                    for (int j = 0; j < eventLines.vLines.middleLines.Count; j++)
                    {
                        EventVLine eventV = (EventVLine)eventLines.vLines.middleLines[j];
                        switch (eventV.eventType)
                        {
                            case EventType.moveX:
                                sync.Init(firstBeatLine, eventV, findedPublic_LineDiv, eventV.events);
                                sync.thisEvent.endTime = instEvent.thisEvent.endTime;
                                sync.thisEvent.isSelected = false;
                                eventV.AddEventEdit2ChartDataEvent(true, sync);
                                break;
                        }
                    }
                }
            }
            else if (instEvent.eventVLine.eventType == EventType.moveX)
            {
                EventEdit sync = Instantiate(instEvent, Vector2.zero, Quaternion.identity, findedPublic_LineDiv.notesCanvas.transform);
                //.Init(firstBeatLine, ChartPreviewEdit.Instance.eventLines., findedPublic_LineDiv, eventVLine.events);
                for (int i = 0; i < ChartPreviewEdit.Instance.eventLines.Count; i++)
                {
                    Public_LineDiv eventLines = ChartPreviewEdit.Instance.eventLines[i];
                    for (int j = 0; j < eventLines.vLines.middleLines.Count; j++)
                    {
                        EventVLine eventV = (EventVLine)eventLines.vLines.middleLines[j];
                        switch (eventV.eventType)
                        {
                            case EventType.moveY:
                                sync.Init(firstBeatLine, eventV, findedPublic_LineDiv, eventV.events);
                                sync.thisEvent.endTime = instEvent.thisEvent.endTime;
                                sync.thisEvent.isSelected = false;
                                eventV.AddEventEdit2ChartDataEvent(true, sync);
                                break;
                        }
                    }
                }
            }
        }
    }
    private static void GetNearestBeatLineAndVerticalLine(out BeatLine nearestBeatLine, out VLine nearestVerticalLine, out Public_LineDiv public_Linediv)
    {
        nearestBeatLine = null;//最近的节拍线
        nearestVerticalLine = null;//最近的垂直线
        public_Linediv = null;
        //float currentNearestDistance = 10000;
        float currentNearestBeatLine = 10000;
        float currentNearestVerticalLine = 10000;
        for (int i = 0; i < ChartPreviewEdit.Instance.eventLines.Count; i++)
        {
            if (!ChartPreviewEdit.Instance.eventLines[i].gameObject.activeInHierarchy) continue;
            foreach (BeatLine beatLine in ChartPreviewEdit.Instance.eventLines[i].beatLines)
            {
                //Input.mousePosition-beatLine.transform.position
                RectTransformUtility.ScreenPointToWorldPointInRectangle(ShortcutKeyEvents.Instance.canvas.transform as RectTransform, Input.mousePosition, null, out Vector3 beatLine_worldPosition);
                Vector3 beatLine_distance = beatLine_worldPosition - beatLine.transform.position;
                if (beatLine_distance.magnitude < currentNearestBeatLine)
                {
                    nearestBeatLine = beatLine;
                    currentNearestBeatLine = beatLine_distance.magnitude;
                    public_Linediv = ChartPreviewEdit.Instance.eventLines[i];
                }
            }



            foreach (EventVLines vLine in VerticalLine.Instance.eventVLines.Cast<EventVLines>())
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
