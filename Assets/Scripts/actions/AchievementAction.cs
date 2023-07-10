using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementAction : ActionBase
{
    public string AchievementTag;

    public AchievementAction() : base("CollectAchievement")
    {
        AchievementTag = string.Empty;
    }

    public override IEnumerator EventCorotine()
    {
        yield return null;

        GameManager.Instance.Achievements.CollectAchievement(AchievementTag);
    }

    public override string GetInfo()
    {
        return $"Тег: {AchievementTag}";
    }

    public override string GetHeader()
    {
        return $"Засчитать достижение";
    }
}
