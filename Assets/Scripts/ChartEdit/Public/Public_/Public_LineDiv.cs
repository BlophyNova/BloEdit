using Blophy.Chart;
using Blophy.ChartEdit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Public_LineDiv : MonoBehaviour
{
    public Transform arisePosition;//出现位置的Transform组件
    public BeatLine beatLine;//节拍线的预制件
    public List<BeatLine> beatLines = new();//节拍线游戏物体管理列表
    public BPM lastBPM;//上一次的BPM
    public RectTransform notesCanvas;//NoteEdit的画布
    public RectTransform beatLinesCanvas;//节拍线的画布
    public VLines vLines;//竖线的总监

    public List<NoteEdit> noteEdits;//用来追踪所有NoteEdit的列表
    public bool isLocal;//是否执行本地线框的索引
    public int localBoxNumber;//本地框索引
    public int localLineNumber;//本地线索引
    public float ariseLineAndLinePositionYDelta;
    public float ariseLineAndLineSeconds => ariseLineAndLinePositionYDelta / 100;
    public float CurrentBasicLine => YScale.Instance.GetPositionYWithSecondsTime((float)ProgressManager.Instance.CurrentTime);
    public float CurrentAriseLine => YScale.Instance.GetPositionYWithSecondsTime((float)ProgressManager.Instance.CurrentTime) + ariseLineAndLinePositionYDelta;

    public int lastAriseBeatLineIndex = 0;
    public virtual void RefreshNoteEdits()
    {
        for (int i = 0; i < noteEdits.Count; i++)
        {
            NoteEdit noteEdit = noteEdits[i];
            Destroy(noteEdit.gameObject);
        }
        noteEdits.Clear();
        int exeBoxNumber;
        int exeLineNumber;
        if (isLocal)
        {
            exeBoxNumber = localBoxNumber;
            exeLineNumber = localLineNumber;
        }
        else
        {
            exeBoxNumber = int.Parse(BoxNumber.Instance.thisText.text) - 1;
            exeLineNumber = int.Parse(LineNumber.Instance.thisText.text) - 1;
        }
        List<Blophy.ChartEdit.Note> notesOnThisCanvas = Chart.Instance.chartEdit.boxesEdit[exeBoxNumber].lines[exeLineNumber].onlineNotes;
        for (int i = 0; i < notesOnThisCanvas.Count; i++)
        {
            NoteEdit instNote = Instantiate(notesOnThisCanvas[i].noteType switch
            {
                NoteType.Tap => AddTap.Instance.thisNote,
                NoteType.Hold => AddHold.Instance.thisNote,
                NoteType.Drag => AddDrag.Instance.thisNote,
                NoteType.Flick => AddFlick.Instance.thisNote,
                NoteType.Point => AddPoint.Instance.thisNote,
                NoteType.FullFlickPink => AddFullFlick.Instance.thisNote,
                NoteType.FullFlickBlue => AddFullFlick.Instance.thisNote,
                _ => throw new System.Exception("哼哼啊啊啊啊啊啊啊啊啊1145141919810，你知道为什么报错嘛？哼哼啊啊啊啊啊啊啊啊啊，报错的原因是，没有找到音符类型哼哼啊啊啊啊啊啊啊啊啊")

            }, Vector2.zero, Quaternion.identity, notesCanvas.transform);
            //NoteEdit note = notesOnThisCanvas[i].noteType switch
            //{
            //    NoteType.Tap => AddTap.Instance.thisNote,
            //    NoteType.Hold => AddHold.Instance.thisNote,
            //    NoteType.Drag => AddDrag.Instance.thisNote,
            //    NoteType.Flick => AddFlick.Instance.thisNote,
            //    NoteType.Point => AddPoint.Instance.thisNote,
            //    NoteType.FullFlickPink => AddFullFlick.Instance.thisNote,
            //    NoteType.FullFlickBlue => AddFullFlick.Instance.thisNote,
            //    _ => throw new System.Exception("哼哼啊啊啊啊啊啊啊啊啊1145141919810，你知道为什么报错嘛？哼哼啊啊啊啊啊啊啊啊啊，报错的原因是，没有找到音符类型哼哼啊啊啊啊啊啊啊啊啊")
            //};
            instNote.thisNote = notesOnThisCanvas[i];
            instNote.IsRefresh().Init(instNote.thisNote.hitTime, instNote.thisNote.positionX, this, notesOnThisCanvas);
            //NoteEdit instNote = Instantiate(note, Vector2.zero, Quaternion.identity, notesCanvas.transform).IsRefresh().Init(note.thisNote.hitTime, note.thisNote.positionX, this);
            noteEdits.Add(instNote);
        }
        ChartTools.EditNote2ChartDataNote(
        Chart.Instance.chartData.boxes[exeBoxNumber].lines[exeLineNumber],
        Chart.Instance.chartEdit.boxesEdit[exeBoxNumber].lines[exeLineNumber].onlineNotes);
    }
    private void Update()
    {
        UpdateBeatLines();
    }
    private void UpdateBeatLines()
    {
        transform.localPosition = Vector2.down * CurrentBasicLine;
        arisePosition.localPosition = Vector2.up * CurrentAriseLine;
        float ariseBPMSeconds = BPMManager.Instance.GetBPMSecondsWithSecondsTime((float)(ProgressManager.Instance.CurrentTime + ariseLineAndLineSeconds * (1 / YScale.Instance.CurrentYScale)));
        int ariseBeatLineIndex = Algorithm.BinarySearch(BPMManager.Instance.bpmList, m => m.thisStartBPM < ariseBPMSeconds, false);
        while (lastBPM.thisStartBPM < ariseBPMSeconds)
        {
            BeatLine initBeatLine = Instantiate(beatLine, beatLinesCanvas.transform).Init(lastBPM.thisStartBPM, lastBPM);
            beatLines.Add(initBeatLine);
            lastBPM.AddOneBeat();
        }
        float BPMSeconds = BPMManager.Instance.GetBPMSecondsWithSecondsTime((float)ProgressManager.Instance.CurrentTime);
        //int beatLinesCount = beatLines.Count;
        for (int i = 0; i < beatLines.Count; i++)
        {
            while (beatLines[i].thisBPM.thisStartBPM < BPMSeconds)
            {
                BeatLine thisBeatLine = beatLines[i];
                beatLines.Remove(thisBeatLine);
                Destroy(thisBeatLine.gameObject);
            }
            while (beatLines[i].thisBPM.thisStartBPM > ariseBPMSeconds)
            {
                BeatLine thisBeatLine = beatLines[i];
                beatLines.Remove(thisBeatLine);
                Destroy(thisBeatLine.gameObject);
            }
        }
    }

    public void ResetLastBPM(int integer = 0, int molecule = 0, int denominator = 1)
    {
        lastBPM.integer = integer;
        lastBPM.molecule = molecule;
        lastBPM.denominator = denominator;
    }
    public void ResetLastBPM(BPM bpm)
    {
        lastBPM = new(bpm);
    }
}
