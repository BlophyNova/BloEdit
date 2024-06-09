using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMList_Add : Public_Button
{
    public BPMList bpmList;
    public override void OnStart()
    {
        thisButton.onClick.AddListener(() =>
        {
            Chart.Instance.chartData.globalData.BPMlist.Add(new(Chart.Instance.chartData.globalData.BPMlist[^1]));
            bpmList.RefreshList();
            Chart.Instance.Refresh();
        });
    }

}
