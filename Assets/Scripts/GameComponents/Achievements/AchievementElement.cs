using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementElement : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI tittle;
    [SerializeField]
    private TextMeshProUGUI description;

    public string Tag;

    private bool isColleted;

    public void Init()
    {
        isColleted = GameManager.Instance.Achievements.ColletedAchievementsTags.Contains(Tag);

        Achievement achievement = GameManager.Instance.Achievements.AllAchievements.First(achievement => achievement.Tag == Tag);

        if (isColleted)
        {
            icon.sprite = achievement.Icon;
            tittle.text = achievement.Title;
            description.text = achievement.Description;
        }
        else
        {
            icon.sprite = GameManager.Instance.Achievements.DefaultSprite;
            tittle.text = achievement.Condition;
            description.text = "";
        }
    }
}
