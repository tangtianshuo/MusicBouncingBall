/*
 * 作者: 唐天硕
 * 创建: 2024-05-22 18:53
 * 版权: 国泰新点软件股份有限公司
 *
 * 描述: Midi 音乐解析器，解析midi音乐，根据midi格式的音频，记录节奏点的时间。
 */

using MidiSharp;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MidiSharp.Events;

using MidiSharp.Headers;
public class MidiParser
{


    public Dictionary<string, List<MidiEvent>> ParseMidi(string filePath)
    {
        Dictionary<string, List<MidiEvent>> midiInfo = new();
        List<MidiEvent> notes = new List<MidiEvent>();

        // 读取MIDI文件
        MidiSequence sequence = MidiSequence.Open(File.OpenRead(filePath));

        // 遍历每个轨道
        foreach (MidiTrack track in sequence.Tracks)
        {
            // midiInfo.Add(track.)
            foreach (MidiEvent midiEvent in track.Events)
            {
                notes.Add(midiEvent);

            }
        }

        return midiInfo;
    }
}