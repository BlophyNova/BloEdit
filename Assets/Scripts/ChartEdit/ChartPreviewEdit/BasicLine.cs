using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasicLine : MonoBehaviour
{
    public TextMeshProUGUI thisText;
    private void Update()
    {
        thisText.text = $"{BPMManager.Instance.GetBPMSecondsWithSecondsTime(((float)ProgressManager.Instance.CurrentTime)):F2}";
    }
}
