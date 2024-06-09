using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EventsEdit_LineDiv : Public_LineDiv
{
    public EventEdit eventEditPrefab;
    public override void RefreshNoteEdits()//刷新方框事件
    {
        ClearAllEvents();
        List<VLines> eventVLines = VerticalLine.Instance.eventVLines;
        int currentBoxNumber = int.Parse(BoxNumber.Instance.thisText.text) - 1;

        for (int i = 0; i < eventVLines.Count; i++)
        {
            EventVLine edgeLeftVerticalLine = (EventVLine)eventVLines[i].edgeLeftVerticalLine;
            EventVLine edgeRightVerticalLine = (EventVLine)eventVLines[i].edgeRightVerticalLine;
            UpdateSingleEvents(Chart.Instance.chartEdit.boxesEdit[currentBoxNumber].lines[0].speed, edgeLeftVerticalLine);
            UpdateSingleEvents(Chart.Instance.chartEdit.boxesEdit[currentBoxNumber].boxEvents.lineAlpha, edgeRightVerticalLine);
            for (int j = 0; j < eventVLines[i].middleLines.Count; j++)
            {
                EventVLine middleVLine = (EventVLine)eventVLines[i].middleLines[j];
                switch (middleVLine.eventType)
                {
                    case EventType.centerX:
                        UpdateSingleEvents(Chart.Instance.chartEdit.boxesEdit[currentBoxNumber].boxEvents.centerX, middleVLine);
                        break;
                    case EventType.centerY:
                        UpdateSingleEvents(Chart.Instance.chartEdit.boxesEdit[currentBoxNumber].boxEvents.centerY, middleVLine);
                        break;
                    case EventType.moveX:
                        UpdateSingleEvents(Chart.Instance.chartEdit.boxesEdit[currentBoxNumber].boxEvents.moveX, middleVLine);
                        break;
                    case EventType.moveY:
                        UpdateSingleEvents(Chart.Instance.chartEdit.boxesEdit[currentBoxNumber].boxEvents.moveY, middleVLine);
                        break;
                    case EventType.scaleX:
                        UpdateSingleEvents(Chart.Instance.chartEdit.boxesEdit[currentBoxNumber].boxEvents.scaleX, middleVLine);
                        break;
                    case EventType.scaleY:
                        UpdateSingleEvents(Chart.Instance.chartEdit.boxesEdit[currentBoxNumber].boxEvents.scaleY, middleVLine);
                        break;
                    case EventType.rotate:
                        UpdateSingleEvents(Chart.Instance.chartEdit.boxesEdit[currentBoxNumber].boxEvents.rotate, middleVLine);
                        break;
                    case EventType.alpha:
                        UpdateSingleEvents(Chart.Instance.chartEdit.boxesEdit[currentBoxNumber].boxEvents.alpha, middleVLine);
                        break;
                }
            }
        }
        //EventsEdit_Edit.Instance.UpdateEditingInfo(selectedEventEdit);
    }

    private void UpdateSingleEvents(List<Blophy.ChartEdit.Event> events, EventVLine eventVLine)
    {
        for (int i = 0; i < events.Count; i++)
        {
            Blophy.ChartEdit.Event currentEvent = events[i];
            EventEdit instEventEdit = Instantiate(eventEditPrefab, notesCanvas.transform);
            instEventEdit.thisEvent = currentEvent;
            instEventEdit.IsRefresh();
            instEventEdit.Init(currentEvent.startTime, eventVLine, this, events);
            eventVLine.eventsEditList.Add(instEventEdit);

            if (currentEvent.isSelected)
                EventsEdit_Edit.Instance.UpdateEditingInfo(instEventEdit, false);
        }
    }

    private static void ClearAllEvents()
    {
        List<VLines> eventVLines = VerticalLine.Instance.eventVLines;
        for (int i = 0; i < eventVLines.Count; i++)
        {
            EventVLine edgeLeftVerticalLine = (EventVLine)eventVLines[i].edgeLeftVerticalLine;
            EventVLine edgeRightVerticalLine = (EventVLine)eventVLines[i].edgeRightVerticalLine;

            for (int j = 0; j < edgeLeftVerticalLine.eventsEditList.Count; j++)
            {
                EventEdit eventEdit = edgeLeftVerticalLine.eventsEditList[j];
                Destroy(eventEdit.gameObject);
            }
            edgeLeftVerticalLine.eventsEditList.Clear();

            for (int j = 0; j < edgeRightVerticalLine.eventsEditList.Count; j++)
            {
                EventEdit eventEdit = edgeRightVerticalLine.eventsEditList[j];
                Destroy(eventEdit.gameObject);
            }
            edgeRightVerticalLine.eventsEditList.Clear();

            for (int j = 0; j < eventVLines[i].middleLines.Count; j++)
            {
                EventVLine eventVLine = (EventVLine)eventVLines[i].middleLines[j];
                for (int k = 0; k < eventVLine.eventsEditList.Count; k++)
                {
                    EventEdit eventEdit = eventVLine.eventsEditList[k];
                    Destroy(eventEdit.gameObject);
                }
                eventVLine.eventsEditList.Clear();
            }
        }
    }
}
