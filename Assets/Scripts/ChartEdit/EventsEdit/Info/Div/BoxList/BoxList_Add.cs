using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blophy.Chart;
public class BoxList_Add : Public_Button
{
    public BoxList boxList;
    public override void OnStart()
    {
        thisButton.onClick.AddListener(() =>
        {
            SpeckleManager.Instance.allLineNoteControllers.Clear();
            Chart.Instance.chartData.boxes.Add(ChartTools.NewBox());
            Chart.Instance.chartEdit.boxesEdit.Add(ChartTools.CreateNewBoxEdit());
            BoxManager.Instance.RefreshList();
            boxList.RefreshList();
            Chart.Instance.Refresh();
        });
    }
}
