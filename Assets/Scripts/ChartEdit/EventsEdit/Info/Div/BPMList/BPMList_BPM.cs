using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BPMList_BPM : MonoBehaviour
{
    public Blophy.Chart.BPM thisBPM;

    public TMP_InputField startBeats;
    public TMP_InputField bpm;

    public BPMList bpmList;
    void Start()
    {
        startBeats.onValueChanged.AddListener((value) =>
        {
            Match match = Regex.Match(value, @"(\d+):(\d+)/(\d+)");
            if (match.Success)
            {
                thisBPM.integer = int.Parse(match.Groups[1].Value);
                thisBPM.molecule = int.Parse(match.Groups[2].Value);
                thisBPM.denominator = int.Parse(match.Groups[3].Value);
                Chart.Instance.Refresh();
            }
        });
        bpm.onValueChanged.AddListener((value) =>
        {
            if (float.TryParse(value, out float result))
            {
                thisBPM.currentBPM = result;
                Chart.Instance.Refresh();
            }
        });

    }
    public BPMList_BPM Init(Blophy.Chart.BPM bpm, BPMList bpmList)
    {
        this.bpmList = bpmList;
        thisBPM = bpm;
        startBeats.SetTextWithoutNotify($"{bpm.integer}:{bpm.molecule}/{bpm.denominator}");
        this.bpm.SetTextWithoutNotify($"{bpm.currentBPM}");
        return this;
    }
}
