using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPointRecoder : MonoBehaviour
{
    // Start is called before the first frame update
    public PianoKeysDetector detector;

    private List<EventPoint> recordPoints;

    /// <summary>
    /// 音乐开始时间
    /// </summary>
    public Time musicStartTime;

    /// <summary>
    ///  记录当前事件节点信息
    /// </summary>
    public void Recoder(EventPoint eventPoint)
    {
        recordPoints.Add(eventPoint);
    }
}


[Serializable]
public class EventPoint
{
    /// <summary>
    /// 音高
    /// </summary>
    public int pitch;

    /// <summary>
    /// 节点
    /// </summary>
    public float key;

    /// <summary>
    /// 当前帧
    /// </summary>
    public int frame;

    /// <summary>
    /// 开始时间与当前节点时间的偏移量
    /// </summary>
    public Time musicTimePoint;
}
