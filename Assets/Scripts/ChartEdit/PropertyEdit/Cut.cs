using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cut : MonoBehaviour
{
    public Button thisButton;
    private void Start()
    {
        thisButton = GetComponent<Button>();
    }
}
