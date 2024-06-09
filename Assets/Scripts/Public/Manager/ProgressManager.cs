using Blophy.Chart;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ProgressManager : MonoBehaviourSingleton<ProgressManager>
{
    public Stopwatch musicPlayerTime = new();//计时器，谱面时间和音乐时间的
    public double dsp_StartPlayMusic;//开始时间
    public double dsp_LastPlayMusic;//上一次暂停后的时间
    public double Offset => Chart.Instance.chartData.globalData.offset;//偏移
    public double skipTime = 0;//时间跳转

    public float playSpeed = 1;
    public double CurrentTime => musicPlayerTime.ElapsedMilliseconds * playSpeed / 1000d + skipTime;//当前时间

    public void SetPlaySpeed(float playSpeed)
    {
        this.playSpeed = playSpeed;
        AssetManager.Instance.musicPlayer.pitch = playSpeed;
    }
    /// <summary>
    /// 开始播放
    /// </summary>
    /// <param name="offset">偏移</param>
    public void StartPlay()
    {
        dsp_StartPlayMusic = AudioSettings.dspTime + Offset;//获取到开始播放的时间
        dsp_LastPlayMusic = dsp_StartPlayMusic;//同步LastPlayMusic
        AssetManager.Instance.musicPlayer.PlayScheduled(dsp_StartPlayMusic);//在绝对的时间线上播放
        musicPlayerTime.Start();//开始计时
    }
    /// <summary>
    /// 暂停播放
    /// </summary>
    public void PausePlay()
    {
        dsp_LastPlayMusic = AudioSettings.dspTime + Offset;//更新暂停时候的时间
        StopMusic();//暂停播放音乐
    }
    /// <summary>
    /// 继续播放音乐
    /// </summary>
    public void ContinuePlay()
    {
        AssetManager.Instance.musicPlayer.UnPause();//播放器解除暂停状态
        musicPlayerTime.Start();//音乐播放器的时间开始播放
    }

    /// <summary>
    /// 暂停时间
    /// </summary>
    void StopMusic()
    {
        Debug.Log("ExeStop");
        AssetManager.Instance.musicPlayer.Pause();
        musicPlayerTime.Stop();
    }
    /// <summary>
    /// 跳转时间
    /// </summary>
    /// <param name="time">跳转到哪里</param>
    public void SetTime(double time)
    {
        double timeDelta = Math.Round(time - CurrentTime, 3);
        OffsetTime(timeDelta);
    }
    /// <summary>
    /// 在当前时间的基础上加或者减时间
    /// </summary>
    /// <param name="time">加多少或者减多少时间</param>
    public void OffsetTime(double time)
    {
        skipTime += time;

        float currentMusicPlayerTime = (float)(CurrentTime - Offset);
        if (currentMusicPlayerTime >= 0)
            AssetManager.Instance.musicPlayer.time = currentMusicPlayerTime;
        else
        {
            //AssetManager.Instance.musicPlayer.Pause();
            AssetManager.Instance.musicPlayer.SetScheduledStartTime(AudioSettings.dspTime - currentMusicPlayerTime);
            AssetManager.Instance.musicPlayer.time = 0;
        }
        ResetAllLineNoteState();
    }
    /// <summary>
    /// 重置时间
    /// </summary>
    public void ResetTime()
    {
        Debug.Log("ExeResetTime");
        skipTime = 0;
        musicPlayerTime.Reset();
        //AssetManager.Instance.musicPlayer.time = (float)CurrentTime;
        AssetManager.Instance.musicPlayer.time = 0;

    }
    /// <summary>
    /// 让时间重新开始计算
    /// </summary>
    public void RestartTime()
    {
        skipTime = 0;
        musicPlayerTime.Restart();
        AssetManager.Instance.musicPlayer.time = (float)CurrentTime;
        StartPlay();
    }
    void ResetAllLineNoteState()
    {
        for (int i = 0; i < SpeckleManager.Instance.allLineNoteControllers.Count; i++)
        {
            ResetLineNoteState(ref SpeckleManager.Instance.allLineNoteControllers[i].lastOnlineIndex,
                SpeckleManager.Instance.allLineNoteControllers[i].ariseOnlineNotes,
                SpeckleManager.Instance.allLineNoteControllers[i].endTime_ariseOnlineNotes,
                SpeckleManager.Instance.allLineNoteControllers[i].decideLineController,
                SpeckleManager.Instance.allLineNoteControllers[i].decideLineController.ThisLine.onlineNotes, true);

            ResetLineNoteState(ref SpeckleManager.Instance.allLineNoteControllers[i].lastOfflineIndex,
                SpeckleManager.Instance.allLineNoteControllers[i].ariseOfflineNotes,
                SpeckleManager.Instance.allLineNoteControllers[i].endTime_ariseOfflineNotes,
                SpeckleManager.Instance.allLineNoteControllers[i].decideLineController,
                SpeckleManager.Instance.allLineNoteControllers[i].decideLineController.ThisLine.offlineNotes, false);
        }
    }
    public void ResetLineNoteState(ref int lastIndex, List<NoteController> ariseLineNotes, List<NoteController> endTime_ariseLineNotes, DecideLineController decideLine, List<Note> notes, bool isOnlineNote)
    {
        lastIndex = 0;
        for (int i = 0; i < notes.Count; i++)
        {
            if (notes[i].hitTime < CurrentTime)
            {
                lastIndex++;
            }
            else break;
        }
        for (int i = 0; i < ariseLineNotes.Count; i++)
        {
            NoteController note = ariseLineNotes[i];
            decideLine.ReturnNote(note, note.thisNote.noteType, isOnlineNote);
        }
        ariseLineNotes.Clear();
        endTime_ariseLineNotes.Clear();
    }
}
