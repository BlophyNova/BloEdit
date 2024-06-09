using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChartPreviewEdit : MonoBehaviourSingleton<ChartPreviewEdit>
{
    public List<Public_LineDiv> noteLines;
    public List<Public_LineDiv> eventLines;
    public List<Public_LineDiv> lines
    {
        get
        {
            List<Public_LineDiv> list = new();
            foreach (var item in noteLines)
            {
                list.Add(item);
            }
            foreach (var item in eventLines)
            {
                list.Add(item);
            }
            return list;
        }
    }
    public static void DestoryAllBeatLines()
    {
        DestoryAllBeatLines(Instance.noteLines);
        DestoryAllBeatLines(Instance.eventLines);
    }
    public static void DestoryAllBeatLines(List<Public_LineDiv> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].beatLines.Count; j++)
            {
                Destroy(lines[i].beatLines[j].gameObject);
            }
            lines[i].beatLines.Clear();
            lines[i].lastBPM = BPMManager.Instance.GetBPMWithSecondsTime(((float)ProgressManager.Instance.CurrentTime));
        }
    }
}
