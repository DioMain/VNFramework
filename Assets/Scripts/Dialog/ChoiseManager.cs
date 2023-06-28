using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChoiseManager : MonoBehaviour, IGameInited
{
    private const float minHeight = 54f;
    private const float oneHeight = 74f;

    [SerializeField]
    private ChoiseObject[] choiseObjects = new ChoiseObject[5];

    [SerializeField]
    private GameObject container;

    [SerializeField]
    private bool isChosing = false;
    public bool IsChosing => isChosing;

    [SerializeField]
    private int lastChoiseResult = -1;
    public int LastChoiseResult => lastChoiseResult;

    [SerializeField]
    private int choiseId = 0;
    [SerializeField]
    private int countOfChoising = -1;

    public bool IsShow
    {
        get => container.activeSelf;
        set => container.SetActive(value);
    }

    public void Init()
    {

    }

    /// <param name="strings">To 5 params</param>
    public void InvokeChoise(params string[] strings)
    {
        container.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical, minHeight + oneHeight * strings.Length);

        countOfChoising = strings.Length > 5 ? 5 : strings.Length;
        choiseId = 0;

        foreach (var item in choiseObjects)
        {
            item.gameObject.SetActive(false);
        }
        for (int i = 0; i < countOfChoising; i++)
        {
            choiseObjects[i].IsChoised = false;
            choiseObjects[i].gameObject.SetActive(true);
            choiseObjects[i].TextMesh.text = strings[i];
        }

        float maxchars = choiseObjects.Max(i => i.TextMesh.GetPreferredValues().x);

        container.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, maxchars + 36 * 2);

        SetChoised(0);

        isChosing = true;
    }

    private void Update()
    {
        if (isChosing)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SetChoised(choiseId + 1);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {


                SetChoised(choiseId - 1);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Submit();
            }
        }
    }

    public void Submit()
    {
        lastChoiseResult = choiseId;
        isChosing = false;
    }

    public void SetChoised(int id)
    {
        if (id > countOfChoising - 1)
            choiseId = 0;
        else if (id < 0)
            choiseId = countOfChoising - 1;
        else
            choiseId = id;

        for (int i = 0; i < choiseObjects.Length; i++)
        {
            if (i == choiseId)
                choiseObjects[i].IsChoised = true;
            else
                choiseObjects[i].IsChoised = false;
        }
    }
    public void SetChoised(ChoiseObject choise)
    {
        for (int i = 0; i < choiseObjects.Length; i++)
        {
            if (choiseObjects[i] == choise)
                choiseObjects[i].IsChoised = true;
            else
                choiseObjects[i].IsChoised = false;
        }
    }
}
