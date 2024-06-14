using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileUtils
{

    readonly private string filePath = "./Assets/MusicBouncingBall2/Data";

    public string GetFilePath(string fileName)
    {
        return filePath + "/" + fileName + ".json";
    }
    public void SaveFile(string fileName, string content)
    {
        if (!File.Exists(GetFilePath(fileName)))
        {

            File.Create(GetFilePath(fileName)).Dispose();
            File.WriteAllText(GetFilePath(fileName), content);
        }
        else
        {
            Debug.Log(File.ReadAllText(GetFilePath(fileName)));
            File.Delete(GetFilePath(fileName));
            File.Create(GetFilePath(fileName)).Dispose();
            File.WriteAllText(GetFilePath(fileName), content);
        }

    }
    public string ReadFile(string fileName)
    {
        if (File.Exists(GetFilePath(fileName)))
        {
            return File.ReadAllText(GetFilePath(fileName));
        }
        else
        {
            throw new FileNotFoundException("未找到文件::" + GetFilePath(fileName));
        }

    }
}
