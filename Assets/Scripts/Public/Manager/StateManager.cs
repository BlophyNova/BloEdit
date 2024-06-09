using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviourSingleton<StateManager>
{
    public bool _isStart = false;//已经开始
    public bool _isEnd = false;//已经结束
    public bool _isPause = false;//已经暂停
    public bool IsStart
    {
        get => _isStart;
        set
        {
            if (_isStart) return;//如果已经开始了就直接返回
            _isStart = value;//设置状态为开始
            ProgressManager.Instance.StartPlay();//谱面开始播放
            AssetManager.Instance.box.gameObject.SetActive(true);//激活所有方框

        }
    }
    public bool IsEnd
    {
        get => _isEnd;
        set => _isEnd = value;
    }
    public bool IsPause
    {
        get => _isPause;
        set
        {
            _isPause = value;
            switch (value)
            {
                case true:
                    ProgressManager.Instance.PausePlay();
                    break;
                case false:
                    ProgressManager.Instance.ContinuePlay();
                    break;
            }
        }
    }
    public bool IsPlaying => IsStart && !IsPause && !IsEnd;//正在播放中，判定方法为：已经开始并且没有暂停没有结束

    public static void RestartTime(bool isContinuePlay)
    {
        Instance._isStart = false;
        Instance._isPause = false;
        Instance._isEnd = false;
        ProgressManager.Instance.ResetTime();

        if (isContinuePlay)
        {
            Instance.IsStart = true;
        }
    }
}
