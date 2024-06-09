using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Copy : MonoBehaviour
{
    public Button thisButton;
    private void Start()
    {
        thisButton = GetComponent<Button>();
    }
}
