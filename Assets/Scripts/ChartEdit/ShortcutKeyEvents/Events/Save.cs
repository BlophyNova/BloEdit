using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Save : ShortcutKeyEvent
{
    public override void ExeDown()
    {
        string chartEdit = JsonConvert.SerializeObject(Chart.Instance.chartEdit);
        File.WriteAllText($"{Application.dataPath}/chartEdit.json", chartEdit);
    }
}
