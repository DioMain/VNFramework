using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementObject : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private AudioSource m_AudioSource;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI tittle;
    [SerializeField]
    private TextMeshProUGUI description;

    private void Start()
    {
        GameManager.Instance.Achievements.OnAchievementColleted += Achievements_OnAchievementColleted;
    }

    private void Achievements_OnAchievementColleted(Achievement obj)
    {
        icon.sprite = obj.Icon;

        tittle.text = obj.Title; 
        description.text = obj.Description;

        m_Animator.SetTrigger("Invoke");
        m_AudioSource.Play();
    }

    private void OnDestroy()
    {
        GameManager.Instance.Achievements.OnAchievementColleted -= Achievements_OnAchievementColleted;
    }
}
