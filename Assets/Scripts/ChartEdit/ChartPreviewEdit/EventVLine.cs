using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventVLine : VLine
{
    public EventType eventType;
    public List<EventEdit> eventsEditList;
    public List<Blophy.ChartEdit.Event> events;
    public EventVLine ChangeEventEdit(EventEdit eventEdit)
    {
        EventEdit findedEventEdit = eventsEditList.Find((m) => m == eventEdit);
        findedEventEdit = eventEdit;
        return this;
    }
    public void AddEventEdit2ChartDataEvent(bool refreshYesOrNo, EventEdit instEvent = null)
    {
        //添加事件到上边的事件列表
        if (instEvent != null)
        {
            eventsEditList.Add(instEvent);
        }
        //给boxEvents一个备份
        Blophy.Chart.BoxEvents boxEvents = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents;
        //看看自己是谁
        List<Blophy.Chart.Event> events = eventType switch
        {
            EventType.centerX => boxEvents.centerX,
            EventType.centerY => boxEvents.centerY,
            EventType.moveX => boxEvents.moveX,
            EventType.moveY => boxEvents.moveY,
            EventType.scaleX => boxEvents.scaleX,
            EventType.scaleY => boxEvents.scaleY,
            EventType.rotate => boxEvents.rotate,
            EventType.alpha => boxEvents.alpha,
            EventType.lineAlpha => boxEvents.lineAlpha,
            EventType.speed => null,
            _ => throw new System.Exception("Ohhhhh...没找到对应的事件类型")
        };
        //如果是null说明自己是speed
        events ??= Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[1].Speed
            = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[2].Speed
            = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[3].Speed
            = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[4].Speed
            = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[0].Speed;
        Algorithm.BubbleSort(eventsEditList, (a, b) =>//排序
        {
            if (a.thisEvent.startTime.thisStartBPM > b.thisEvent.startTime.thisStartBPM)
            {
                return 1;
            }
            else if (a.thisEvent.startTime.thisStartBPM < b.thisEvent.startTime.thisStartBPM)
            {
                return -1;
            }
            return 0;
        });
        if (instEvent != null)
        {

            int indexInList = eventsEditList.IndexOf(instEvent);
            if (indexInList - 1 >= 0)
                eventsEditList[indexInList].thisEvent.startValue = eventsEditList[indexInList - 1].thisEvent.endValue;
        }
        //如果是
        switch (eventType)
        {
            case EventType.speed:
                List<Blophy.ChartEdit.Event> allSpeed
                    = Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[0].speed
                      = Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[1].speed
                      = Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[2].speed
                      = Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[3].speed
                      = Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[4].speed;
                allSpeed.Clear();
                for (int i = 0; i < eventsEditList.Count; i++)
                {
                    allSpeed.Add(eventsEditList[i].thisEvent);
                }

                break;
            case EventType.centerX://一下的这些，那就清空ChartEdit的数据，把自己单独的轨道里的数据复制过去
                UpdateEvents(Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.centerX);
                break;
            case EventType.centerY:
                UpdateEvents(Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.centerY);
                break;
            case EventType.moveX:
                UpdateEvents(Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.moveX);
                break;
            case EventType.moveY:
                UpdateEvents(Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.moveY);
                break;
            case EventType.scaleX:
                UpdateEvents(Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.scaleX);
                break;
            case EventType.scaleY:
                UpdateEvents(Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.scaleY);
                break;
            case EventType.rotate:
                UpdateEvents(Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.rotate);
                break;
            case EventType.alpha:
                UpdateEvents(Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.alpha);
                break;
            case EventType.lineAlpha:
                UpdateEvents(Chart.Instance.chartEdit.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.lineAlpha);
                break;
        }

        events.Clear();
        for (int i = 0; i < eventsEditList.Count; i++)
        {
            Blophy.ChartEdit.Event currentEventEdit = eventsEditList[i].thisEvent;
            Blophy.Chart.Event chartDataEvent = new();
            chartDataEvent.startTime = BPMManager.Instance.GetSecondsTimeWithBPMSeconds(currentEventEdit.startTime.thisStartBPM);
            chartDataEvent.endTime = BPMManager.Instance.GetSecondsTimeWithBPMSeconds(currentEventEdit.endTime.thisStartBPM);
            chartDataEvent.startValue = currentEventEdit.startValue;
            chartDataEvent.endValue = currentEventEdit.endValue;
            chartDataEvent.curve = currentEventEdit.curve;

            events.Add(chartDataEvent);
        }
        if (eventType == EventType.speed)
        {
            //***这里补充Speed的事件空隙
            List<Blophy.Chart.Event> speedEventVoidFill = new();
            float defaultStartTime = 0;
            float defaultValue = 2;
            for (int i = 0; i < events.Count; i++)
            {
                if (events[i].startTime > defaultStartTime)
                {
                    Blophy.Chart.Event speedEvent = new();
                    speedEvent.startTime = defaultStartTime;
                    speedEvent.endTime = events[i].startTime;
                    speedEvent.startValue = defaultValue;
                    speedEvent.endValue = defaultValue;
                    Public_AnimationCurveEaseEnum.keyValuePairs.TryGetValue(0, out AnimationCurve anim);
                    speedEvent.curve = anim;
                    speedEventVoidFill.Add(speedEvent);
                }
                defaultValue = events[i].endValue;
                defaultStartTime = events[i].endTime;
                speedEventVoidFill.Add(events[i]);
            }
            if (events.Count != 0)
            {
                if (speedEventVoidFill[^1].endTime < Chart.Instance.chartData.globalData.MusicLength)
                {
                    Blophy.Chart.Event speedEvent = new();
                    speedEvent.startTime = defaultStartTime;
                    speedEvent.endTime = Chart.Instance.chartData.globalData.MusicLength;
                    speedEvent.startValue = defaultValue;
                    speedEvent.endValue = defaultValue;
                    Public_AnimationCurveEaseEnum.keyValuePairs.TryGetValue(0, out AnimationCurve anim);
                    speedEvent.curve = anim;
                    speedEventVoidFill.Add(speedEvent);
                }
            }
            else
            {
                Public_AnimationCurveEaseEnum.keyValuePairs.TryGetValue(0, out AnimationCurve result);
                speedEventVoidFill.Add(new() { startTime = 0, endTime = 200, startValue = 2, endValue = 2, curve = result });
            }
            events = speedEventVoidFill;
            Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[1].Speed
            = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[2].Speed
            = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[3].Speed
            = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[4].Speed
            = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[0].Speed = events;
            //***
            Box currentBox = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1];
            for (int i = 0; i < currentBox.lines.Count; i++)
            {
                ChartTools.SpeedEvents2Far(currentBox.lines[i]);
            }
        }
        if (refreshYesOrNo)
        {
            Chart.Instance.Refresh();
        }
    }
    private void UpdateEvents(List<Blophy.ChartEdit.Event> events)
    {
        events.Clear();
        for (int i = 0; i < eventsEditList.Count; i++)
        {
            /*Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.centerX*/
            events.Add(eventsEditList[i].thisEvent);
        }
        this.events = events;
    }
}