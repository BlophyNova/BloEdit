using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VerticalLine : MonoBehaviourSingleton<VerticalLine>
{
    public TextMeshProUGUI thisText;
    public Button u;
    public Button d;
    public List<VLines> vLines;
    public List<VLines> eventVLines;
    private void Start()
    {
        u.onClick.AddListener(() =>
        {
            for (int i = 0; i < vLines.Count; i++)
            {
                vLines[i].AddLineOrRemoveLine(1, thisText.text);
            }
            thisText.text = $"{int.Parse(thisText.text) + 1}";
        });
        d.onClick.AddListener(() =>
        {
            for (int i = 0; i < vLines.Count; i++)
            {
                vLines[i].AddLineOrRemoveLine(-1, thisText.text);
            }
            thisText.text = $"{int.Parse(thisText.text) - 1}";
        });
        for (int i = 0; i < eventVLines.Count; i++)
        {
            eventVLines[i].AddLineOrRemoveLine(0, thisText.text);
        }
        for (int i = 0; i < vLines.Count; i++)
        {
            vLines[i].AddLineOrRemoveLine(0, thisText.text);
        }
    }

}
