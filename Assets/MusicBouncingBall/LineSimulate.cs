using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LineSimulate : MonoBehaviour
{
    public LineRenderer confirmLine;

    void Awake()
    {
        confirmLine = GetComponent<LineRenderer>();
        confirmLine.positionCount = 0;
        EventManager.Instance.RecordConfirmLine += RecordConfirmLine;
    }
    public void RecordConfirmLine(List<Vector3> confirmLinePosition)
    {
        foreach (var position in confirmLinePosition)
        {
            confirmLine.positionCount++;
            confirmLine.SetPosition(confirmLine.positionCount - 1, position);
        }

    }
}
