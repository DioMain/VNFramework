using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoxWriter : TextWriterBase
{
    [SerializeField]
    private GameObject container;
    [SerializeField]
    private GameObject arrow;

    public bool IsShow
    {
        get => container.activeSelf;
        set => container.SetActive(value);
    }

    public override void Init()
    {
        arrow.SetActive(false);
    }

    public override void OnWaitStart()
    {
        arrow.SetActive(true);
    }
    public override void OnWaitEnd()
    {
        arrow.SetActive(false);
    }

    public override void OnEveryLetter(char letter)
    {

    }

    public override void OnSpecialSituation(string text, TextWriterSS situation)
    {
        
    }

    public override void OnWriteEnd()
    {

    }

    public override void OnWriteStart()
    {
        
    }
}
