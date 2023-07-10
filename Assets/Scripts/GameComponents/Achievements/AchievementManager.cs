using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> AllAchievements = new List<Achievement>();

    public List<string> ColletedAchievementsTags = new List<string>();

    [SerializeField]
    private Sprite defaultSprite;
    public Sprite DefaultSprite => defaultSprite;

    public event Action<Achievement> OnAchievementColleted;

    private void Start()
    {
        ColletedAchievementsTags = GameManager.Instance.SaveLoad.LoadAchievements();
    }

    public void CollectAchievement(string tag)
    {
        if (!AllAchievements.Any(a => a.Tag == tag))
        {
            Debug.LogError("ƒостижени€ с таким тего не существует!");
            return;
        }

        if (ColletedAchievementsTags.Contains(tag))
            return;

        Achievement achievement = AllAchievements.First(a => a.Tag == tag);

        ColletedAchievementsTags.Add(achievement.Tag);

        OnAchievementColleted?.Invoke(achievement);
    }

    private void OnApplicationQuit()
    {
        GameManager.Instance.SaveLoad.SaveAchievements();
    }
}
