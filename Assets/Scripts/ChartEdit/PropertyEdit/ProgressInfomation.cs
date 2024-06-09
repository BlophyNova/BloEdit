using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressInfomation : MonoBehaviourSingleton<ProgressInfomation>
{
    public TextMeshProUGUI thisText;
    private void Update()
    {
        thisText.text = $"当前时间：{(int)(ProgressManager.Instance.CurrentTime / 60):D2}:" +
            $"{(int)(ProgressManager.Instance.CurrentTime - (int)(ProgressManager.Instance.CurrentTime / 60) * 60):D2}:" +
            $"{(int)((ProgressManager.Instance.CurrentTime - (int)ProgressManager.Instance.CurrentTime) * 1000):D3}\t总时间：" +
            $"{(int)(Chart.Instance.chartData.globalData.MusicLength / 60):D2}:" +
            $"{(int)(Chart.Instance.chartData.globalData.MusicLength - (int)(Chart.Instance.chartData.globalData.MusicLength / 60) * 60):D2}:" +
            $"{(int)((Chart.Instance.chartData.globalData.MusicLength - (int)Chart.Instance.chartData.globalData.MusicLength) * 1000):D3}\t当前BPM：" +
            $"{BPMManager.Instance.thisCurrentTotalBPM}\t当前Beats：" +
            $"{BPMManager.Instance.GetBPMSecondsWithSecondsTime((float)ProgressManager.Instance.CurrentTime)}";
    }
}
