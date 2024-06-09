using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMManager : MonoBehaviourSingleton<BPMManager>
{
    public List<BPM> bpmList => Chart.Instance.chartData.globalData.BPMlist;
    public float CurrentBeatsPerSecond => bpmList[CurrentBPMListIndex].currentBPM / 60;
    public float thisCurrentTotalBPM => bpmList[CurrentBPMListIndex].currentBPM;

    public int CurrentBPMListIndex
    {
        get
        {
            float lastBPMStartTime = 0;
            int index = 0;
            for (; index < bpmList.Count; index++)
            {
                if (index + 1 >= bpmList.Count) break;
                float currentBPMHoldTime = (bpmList[index + 1].thisStartBPM - bpmList[index].thisStartBPM) / (bpmList[index].currentBPM / 60);
                if (currentBPMHoldTime + lastBPMStartTime <= ProgressManager.Instance.CurrentTime) lastBPMStartTime += currentBPMHoldTime;
                else break;
            }
            return index;
        }
    }
    public float GetBPMSecondsWithSecondsTime(float secondsTime)
    {
        float result = (float)(bpmList[GetBPMListIndexWithSecondsTime(secondsTime)].currentBPM / 60 * (secondsTime - GetLastBPMStartTimeWithSecondsTime(secondsTime)) + bpmList[GetBPMListIndexWithSecondsTime(secondsTime)].thisStartBPM);
        return result;
    }
    public BPM GetBPMWithSecondsTime(float SecondsTime)
    {
        int BPMSecondsTime = (int)GetBPMSecondsWithSecondsTime(SecondsTime);
        BPM bpm = new()
        {
            integer = BPMSecondsTime,
            molecule = 0,
            denominator = int.Parse(HorizontalLine.Instance.thisText.text) + 1
        };
        return bpm;
    }
    public int GetBPMListIndexWithSecondsTime(float secondsTime)
    {
        float lastBPMStartTime = 0;
        int index = 0;
        for (; index < bpmList.Count; index++)
        {
            if (index + 1 >= bpmList.Count) break;
            float currentBPMHoldTime = (bpmList[index + 1].thisStartBPM - bpmList[index].thisStartBPM) / (bpmList[index].currentBPM / 60);
            if (currentBPMHoldTime + lastBPMStartTime <= secondsTime) lastBPMStartTime += currentBPMHoldTime;
            else break;
        }
        return index;
    }
    public float GetLastBPMStartTimeWithSecondsTime(float secondsTime)
    {
        float lastBPMStartTime = 0;
        int index = 0;
        for (; index < bpmList.Count; index++)
        {
            if (index + 1 >= bpmList.Count) break;
            float currentBPMHoldTime = (bpmList[index + 1].thisStartBPM - bpmList[index].thisStartBPM) / (bpmList[index].currentBPM / 60);
            if (currentBPMHoldTime + lastBPMStartTime <= secondsTime) lastBPMStartTime += currentBPMHoldTime;
            else break;
        }
        return lastBPMStartTime;
    }
    public float GetSecondsTimeWithBPMSeconds(float BPMSeconds)
    {
        float secondsTime = 0;
        int index = -1;
        for (int i = 0; i < bpmList.Count; i++)
        {
            if (bpmList[i].thisStartBPM <= BPMSeconds) index++;
        }
        for (int i = 0; i < index; i++)
        {
            secondsTime += (bpmList[i + 1].thisStartBPM - bpmList[i].thisStartBPM) / GetBeatsPerSecondWithBPMEvent(bpmList[i]);
        }
        secondsTime += (BPMSeconds - bpmList[index].thisStartBPM) / GetBeatsPerSecondWithBPMEvent(bpmList[index]);
        return secondsTime;
    }
    public float GetBeatsPerSecondWithBPMEvent(BPM bpm)
    {
        return bpm.currentBPM / 60;
    }
    public int GetBPMListIndexWithBPMSeconds(float BPMSeconds)
    {
        int BPMListIndex = 0;
        for (int i = 0; i < bpmList.Count; i++)
        {
            if (bpmList[i].thisStartBPM < BPMSeconds) BPMListIndex++;
            else break;
        }
        return BPMListIndex;
    }
}
