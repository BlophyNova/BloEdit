using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPlayPlayer : ShortcutKeyEvent
{
    public override void ExeDown()
    {
        ShowEdit.Instance.edit.SetActive(false);
        Play.Instance.PlayBack();
    }
    public override void ExeUp()
    {
        ShowEdit.Instance.edit.SetActive(true);
        Pause.Instance.PauseBack();
    }
}
