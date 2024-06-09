using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviourSingleton<Pause>
{
    public Button thisButton;
    private void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(() =>
        {
            PauseBack();

        });
    }

    public void PauseBack()
    {
        StateManager.Instance.IsPause = true;
    }
}
