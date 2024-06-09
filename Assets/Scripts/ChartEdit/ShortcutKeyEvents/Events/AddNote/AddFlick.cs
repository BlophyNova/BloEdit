using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFlick : AddNote
{
    public static AddFlick Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<AddFlick>();
        }
        else Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
