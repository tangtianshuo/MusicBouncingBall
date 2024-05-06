using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MusicEventCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public ArrayList trackEvents;

    private AudioSource audioSource;

    public MusicTrackPoint[] trackPoints;


    public MusicTrackPoint track;

    void Start()

    {
        trackEvents = new ArrayList();
        audioSource = GetComponent<AudioSource>();
        track = new MusicTrackPoint();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    track.checkPoint.Add(audioSource.time);
        //    Debug.Log(audioSource.time);
        //}

        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    foreach (float time in track.checkPoint)
        //    {
        //        Debug.Log(time);
        //    }
        //    CreateEventFile();
        //}
    }
    public string JsonPath()
    {
            return Application.streamingAssetsPath + "/MusicTrack.json";

    }
    private void CreateEventFile()
    {
          //如果本地没有对应的json 文件，重新创建
        if (!File.Exists(JsonPath()))
        {
            File.Create(JsonPath());
        }
        string json  = JsonUtility.ToJson(track,true);

        File.WriteAllText(JsonPath(),json);
        
    }

}
