using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaySpeed : MonoBehaviourSingleton<PlaySpeed>
{
    public TMP_InputField thisInputField;
    private void Start()
    {
        thisInputField.onValueChanged.AddListener((value) =>
        {
            if (!float.TryParse(value, out float playSpeed)) return;
            double currentTime = ProgressManager.Instance.CurrentTime;
            ProgressManager.Instance.SetPlaySpeed(playSpeed);
            ProgressManager.Instance.SetTime(currentTime);
            Chart.Instance.Refresh();
        });
    }
}
