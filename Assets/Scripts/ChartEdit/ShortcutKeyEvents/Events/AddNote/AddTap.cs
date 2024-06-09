using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTap : AddNote
{
    public static AddTap Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<AddTap>();
        }
        else Destroy(this);
    }
    void Start()
    {

    }
    void Update()
    {

    }
}
