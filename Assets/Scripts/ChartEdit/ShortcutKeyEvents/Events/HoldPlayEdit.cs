using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPlayEdit : ShortcutKeyEvent
{
    public override void ExeDown()
    {
        Play.Instance.PlayBack();
    }
    public override void ExeUp()
    {
        Pause.Instance.PauseBack();
    }
}
