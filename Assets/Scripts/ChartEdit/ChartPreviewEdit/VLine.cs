using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VLine : MonoBehaviour
{
    public VLines vLines;
    public float CanvasX => Mathf.Abs((vLines.edgeRightVerticalLine.transform.localPosition - vLines.edgeLeftVerticalLine.transform.localPosition).x) * positionX;//这跟竖线相对于画布的X轴坐标，是从-1-1转换过来的
    public float positionX;
    public float PositionX
    {
        get => positionX;
        set
        {
            positionX = value;
            Debug.Log(CanvasX);
        }
    }
}
