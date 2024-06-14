using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MusicBouncingBallData
{
    public List<BouncingData> data;
    public MusicBouncingBallData()
    {
        data = new List<BouncingData>();
    }
}

[Serializable]

public class BouncingData
{

    public float timeOffset;

    public MusicBouncingBallVector ballV;

    public MusicBouncingBallVector panelPosition;

    public MusicBouncingBallVector panelRotation;
    public BouncingData(float timeOffset, Vector2 ballV, Vector2 panelPosition, Vector2 panelRotation)
    {
        this.timeOffset = timeOffset;
        this.ballV = new(ballV);
        this.panelPosition = new(panelPosition);
        this.panelRotation = new(panelRotation);

    }

}

[Serializable]
public class MusicBouncingBallVector
{
    public float x;
    public float y;

    public MusicBouncingBallVector(Vector2 vec)
    {
        this.x = vec.x;
        this.y = vec.y;
    }

}
