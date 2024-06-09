using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEdit : ShortcutKeyEvent
{
    public override void ExeDown()
    {
        if (StateManager.Instance.IsPlaying)
        {
            Pause.Instance.PauseBack();
        }
        else
        {
            Play.Instance.PlayBack();
        }
    }
}
