using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [Header("¡ÛÙÙÂ˚ ‰Îˇ Á‡„ÛÁÍË:")]
    public List<Sprite> BackgroundImages = new List<Sprite>();
    public List<Sprite> CharacterImages = new List<Sprite>();

    public List<AudioClip> BackgroundMusics = new List<AudioClip>();
    public List<AudioClip> BackgroundSounds = new List<AudioClip>();

    public const string ConfigFileName = "GameConfig.cfg";
    public const string AchievementsFIleName = "Achievements.armenachiv";

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
        if (MapManager.Instance == null)
        {
            Debug.LogWarning("Map manager is not exists!");
            return new SlotInfo();
        }

        SlotInfo info = new SlotInfo()
        {
            SlotID = id,
            BackgroundImageName = MapManager.Instance.BackgroundImage.CurrentImage.name,
            BGMName = GameManager.Instance.Audio.BGMClip.name,
            BGMVolume = GameManager.Instance.Audio.BGMVolume,
            IsBGMPlay = GameManager.Instance.Audio.BGMPlaying,
            BGSName = GameManager.Instance.Audio.BGSClip.name,
            BGSVolume = GameManager.Instance.Audio.BGSVolume,
            IsBGSPlay = GameManager.Instance.Audio.BGSPlaying,
            BoolKeys = new List<string>(),
            IntKeys = new List<string>(),
            FloatKeys = new List<string>(),
            StringKeys = new List<string>(),
            BoolValues = new List<bool>(),
            IntValues = new List<int>(),
            StringValues = new List<string>(),
            FloatValues = new List<float>(),
            Characters = new List<SlotInfo.CharacterInfo>(),
            HistoryElements = new List<HistoryElementInfo>()
        };

        int pindex = MapManager.Instance.Event.Event.Index;

        if (MapManager.Instance.Event.Event.Actions[pindex] is MessageAction)
        {
            info.PipeLineIndex = MapManager.Instance.Event.Event.Index;
            info.savedInMA = true;
        }
            
        else
        {
            for (int i = pindex - 1; i > 0; i++)
            {
                if (MapManager.Instance.Event.Event.Actions[i] is MessageAction ma)
                {
                    if (ma.CloseAfter)
                    {
                        info.PipeLineIndex = MapManager.Instance.Event.Event.Index;
                        info.savedInMA = false;
                    }                       
                    else
                    {
                        info.PipeLineIndex = i;
                        info.savedInMA = true;
                    }
                        

                    break;
                }
            }
        }

        foreach (var item in GameManager.Instance.Data.BoolValues.Keys)
            info.BoolKeys.Add(item);
        foreach (var item in GameManager.Instance.Data.BoolValues.Values)
            info.BoolValues.Add(item);

        foreach (var item in GameManager.Instance.Data.IntValues.Keys)
            info.IntKeys.Add(item);
        foreach (var item in GameManager.Instance.Data.IntValues.Values)
            info.IntValues.Add(item);

        foreach (var item in GameManager.Instance.Data.FloatValues.Keys)
            info.FloatKeys.Add(item);
        foreach (var item in GameManager.Instance.Data.FloatValues.Values)
            info.FloatValues.Add(item);

        foreach (var item in GameManager.Instance.Data.StringValues.Keys)
            info.StringKeys.Add(item);
        foreach (var item in GameManager.Instance.Data.StringValues.Values)
            info.StringValues.Add(item);

        string[] chkeys = MapManager.Instance.Characters.Characters.Keys.ToArray();

        for (int i = 0; i < MapManager.Instance.Characters.Characters.Count; i++)
        {
            info.Characters.Add(new SlotInfo.CharacterInfo()
            {
                ImageName = MapManager.Instance.Characters.Characters[chkeys[i]].OwnSprite.name,
                Key = chkeys[i],
                State = MapManager.Instance.Characters.Characters[chkeys[i]].State,
                Size = MapManager.Instance.Characters.Characters[chkeys[i]].Size,
                Position = MapManager.Instance.Characters.Characters[chkeys[i]].Position
            });
        }

        for (int i = 0; i < MapManager.Instance.History.historyElements.Count + (info.savedInMA ? -1 : 0); i++)
            info.HistoryElements.Add(MapManager.Instance.History.historyElements[i]);

        return info;
    }

    public void ApplySlot(int id)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{SlotFileName}{id}{SlotFileEx}");

        if (!File.Exists(path))
        {
            Debug.LogError($"CRITICAL ERROR: Slot is not exists! [SLOT ID: {id}]");

            return;
        }

        SlotInfo slot = GetSlot(id);

        MapManager.Instance.Event.Event.SetIndex(slot.PipeLineIndex, false);

        Sprite bg = null;

        try
        {
            bg = BackgroundImages.First(i => i.name == slot.BackgroundImageName);
        }
        catch { }

        if (bg == null)
            Debug.LogError("»ÁÓ·‡ÊÂÌËÂ ÌÂ Ì‡È‰ÂÌÓ ‚ ÔÛÎÂ ÒÓı‡ÌÂÌËˇ!\n" +
                "GameController -> SaveLoadManager -> BackgroundImages\n" +
                "»«Ã≈Õ≈Õ»ﬂ œ–Œ»«¬Œƒ»“‹ “ŒÀ‹ Œ ¬ œ–≈‘¿¡≈!");

        MapManager.Instance.BackgroundImage.SetImage(bg);

        AudioClip bgm = null;
        AudioClip bgs = null;

        try
        {
            bgm = BackgroundMusics.First(i => i.name == slot.BGMName);
            bgs = BackgroundSounds.First(i => i.name == slot.BGSName);
        }
        catch { }


        if (bgm == null)
            Debug.LogError("BGM ÌÂ Ì‡È‰ÂÌÓ ‚ ÔÛÎÂ ÒÓı‡ÌÂÌËˇ!\n" +
                "GameController -> SaveLoadManager -> BackgroundMusics\n" +
                "»«Ã≈Õ≈Õ»ﬂ œ–Œ»«¬Œƒ»“‹ “ŒÀ‹ Œ ¬ œ–≈‘¿¡≈!");

        if (bgs == null)
            Debug.LogError("BGS ÌÂ Ì‡È‰ÂÌÓ ‚ ÔÛÎÂ ÒÓı‡ÌÂÌËˇ!\n" +
                "GameController -> SaveLoadManager -> BackgroundSounds\n" +
                "»«Ã≈Õ≈Õ»ﬂ œ–Œ»«¬Œƒ»“‹ “ŒÀ‹ Œ ¬ œ–≈‘¿¡≈!");

        GameManager.Instance.Audio.SetBGM(bgm, slot.IsBGMPlay);
        GameManager.Instance.Audio.BGMVolume = slot.BGMVolume;

        GameManager.Instance.Audio.SetBGS(bgs, slot.IsBGSPlay);
        GameManager.Instance.Audio.BGSVolume = slot.BGSVolume;

        for (int i = 0; i < slot.BoolKeys.Count; i++)
            GameManager.Instance.Data.BoolValues.Add(slot.BoolKeys[i], slot.BoolValues[i]);

        for (int i = 0; i < slot.IntKeys.Count; i++)
            GameManager.Instance.Data.IntValues.Add(slot.IntKeys[i], slot.IntValues[i]);

        for (int i = 0; i < slot.FloatKeys.Count; i++)
            GameManager.Instance.Data.FloatValues.Add(slot.FloatKeys[i], slot.FloatValues[i]);

        for (int i = 0; i < slot.StringKeys.Count; i++)
            GameManager.Instance.Data.StringValues.Add(slot.StringKeys[i], slot.StringValues[i]);

        foreach (var item in slot.Characters)
        {
            Sprite charsprite = null;
            try
            {
                charsprite = CharacterImages.First(i => i.name == item.ImageName);
            }
            catch { }

           

            if (charsprite == null)
                Debug.LogError("»ÁÓ·‡ÊÂÌËÂ ÔÂÒÓÌ‡Ê‡ ÌÂ Ì‡È‰ÂÌÓ ‚ ÔÛÎÂ ÒÓı‡ÌÂÌËˇ!\n" +
                    "GameController -> SaveLoadManager -> CharacterImages\n" +
                    "»«Ã≈Õ≈Õ»ﬂ œ–Œ»«¬Œƒ»“‹ “ŒÀ‹ Œ ¬ œ–≈‘¿¡≈!");

            MapManager.Instance.Characters.AddCharacter(item.Key, charsprite, item.Position, item.Size, false);

            MapManager.Instance.Characters.SetCharacterState(item.Key, item.State);
        }

        foreach (var item in slot.HistoryElements)
            MapManager.Instance.History.AddHistoryInfo(item);

    }

    public void GameCleanup()
    {
        if (MapManager.Instance == null)
        {
            Debug.LogWarning("Map manager is not exists!");
            return;
        }

        MapManager.Instance.Event.Event.SetIndex(0);
        MapManager.Instance.BackgroundImage.SetImage(MapManager.Instance.BackgroundImage.DefaultImage);
        GameManager.Instance.Audio.StopBGM();
        GameManager.Instance.Audio.StopBGS();

        GameManager.Instance.Data.BoolValues.Clear();
        GameManager.Instance.Data.IntValues.Clear();
        GameManager.Instance.Data.FloatValues.Clear();
        GameManager.Instance.Data.StringValues.Clear();

        MapManager.Instance.Characters.Cleanup();

        MapManager.Instance.History.historyElements.Clear();
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
            RefreshRate = max.refreshRateRatio.value,
            Resolution = new Vector2Int(max.width, max.height)
        };

        return info;
    }

    public List<string> LoadAchievements()
    {
        string path = Path.Combine(Application.persistentDataPath, AchievementsFIleName);

        if (!File.Exists(path))
            return new List<string>();

        AchievementsInfo info = JsonUtility.FromJson<AchievementsInfo>(File.ReadAllText(path));

        return info.Tags;
    }

    public void SaveAchievements()
    {
        string path = Path.Combine(Application.persistentDataPath, AchievementsFIleName);

        if (File.Exists(path))
            File.Delete(path);

        AchievementsInfo info = new AchievementsInfo()
        {
            Tags = GameManager.Instance.Achievements.ColletedAchievementsTags
        };

        File.WriteAllText(path, JsonUtility.ToJson(info, true));
    }
}

[Serializable]
public class AchievementsInfo
{
    public List<string> Tags;
}

[Serializable]
public class ConfigInfo
{
    public float BGMVolume;
    public float BGSVolume;
    public float SEVolume;

    public bool IsFullScreen;

    public Vector2Int Resolution;
    public double RefreshRate;
}
