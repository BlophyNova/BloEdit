using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : NoteController
{
    /// <summary>
    /// 被成功判定后调用
    /// </summary>
    /// <param name="currentTime"></param>
    public override void Judge(double currentTime, TouchPhase touchPhase)
    {
        isJudged = true;//修改属性为成功判定
        CompletedJudge();
        base.Judge(currentTime, touchPhase);//执行基类的判定方法
    }
    public override void Judge()
    {
        isJudged = true;
        CompletedJudge();
        base.Judge();
    }
}
