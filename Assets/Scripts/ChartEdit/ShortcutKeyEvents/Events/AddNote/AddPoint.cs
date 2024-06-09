using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPoint : AddNote
{
    public static AddPoint Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<AddPoint>();
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
