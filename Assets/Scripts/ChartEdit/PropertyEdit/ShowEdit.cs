using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEdit : MonoBehaviourSingleton<ShowEdit>
{
    public Button thisButton;
    public GameObject edit;
    public bool isOn = true;
    private void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(() =>
        {
            Show();
        });
    }

    public void Show()
    {
        if (isOn) isOn = false;
        else isOn = true;
        edit.SetActive(isOn);
    }
}
