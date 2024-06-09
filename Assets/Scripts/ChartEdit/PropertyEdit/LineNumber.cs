using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LineNumber : MonoBehaviourSingleton<LineNumber>
{
    public TextMeshProUGUI thisText;
    public Button u;
    public Button d;
    private void Start()
    {
        u.onClick.AddListener(() =>
        {
            thisText.text = $"{int.Parse(thisText.text) + 1}";
            for (int i = 0; i < ChartPreviewEdit.Instance.noteLines.Count; i++)
            {
                ChartPreviewEdit.Instance.noteLines[i].RefreshNoteEdits();
            }
        });
        d.onClick.AddListener(() =>
        {
            thisText.text = $"{int.Parse(thisText.text) - 1}";
            for (int i = 0; i < ChartPreviewEdit.Instance.noteLines.Count; i++)
            {
                ChartPreviewEdit.Instance.noteLines[i].RefreshNoteEdits();
            }
        });
    }
}