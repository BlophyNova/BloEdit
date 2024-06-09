using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxList : MonoBehaviour
{
    public Transform parent;
    public BoxList_Box box;
    public List<BoxList_Box> boxList_box;
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
        int count = boxList_box.Count;
        for (int i = 0; i < count; i++)
        {
            BoxList_Box box = boxList_box[0];
            boxList_box.Remove(box);
            Destroy(box.gameObject);
        }
        for (int i = 0; i < BoxManager.Instance.boxes.Count; i++)
        {
            BoxList_Box instBox = Instantiate(box, parent).Init(i, BoxManager.Instance.boxes[i], Chart.Instance.chartData.boxes[i], Chart.Instance.chartEdit.boxesEdit[i], this);
            instBox.thisBoxIndex.text = (i + 1).ToString();
            instBox.thisBox = BoxManager.Instance.boxes[i];
            boxList_box.Add(instBox);
        }
    }
}
