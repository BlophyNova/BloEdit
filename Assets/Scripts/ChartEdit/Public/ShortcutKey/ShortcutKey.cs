using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutKey : MonoBehaviour
{
    public KeyCode keyCode;
    public KeyCode keyCode2;
    public Action keyDown;
    public Action keyHold;
    public Action keyUp;
    public bool isDoublePress;
    public ShortcutKey Init(KeyCode keyCode, KeyCode keyCode2, bool isDoublePress, Action keyDown, Action keyHold, Action keyUp)
    {
        this.keyCode = keyCode;
        this.keyCode2 = keyCode2;
        this.isDoublePress = isDoublePress;
        this.keyDown = keyDown;
        this.keyHold = keyHold;
        this.keyUp = keyUp;
        return this;
    }
    private void Update()
    {
        if (isDoublePress)//多押
        {
            if (Input.GetKey(keyCode) && Input.GetKeyDown(keyCode2))
            {
                keyDown();
            }
            if (Input.GetKey(keyCode) && Input.GetKey(keyCode2))
            {
                keyHold();
            }
            if (Input.GetKey(keyCode) && Input.GetKeyUp(keyCode2))
            {
                keyUp();
            }
        }
        else
        {
            //单压
            if (Input.GetKeyDown(keyCode))
            {
                keyDown();
            }
            if (Input.GetKey(keyCode))
            {
                keyHold();
            }
            if (Input.GetKeyUp(keyCode))
            {
                keyUp();
            }
        }
    }
}
