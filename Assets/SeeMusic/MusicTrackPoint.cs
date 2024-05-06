using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音轨打点实体类
/// </summary>
[SerializeField]
public class MusicTrackPoint
{
    public List<CheckPoint> checkPoint = new List<CheckPoint>();
}

[SerializeField]
public class CheckPoint
{
    /// <summary>
    /// 当前点位对应帧数 从音乐播放时开始计算
    /// </summary>
    public int frameIndex;

    /// <summary>
    /// 音乐点位时间节点
    /// </summary>
    public float musicTime;

    /// <summary>
    /// 系统总运行时间
    /// </summary>
    public float systemTime;
}