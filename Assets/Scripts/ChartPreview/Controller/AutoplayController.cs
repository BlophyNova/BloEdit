using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoplayController : MonoBehaviourSingleton<AutoplayController>
{
    public SpeckleManager speckleManager;
    public float currentH;
    private void Start()
    {
        speckleManager.enabled = false;
    }
    private void Update()
    {
        foreach (LineNoteController lineNoteController in speckleManager.allLineNoteControllers)
        {
            FindPassHitTimeNotes(lineNoteController.ariseOnlineNotes);
            FindPassHitTimeNotes(lineNoteController.ariseOfflineNotes);
        }
        if (currentH >= 1) currentH = 0;
        currentH += Time.deltaTime;
        ValueManager.Instance.perfectJudge = Color.HSVToRGB(currentH, 1, 1);
    }
    /// <summary>
    /// 音符过了打击时间但是没有Miss掉的这个期间每一帧调用
    /// </summary>
    /// <param name="ariseNotes">需要调用的列表</param>
    void FindPassHitTimeNotes(List<NoteController> ariseNotes)
    {
        if (ariseNotes.Count <= 0) return;
        int end_index = Algorithm.BinarySearch(ariseNotes, m => m.thisNote.hitTime < ProgressManager.Instance.CurrentTime + .00001, false);//寻找音符过了打击时间但是没有Miss掉的音符
        for (int i = end_index - 1; i >= 0; i--)
        {
            ariseNotes[i].Judge();
        }
    }
}
