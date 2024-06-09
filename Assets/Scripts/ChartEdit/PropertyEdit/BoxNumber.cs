using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxNumber : MonoBehaviourSingleton<BoxNumber>
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
            for (int i = 0; i < ChartPreviewEdit.Instance.eventLines.Count; i++)
            {
                ChartPreviewEdit.Instance.eventLines[i].RefreshNoteEdits();
            }
        });
        d.onClick.AddListener(() =>
        {
            thisText.text = $"{int.Parse(thisText.text) - 1}";
            for (int i = 0; i < ChartPreviewEdit.Instance.noteLines.Count; i++)
            {
                ChartPreviewEdit.Instance.noteLines[i].RefreshNoteEdits();
            }
            for (int i = 0; i < ChartPreviewEdit.Instance.eventLines.Count; i++)
            {
                ChartPreviewEdit.Instance.eventLines[i].RefreshNoteEdits();
            }
        });
    }
}
