using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class Edit_EventEdit : MonoBehaviour
{
    [SerializeField] private EventEdit eventEdit;
    public EventEdit EventEdit
    {
        get
        {
            return eventEdit;
        }
        set
        {
            Debug.Log(value.thisEvent.isSelected);
            eventEdit = value;
        }
    }

    public TMP_InputField startTime;
    public TMP_InputField endTime;
    public TMP_InputField startValue;
    public TMP_InputField endValue;
    public TMP_Dropdown curveSelect;
    public LineRenderer curve;
    public Camera lineRendererCamera;
    private void Start()
    {
        startTime.onValueChanged.AddListener((value) =>
        {
            Match match = Regex.Match(value, @"(\d+):(\d+)/(\d+)");
            if (match.Success)
            {
                EventEdit.thisEvent.startTime.integer = int.Parse(match.Groups[1].Value);
                EventEdit.thisEvent.startTime.molecule = int.Parse(match.Groups[2].Value);
                EventEdit.thisEvent.startTime.denominator = int.Parse(match.Groups[3].Value);
                Chart.Instance.Refresh();
            }
        });
        endTime.onValueChanged.AddListener((value) =>
        {
            Match match = Regex.Match(value, @"(\d+):(\d+)/(\d+)");
            if (match.Success)
            {
                EventEdit.thisEvent.endTime.integer = int.Parse(match.Groups[1].Value);
                EventEdit.thisEvent.endTime.molecule = int.Parse(match.Groups[2].Value);
                EventEdit.thisEvent.endTime.denominator = int.Parse(match.Groups[3].Value);
                Chart.Instance.Refresh();
            }
        });
        startValue.onValueChanged.AddListener((value) =>
        {
            //if (eventEdit.eventVLine.eventType == EventType.speed) return;
            if (float.TryParse(value, out float result))
            {
                EventEdit.thisEvent.startValue = result;
                EventEdit.eventVLine.AddEventEdit2ChartDataEvent(true);
                //.asd
                //if (EventEdit.eventVLine.eventType == EventType.speed)
                //    Chart.Instance.Refresh();
            }
        });

        endValue.onValueChanged.AddListener((value) =>
        {
            //if (eventEdit.eventVLine.eventType == EventType.speed) return;
            if (float.TryParse(value, out float result))
            {
                EventEdit.thisEvent.endValue = result;
                EventEdit.eventVLine.AddEventEdit2ChartDataEvent(true);
                //sad
                //if (EventEdit.eventVLine.eventType == EventType.speed)
                //    Chart.Instance.Refresh();
            }
        });
        curveSelect.onValueChanged.AddListener((value) =>
        {
            if (value > 30 || value < 0)
            {

            }
            else
            {
                Public_AnimationCurveEaseEnum.keyValuePairs.TryGetValue(value, out AnimationCurve resultCurve);
                EventEdit.thisEvent.curve = resultCurve;
                EventEdit.eventVLine.AddEventEdit2ChartDataEvent(true);
            }
        });
    }
    public void ResetAllValue()
    {
        startTime.SetTextWithoutNotify(string.Empty);
        endTime.SetTextWithoutNotify(string.Empty);
        startValue.SetTextWithoutNotify(string.Empty);
        endValue.SetTextWithoutNotify(string.Empty);
        curveSelect.SetValueWithoutNotify(0);
        Public_AnimationCurveEaseEnum.keyValuePairs.TryGetValue(0, out AnimationCurve resultCurve);
        for (int i = 0; i < curve.positionCount; i++)
        {
            float x = i / (float)curve.positionCount;
            float y = resultCurve.Evaluate(x);
            curve.endWidth = curve.startWidth = .05f;
            curve.SetPosition(i, (Vector2)lineRendererCamera.ViewportToWorldPoint((new Vector2(x, y) * .5f) + Vector2.one * .25f));
        }
    }
    public void InitThisEventEdit(EventEdit eventEdit, bool changeSelectStateYesOrNo)
    {
        if (changeSelectStateYesOrNo)
        {
            Debug.Log("InitThisEventEdit");
            if (this.EventEdit != null)
            {
                this.EventEdit.thisEvent.isSelected = false;
                EventEdit.image.color = new(1, 1, 1, .61f);
                EventEdit.eventVLine.AddEventEdit2ChartDataEvent(true);
            }
            eventEdit.thisEvent.isSelected = true;
            eventEdit.image.color = Color.white;
            this.EventEdit = eventEdit;
        }
        else
        {
            Debug.Log("InitThisEventEditWithoutChangeState");
            this.EventEdit = eventEdit;
        }


        startTime.SetTextWithoutNotify($"{eventEdit.thisEvent.startTime.integer}:{eventEdit.thisEvent.startTime.molecule}/{eventEdit.thisEvent.startTime.denominator}");
        //startTime.text = $"{eventEdit.thisEvent.startTime.integer}:{eventEdit.thisEvent.startTime.molecule}/{eventEdit.thisEvent.startTime.denominator}";
        endTime.SetTextWithoutNotify($"{eventEdit.thisEvent.endTime.integer}:{eventEdit.thisEvent.endTime.molecule}/{eventEdit.thisEvent.endTime.denominator}");
        //endTime.text = $"{eventEdit.thisEvent.endTime.integer}:{eventEdit.thisEvent.endTime.molecule}/{eventEdit.thisEvent.endTime.denominator}";
        startValue.SetTextWithoutNotify($"{eventEdit.thisEvent.startValue}");
        //startValue.text = $"{eventEdit.thisEvent.startValue}";
        endValue.SetTextWithoutNotify($"{eventEdit.thisEvent.endValue}");
        //endValue.text = $"{eventEdit.thisEvent.endValue}";


        for (int i = 0; i < curve.positionCount; i++)
        {
            float x = i / (float)curve.positionCount;
            float y = eventEdit.thisEvent.curve.Evaluate(x);
            curve.endWidth = curve.startWidth = .05f;
            curve.SetPosition(i, (Vector2)lineRendererCamera.ViewportToWorldPoint((new Vector2(x, y) * .5f) + Vector2.one * .25f));
        }
    }
}
