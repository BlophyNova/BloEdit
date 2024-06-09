using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventView : MonoBehaviour
{
    public TMP_Dropdown thisDropdown;
    public RectTransform noteCanvas;
    public RectTransform eventCanvas;
    public RectTransform note2Canvas;
    private void Start()
    {
        thisDropdown.onValueChanged.AddListener((value) =>
        {
            if (value == 0)//N+E
            {
                noteCanvas.gameObject.SetActive(true);
                eventCanvas.gameObject.SetActive(true);
                note2Canvas.gameObject.SetActive(false);
                noteCanvas.anchoredPosition = new(-480, 0);
                noteCanvas.sizeDelta = new(960, 1080);
                eventCanvas.anchoredPosition = new(480, 0);
                eventCanvas.sizeDelta = new(960, 1080);
                for (int i = 0; i < VerticalLine.Instance.vLines.Count; i++)
                {
                    VerticalLine.Instance.vLines[i].AddLineOrRemoveLine(0, VerticalLine.Instance.thisText.text);
                }
            }
            else if (value == 1)//N+N
            {
                noteCanvas.gameObject.SetActive(true);
                eventCanvas.gameObject.SetActive(false);
                note2Canvas.gameObject.SetActive(true);
                noteCanvas.anchoredPosition = new(-480, 0);
                noteCanvas.sizeDelta = new(960, 1080);
                note2Canvas.anchoredPosition = new(480, 0);
                note2Canvas.sizeDelta = new(960, 1080);
                for (int i = 0; i < VerticalLine.Instance.vLines.Count; i++)
                {
                    VerticalLine.Instance.vLines[i].AddLineOrRemoveLine(0, VerticalLine.Instance.thisText.text);
                }
            }
            else if (value == 2)//N
            {
                noteCanvas.gameObject.SetActive(true);
                eventCanvas.gameObject.SetActive(false);
                note2Canvas.gameObject.SetActive(false);
                noteCanvas.anchoredPosition = Vector2.zero;
                noteCanvas.sizeDelta = new(1920, 1080);
                for (int i = 0; i < VerticalLine.Instance.vLines.Count; i++)
                {
                    VerticalLine.Instance.vLines[i].AddLineOrRemoveLine(0, VerticalLine.Instance.thisText.text);
                }
            }
            for (int i = 0; i < ChartPreviewEdit.Instance.noteLines.Count; i++)
            {
                ChartPreviewEdit.Instance.noteLines[i].RefreshNoteEdits();
            }
        });
    }
}
