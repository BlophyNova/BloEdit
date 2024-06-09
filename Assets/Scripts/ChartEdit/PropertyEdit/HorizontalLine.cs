using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalLine : MonoBehaviourSingleton<HorizontalLine>
{
    public TextMeshProUGUI thisText;
    public Button u;
    public Button d;
    private void Start()
    {
        u.onClick.AddListener(() =>
        {
            thisText.text = $"{int.Parse(thisText.text) + 1}";
            ChartPreviewEdit.DestoryAllBeatLines();
        });
        d.onClick.AddListener(() =>
        {
            thisText.text = $"{int.Parse(thisText.text) - 1}";
            ChartPreviewEdit.DestoryAllBeatLines();
        });
    }


}
