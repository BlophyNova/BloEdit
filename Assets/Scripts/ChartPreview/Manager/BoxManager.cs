using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviourSingleton<BoxManager>
{
    public List<BoxController> boxes = new();
    private void Start()
    {
        RefreshList();
        AssetManager.Instance.box.gameObject.SetActive(true);//激活所有方框
        //StateManager.Instance.IsStart = true;
        //StateManager.Instance.IsPause = true;
    }
    public void RefreshList()
    {
        SpeckleManager.Instance.allLineNoteControllers.Clear();
        int count = boxes.Count;
        for (int i = 0; i < count; i++)
        {
            var box = boxes[i];
            Destroy(box.gameObject);
        }
        boxes.Clear();
        for (int i = 0; i < Chart.Instance.chartData.boxes.Count; i++)
        {
            BoxController box = Instantiate(AssetManager.Instance.boxController, AssetManager.Instance.box)
                .SetSortSeed(i * ValueManager.Instance.noteRendererOrder, i + 1)//这里的3是每一层分为三小层，第一层是方框渲染层，第二和三层是音符渲染层，有些音符占用两个渲染层，例如Hold，FullFlick
                .Init(Chart.Instance.chartData.boxes[i]);
            boxes.Add(box);
        }

    }
}
