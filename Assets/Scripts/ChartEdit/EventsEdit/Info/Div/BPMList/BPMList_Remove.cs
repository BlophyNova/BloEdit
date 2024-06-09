using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMList_Remove : Public_Button
{
    public BPMList_BPM bpm;
    public override void OnStart()
    {
        thisButton.onClick.AddListener(() =>
        {
            Chart.Instance.chartData.globalData.BPMlist.Remove(bpm.thisBPM);
            Destroy(bpm.gameObject);

            bpm.bpmList.RefreshList();
            Chart.Instance.Refresh();
        });
    }
}
