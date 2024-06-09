using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    const float is16_9 = 0.5625f;
    public Camera thisCamera;
    private void Start()
    {
        CalculatedScreenArea();
    }
    void CalculatedScreenArea()
    {

        if (thisCamera.pixelHeight / (float)thisCamera.pixelWidth > is16_9)
        {
            //这里放平板的处理方法
            //1024/16*9/768
            HeightManyLong();
        }
        else
        {
            WidthManyLong();
        }
    }
    void WidthManyLong()
    {
        //1080/9*16/2400
        float w = Screen.height / is16_9 / Screen.width;
        float h = 1;
        float x = (1 - w) / 2;
        float y = 0;
        thisCamera.rect = new(x, y, w, h);
    }
    void HeightManyLong()
    {
        float w = 1;
        float h = Screen.width * is16_9 / Screen.height;
        float x = 0;
        float y = (1 - h) / 2;
        thisCamera.rect = new(x, y, w, h);
    }
}
