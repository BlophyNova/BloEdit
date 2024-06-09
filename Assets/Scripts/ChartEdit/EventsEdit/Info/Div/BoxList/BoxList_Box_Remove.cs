using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxList_Box_Remove : Public_Button
{
    public BoxList_Box box;
    public override void OnStart()
    {
        thisButton.onClick.AddListener(() =>
        {
            SpeckleManager.Instance.allLineNoteControllers.Clear();
            BoxController boxGameObject = box.thisBox;
            BoxManager.Instance.boxes.Remove(boxGameObject);
            Destroy(boxGameObject.gameObject);
            Chart.Instance.chartData.boxes.Remove(box.boxChart);
            Chart.Instance.chartEdit.boxesEdit.Remove(box.boxEdit);
            BoxManager.Instance.RefreshList();
            box.boxList.RefreshList();
            Chart.Instance.Refresh();
        });
    }
}
