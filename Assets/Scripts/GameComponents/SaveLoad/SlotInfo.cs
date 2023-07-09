using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotInfo
{
    [Serializable]
    public struct CharacterInfo
    {
        public CharacterObject.SelectionState State;

        public string Key;
        public string ImageName;

        public Vector2 Position;
        public Vector2 Size;
    }

    public int SlotID;

    public int PipeLineIndex;

    public string BackgroundImageName;

    public string BGMName;
    public float BGMVolume;
    public bool IsBGMPlay;

    public string BGSName;
    public float BGSVolume;
    public bool IsBGSPlay;

    public List<string> IntKeys;
    public List<string> FloatKeys;
    public List<string> BoolKeys;
    public List<string> StringKeys;

    public List<int> IntValues;
    public List<float> FloatValues;
    public List<bool> BoolValues;
    public List<string> StringValues;

    public List<CharacterInfo> Characters;

    public List<HistoryElementInfo> HistoryElements;

    // META

    public bool savedInMA;
}
