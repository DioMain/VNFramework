using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameBox : MonoBehaviour, IGameInited
{
    [SerializeField]
    private GameObject container;
    [SerializeField]
    private TextMeshProUGUI meshProUGUI;

    public bool IsShow
    {
        get => container.activeSelf;
        set
        {
            container.SetActive(value);
        }
    }

    public string Name
    {
        get => meshProUGUI.text;
        set
        {
            meshProUGUI.text = value;
            container.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal, meshProUGUI.GetPreferredValues().x + 26);
        }
    }

    public void Init()
    {
        
    }
}
