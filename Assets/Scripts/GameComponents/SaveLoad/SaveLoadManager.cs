using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [Header("Буфферы для загрузки:")]
    public List<Sprite> BackgroundImages = new List<Sprite>();
    public List<Sprite> CharacterImages = new List<Sprite>();

    public List<AudioClip> BackgroundMusics = new List<AudioClip>();
    public List<AudioClip> BackgroundSounds = new List<AudioClip>();

    public const string ConfigFileName = "GameConfig.cfg";

    public const string SlotFileName = "Slot";
    public const string SlotFileEx = ".armen";

    public bool SlotIsExists(int id)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{SlotFileName}{id}{SlotFileEx}");

        return File.Exists(path);
    }

    public SlotInfo GetSlot(int id)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{SlotFileName}{id}{SlotFileEx}");

        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);

        return JsonUtility.FromJson<SlotInfo>(json);
    }

    public SlotInfo CreateSlot(int id)
    {
        SlotInfo info = new SlotInfo()
        {
            SlotID = id,
            PipeLineIndex = 0,
            BackgroundImageName = "Весна",
            BGMName = "BGM",
            BGMVolume = 1,
            BGSName = "BGS",
            BGSVolume = 0.5f,
            BoolKeys = new List<string>(),
            IntKeys = new List<string>(),
            FloatKeys = new List<string>(),
            StringKeys = new List<string>(),
            BoolValues = new List<bool>(),
            IntValues = new List<int>(),
            StringValues = new List<string>(),
            FloatValues = new List<float>(),
            Characters = new List<SlotInfo.CharacterInfo>()
        };

        info.BoolKeys.Add("b");
        info.IntKeys.Add("i");
        info.FloatKeys.Add("f");
        info.StringKeys.Add("s");

        info.BoolValues.Add(true);
        info.IntValues.Add(1);
        info.FloatValues.Add(1.1f);
        info.StringValues.Add("str");

        info.Characters.Add(new SlotInfo.CharacterInfo()
        {
            Key = "keu",
            ImageName = "Andrey",
            Position = new Vector2(0, 0),
            Size = new Vector2(1, 1),
            State = CharacterObject.SelectionState.Select
        });

        return info;
    }

    public void SaveSlot(int id)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{SlotFileName}{id}{SlotFileEx}");

        if (File.Exists(path))
            File.Delete(path);

        string json = JsonUtility.ToJson(CreateSlot(id), true);

        File.WriteAllText(path, json);
    }

    public void DeleteSlot(int id)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{SlotFileName}{id}{SlotFileEx}");

        if (File.Exists(path))
            File.Delete(path);
    }

    public ConfigInfo GetConfig()
    {
        string path = Path.Combine(Application.persistentDataPath, ConfigFileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            return JsonUtility.FromJson<ConfigInfo>(json);
        }
        else
        {
            ConfigInfo info = CreateConfig();

            SaveConfig(info);

            return info;
        }
    }

    public void SaveConfig(ConfigInfo config)
    {
        string path = Path.Combine(Application.persistentDataPath, ConfigFileName);

        if (File.Exists(path))
            File.Delete(path);

        string json = JsonUtility.ToJson(config, true);

        File.WriteAllText(path, json);
    }

    public ConfigInfo CreateConfig()
    {
        Resolution max = Screen.resolutions.Reverse().ToArray()[0];

        ConfigInfo info = new ConfigInfo()
        {
            BGMVolume = 2,
            BGSVolume = 2,
            SEVolume = 2,
            IsFullScreen = true,
            Resolution = new Vector2Int(max.width, max.height),
        };

        return info;
    }
}

[Serializable]
public class ConfigInfo
{
    public float BGMVolume;
    public float BGSVolume;
    public float SEVolume;

    public bool IsFullScreen;

    public Vector2Int Resolution;
}
