using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAlpha : MonoBehaviourSingleton<PlayerAlpha>
{
    public TMP_InputField thisInputField;
    public RawImage player;
    private void Start()
    {
        thisInputField.onValueChanged.AddListener((value) =>
        {
            if (!float.TryParse(value, out float playerAlpha)) return;
            player.color = new(1, 1, 1, playerAlpha);
        });
    }
}
