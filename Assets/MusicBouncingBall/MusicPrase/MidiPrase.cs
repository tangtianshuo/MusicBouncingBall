/*
 * 作者: 唐天硕
 * 创建: 2024-05-22 18:53
 * 版权: 国泰新点软件股份有限公司
 *
 * 描述: Midi 音乐解析器，解析midi音乐，根据midi格式的音频，记录节奏点的时间。
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiPrase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private List<float> rhythmPoints;

    void Awake()
    {
        rhythmPoints = new List<float>();
        ParseMidiFile("path_to_midi_file.mid");
    }

    void ParseMidiFile(string filePath)
    {

    }
}
