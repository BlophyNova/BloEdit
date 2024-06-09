using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Offset : MonoBehaviourSingleton<Offset>
{
    public TMP_InputField thisInputField;
    private void Start()
    {
        thisInputField.SetTextWithoutNotify($"{(int)(Chart.Instance.chartData.globalData.offset * 1000)}");
        thisInputField.onValueChanged.AddListener((value) =>
        {
            if (float.TryParse(value, out float result))
            {
                Chart.Instance.chartData.globalData.offset = result / 1000;

                ProgressManager.Instance.OffsetTime(0);
            }
        });
    }
}
