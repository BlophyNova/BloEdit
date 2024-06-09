using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartMetaData_Button : Public_Button
{
    public override void OnStart()
    {
        thisButton.onClick.AddListener(() =>
        {
            EventsEdit_Info.Instance.boxList.gameObject.SetActive(false);
            EventsEdit_Info.Instance.chartMetaData.gameObject.SetActive(true);
            EventsEdit_Info.Instance.bpmList.gameObject.SetActive(false);
        });
    }
}
