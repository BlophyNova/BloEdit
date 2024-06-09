using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blophy.ChartEdit;
using System;

public class Chart : MonoBehaviourSingleton<Chart>
{
    public ChartData chartData;
    [Header("下面是制谱器专属的格式")]
    public ChartEdit chartEdit;
    public List<Blophy.ChartEdit.Text> textEdit;
    public void RefreshPlayer() => ProgressManager.Instance.OffsetTime(0);
    public void RefreshChart()
    {
        ChartPreviewEdit.DestoryAllBeatLines();
        for (int i = 0; i < ChartPreviewEdit.Instance.noteLines.Count; i++)
        {
            ChartPreviewEdit.Instance.noteLines[i].RefreshNoteEdits();
        }
        for (int i = 0; i < ChartPreviewEdit.Instance.eventLines.Count; i++)
        {
            ChartPreviewEdit.Instance.eventLines[i].RefreshNoteEdits();
            Debug.Log("刷新了事件编辑");
        }
    }
    public void Refresh(bool isNoteEditing = false)
    {
        if (isNoteEditing)
        {
            ChartPreviewEdit.DestoryAllBeatLines();
            //for (int i = 0; i < ChartPreviewEdit.Instance.eventLines.Count; i++)
            //{
            //    ChartPreviewEdit.Instance.eventLines[i].RefreshNoteEdits();
            //    Debug.Log("刷新了事件编辑");
            //}
            for (int i = 0; i < ChartPreviewEdit.Instance.noteLines.Count; i++)
            {
                ChartPreviewEdit.Instance.noteLines[i].RefreshNoteEdits();
            }
            Instance.RefreshPlayer();
        }
        else
        {
            Instance.RefreshChart();
            Instance.RefreshPlayer();
        }
    }
}
[Serializable]
public class ChartEdit
{
    public List<Blophy.ChartEdit.Box> boxesEdit;
    public GlobalData globalData
    {
        get
        {
            return Chart.Instance.chartData.globalData;
        }
        set
        {
            Chart.Instance.chartData.globalData = value;
        }
    }
    public MetaData metaData
    {
        get
        {
            return Chart.Instance.chartData.metaData;
        }
        set
        {
            Chart.Instance.chartData.metaData = value;
        }
    }

}