using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class Recode : MonoBehaviour
{
    public float startTime;

    private List<float> timeOffsetList;
    void Start()
    {
        timeOffsetList = new List<float>();
        startTime = Time.fixedTime;
        timeOffsetList.Add(startTime);
    }

    void Update()
    {
        float offset;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            float timePin = Time.fixedTime;

            if (timeOffsetList.Count == 0)
            {
                offset = timePin - startTime;
                timeOffsetList.Add(offset);
            }
            else
            {
                offset = timePin - timeOffsetList[timeOffsetList.Count - 1];
                timeOffsetList.Add(offset);
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            InfoList infoList = new InfoList();
            infoList.info = new();
            foreach (var time in timeOffsetList)
            {
                Info info = new();
                info.index = info.index++;
                info.timeOffset = time;
                info.ballInfo = new BallInfo();
                info.jumpPanelInfo = new JumpPanelInfo();
                infoList.info.Add(info);
                Debug.Log(time);
            }
            FileUtils fileUtils = new();
            fileUtils.SaveFile("first", JsonUtility.ToJson(infoList));
        }
    }
}


[Serializable]
public class InfoList
{
    public List<Info> info;
}

[Serializable]
public class Info
{
    public int index;
    public float timeOffset;

    public BallInfo ballInfo;
    public JumpPanelInfo jumpPanelInfo;
}

[Serializable]
public class JumpPanelInfo
{
    public List<float> position;
    public List<float> rotation;
}

[Serializable]
public class BallInfo
{
    public List<float> position;
}