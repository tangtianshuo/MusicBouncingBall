using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public static class FileUtils 
{
    public static string JsonPath()
    {
        return Application.streamingAssetsPath + "/MusicTrack.json";
    }

    public static void WriteCheckPoint<T>(T track)
    {
        //如果本地没有对应的json 文件，重新创建
        if (!File.Exists(JsonPath()))
        {
            File.Create(JsonPath());
        }
        string json = JsonUtility.ToJson(track, true);

        File.WriteAllText(JsonPath(), json);
    }

    public static T ReadCheckPoint<T>(string path)
    {
        T entity  = default(T);
        if (!File.Exists(path))
        {
            Debug.Log("文件不存在");
            return entity;
        }
        else
        {
            string json = File.ReadAllText(path);

           entity = JsonUtility.FromJson<T>(json);
            return entity;
        }

       
    
    }

}
