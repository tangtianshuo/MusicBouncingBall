using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CubeController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pedal;
    public List<GameObject> cubeList;

    public SphereController sphereController;
    public Transform sphere;

    private int pedalCount;
    public string JsonPath()
    {
        return Application.streamingAssetsPath + "/MusicTrack.json";

    }
    public void Start()
    {
        //Debug.Log(ReadMusicTrack(JsonPath()));
        //// 创建List

        //pedalCreator();
    }

    public void update()
    {
        //foreach (var timePoint in ParseMusicTrack())
        //{
        //    if (String.Format("{0:N3}", Time.time)  == String.Format("{0:N3}", timePoint))
        //    {
        //        CreatePedal();
        //    }
        //}
      
    }
 
    private void CreatePedal()
    {
        cubeList[pedalCount].transform.position= new Vector3(sphere.position.x,sphere.position.y-0.5f,sphere.position.z);
        cubeList[pedalCount].SetActive(true);    
    }
    
    
    public List<GameObject> PedalCreator()
    {
        var musicCheckPoint = FileUtils.ReadCheckPoint<MusicTrackPoint>(FileUtils.JsonPath());
        // x 坐标 = 速度 * 时间间隔
        // y 坐标 =小球当前 Y坐标
        float lastTimePoint = 0;
       float speed =  sphereController.speed;
        List<CheckPoint> musicTrackPoint = musicCheckPoint.checkPoint;
       if (musicTrackPoint == null)
       {
           return null;
       }
       // TODO 同时创建两个小球，把小球运动分解为XY方向运动
       for (var i = 0 ; i < musicTrackPoint.Count ;i++)
       {
           GameObject gameObject = Instantiate(pedal);
           // gameObject.SetActive(false);
           //pedal.transform.position = new Vector3(speed * (musicTrackPoint[i] -lastTimePoint), sphere.transform.position.y-1f,
           //sphere.transform.position.z);
           cubeList.Add(gameObject);


       }

       return cubeList;
    }
    private void Update() {
        //根据时间判断是否生成

    

    }

    private string ReadMusicTrack(string  filePath)
    {
        string fileContent = null;
        if (File.Exists(filePath))
        {
             fileContent = File.ReadAllText(filePath);
        }
        
        
        return fileContent;
        
    }

}
