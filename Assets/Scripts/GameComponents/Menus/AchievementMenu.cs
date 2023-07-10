using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementMenu : GameMenuBase
{
    [SerializeField]
    private List<AchievementElement> elements = new List<AchievementElement>();

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private RectTransform spawnpoint;
    [SerializeField]
    private RectTransform content;

    public float PrefabSizeY;
    public float Margin;

    private void Start()
    {
        Disactivate();
    }

    public override void Activate()
    {
        base.Activate();

        foreach (var element in elements)
            Destroy(element.gameObject);

        elements.Clear();

        float commonSizeY = 0;
        foreach (var item in GameManager.Instance.Achievements.AllAchievements)
        {
            if (item.Hiden && !GameManager.Instance.Achievements.ColletedAchievementsTags.Contains(item.Tag))
                continue;

            GameObject obj = Instantiate(prefab, Vector2.zero, Quaternion.identity, content);

            RectTransform rt = obj.GetComponent<RectTransform>();

            AchievementElement element = obj.GetComponent<AchievementElement>();

            rt.anchoredPosition = new Vector2(spawnpoint.anchoredPosition.x, spawnpoint.anchoredPosition.y - commonSizeY);

            element.Tag = item.Tag;
            element.Init();

            elements.Add(element);

            commonSizeY += PrefabSizeY + Margin;
        }

        content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, commonSizeY + Margin);
    }
}
