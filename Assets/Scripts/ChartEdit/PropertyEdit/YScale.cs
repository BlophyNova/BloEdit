using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YScale : MonoBehaviourSingleton<YScale>
{
    public TMP_InputField thisInputField;
    public float yscale = 6;
    public float CurrentYScale
    {
        get
        {
            if (!float.TryParse(thisInputField.text, out float result)) return yscale;
            if (result == 0) return yscale;
            yscale = result;
            return yscale;
        }
    }
    public float GetPositionYWithSecondsTime(float secondsTime)
    {
        float currentTime = secondsTime * 100;
        return currentTime * CurrentYScale;
    }
    private void Start()
    {
        thisInputField.onValueChanged.AddListener((value) =>
        {
            if (!float.TryParse(value, out float result)) return;
            Chart.Instance.RefreshChart();
        });
    }
}
