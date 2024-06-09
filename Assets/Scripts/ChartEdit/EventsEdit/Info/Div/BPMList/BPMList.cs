using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMList : MonoBehaviour
{
    public Transform parent;
    public BPMList_BPM bpm;
    public List<BPMList_BPM> bpmList_bpm;
    private void OnEnable()
    {
        StartCoroutine(UpdateBoxList());
    }

    public void RefreshList()
    {
        StartCoroutine(UpdateBoxList());
    }
    private IEnumerator UpdateBoxList()
    {
        yield return new WaitForEndOfFrame();
        int count = bpmList_bpm.Count;
        for (int i = 0; i < count; i++)
        {
            BPMList_BPM bpm = bpmList_bpm[0];
            bpmList_bpm.Remove(bpm);
            Destroy(bpm.gameObject);
        }
        List<Blophy.Chart.BPM> bpmList = Chart.Instance.chartData.globalData.BPMlist;
        for (int i = 0; i < bpmList.Count; i++)
        {
            BPMList_BPM instBPM = Instantiate(bpm, parent).Init(bpmList[i], this);
            instBPM.thisBPM = bpmList[i];
            bpmList_bpm.Add(instBPM);
        }
    }
}
