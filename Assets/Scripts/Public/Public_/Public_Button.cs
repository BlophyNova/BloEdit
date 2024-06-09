using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Public_Button : MonoBehaviour
{
    public Button thisButton;
    private void Start()
    {
        thisButton = GetComponent<Button>();
        OnStart();
    }
    public virtual void OnStart() { }
}
