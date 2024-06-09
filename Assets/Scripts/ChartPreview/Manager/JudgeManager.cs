using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeManager : MonoBehaviourSingleton<JudgeManager>
{
    public static float perfect = .08f;//完美判定±80ms
    public static float good = .16f;//Good判定±160ms
    public static float bad = .24f;//Bad判定±240ms
}
