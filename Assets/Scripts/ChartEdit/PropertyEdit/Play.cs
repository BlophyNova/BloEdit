using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviourSingleton<Play>
{
    public Button thisButton;
    private void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(() =>
        {
            PlayBack();
        });
    }

    public void PlayBack()
    {
        if (!StateManager.Instance.IsPlaying && !StateManager.Instance.IsStart)
        {
            StateManager.Instance.IsStart = true;
        }
        StateManager.Instance.IsPause = false;
    }
}
