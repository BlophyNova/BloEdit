using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPlayer : ShortcutKeyEvent
{
    public override void ExeDown()
    {
        if (StateManager.Instance.IsPlaying)
        {
            ShowEdit.Instance.edit.SetActive(true);
            Pause.Instance.PauseBack();
        }
        else
        {
            ShowEdit.Instance.edit.SetActive(false);
            Play.Instance.PlayBack();
        }
    }
}
