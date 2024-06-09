using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutKeyEvent : MonoBehaviour
{
    public string eventID;
    public virtual void ExeDown() { }
    public virtual void Exe() { }
    public virtual void ExeUp() { }
}
