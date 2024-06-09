using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetProgress : ShortcutKeyEvent
{
    private void Start()
    {
        ProgressManager.Instance.OffsetTime(-.2f);
        for (int i = 0; i < ChartPreviewEdit.Instance.eventLines.Count; i++)
        {
            EventVLine edgeLeftVerticalLine = (EventVLine)ChartPreviewEdit.Instance.eventLines[i].vLines.edgeLeftVerticalLine;
            edgeLeftVerticalLine.AddEventEdit2ChartDataEvent(true);
            EventVLine edgeRightVerticalLine = (EventVLine)ChartPreviewEdit.Instance.eventLines[i].vLines.edgeRightVerticalLine;
            edgeRightVerticalLine.AddEventEdit2ChartDataEvent(true);
            for (int j = 0; j < ChartPreviewEdit.Instance.eventLines[i].vLines.middleLines.Count; j++)
            {
                EventVLine middleLines = (EventVLine)ChartPreviewEdit.Instance.eventLines[i].vLines.middleLines[j];
                middleLines.AddEventEdit2ChartDataEvent(true);
            }
        }

    }
    public override void ExeDown()
    {
        Debug.Log($"ExeSetProgress");
    }
    private void Update()
    {
        float delta = Input.GetAxis("Mouse ScrollWheel");
        if (delta == 0 || ProgressManager.Instance.CurrentTime + delta < -.2f)
        {
            return;
        }
        StateManager.Instance.IsPause = true;
        ProgressManager.Instance.OffsetTime(delta);
        Chart.Instance.RefreshChart();
        //ProgressManager.Instance.SetTime(ProgressManager.Instance.CurrentTime + delta);
        //for (int i = 0; i < ChartPreviewEdit.Instance.lines.Count; i++)
        //{
        //    ChartPreviewEdit.Instance.lines[i].ResetLastBPM(BPMManager.Instance.GetBPMWithSecondsTime(((float)ProgressManager.Instance.CurrentTime)));
        //}
        //Chart.Instance.Refresh();
    }
}
