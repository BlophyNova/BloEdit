using Blophy.ChartEdit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxList_Box : MonoBehaviour
{
    public TextMeshProUGUI thisBoxIndex;
    public TextMeshProUGUI currentState1;
    public TextMeshProUGUI currentState2;

    public BoxController thisBox;
    public Blophy.Chart.Box boxChart;
    public Blophy.ChartEdit.Box boxEdit;
    public BoxList boxList;
    public BoxList_Box Init(int thisBoxIndex, BoxController thisBox, Blophy.Chart.Box boxChart, Blophy.ChartEdit.Box boxEdit, BoxList boxList)
    {
        //this.thisBoxIndex.text = $"{thisBoxIndex + 1}";
        this.boxChart = boxChart;
        this.boxEdit = boxEdit;
        this.boxList = boxList;
        StartCoroutine(UpdateCurrentState(thisBox));
        return this;
    }
    IEnumerator UpdateCurrentState(BoxController thisBox)
    {
        while (gameObject.activeInHierarchy)
        {
            currentState1.text = $"spd:{thisBox.box.lines[0].career.Evaluate(((float)ProgressManager.Instance.CurrentTime))}\n" +
                $"cetx:{thisBox.currentCenterX}\n" +
                $"cety:{thisBox.currentCenterY}\n" +
                $"mvx:{thisBox.currentMoveX}\n" +
                $"mvy:{thisBox.currentMoveY}";
            currentState2.text = $"slex:{thisBox.currentScaleX}\n" +
                $"sley:{thisBox.currentScaleY}\n" +
                $"rot:{thisBox.currentRotate}\n" +
                $"alp:{thisBox.currentAlpha}\n" +
                $"lalp:{thisBox.currentLineAlpha}";
            yield return new WaitForEndOfFrame();
        }
    }
}
