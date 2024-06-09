using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VLines : MonoBehaviour
{
    public VLine edgeLeftVerticalLine;
    public VLine edgeRightVerticalLine;
    public List<VLine> middleLines = new();
    public virtual void AddLineOrRemoveLine(int addOrRemove, string thisText)
    {
        for (int i = 0; i < middleLines.Count; i++)
            Destroy(middleLines[i].gameObject);
        middleLines.Clear();
        int middleLinesCount = int.Parse(thisText) + addOrRemove;
        float leftRightDelta = (edgeRightVerticalLine.transform.localPosition - edgeLeftVerticalLine.transform.localPosition).x / (middleLinesCount + 1);
        for (int i = 1; i < middleLinesCount + 1; i++)
        {
            VLine instLine = Instantiate(edgeLeftVerticalLine, transform);
            instLine.transform.localPosition = new Vector3(edgeLeftVerticalLine.transform.localPosition.x + leftRightDelta * i, edgeLeftVerticalLine.transform.localPosition.y);
            float positionX = -1 + (float)(1 - -1) / (middleLinesCount + 1) * i;
            instLine.PositionX = positionX;
            middleLines.Add(instLine);
        }
    }
}
