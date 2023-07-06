using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageBoxWriter : TextWriterBase
{
    [SerializeField]
    private GameObject container;
    [SerializeField]
    private GameObject arrow;

    public bool AutoSkip = false;

    public bool IsEntered = false;

    public bool IsShow
    {
        get => container.activeSelf;
        set => container.SetActive(value);
    }

    public event Action<bool> OnAutoSkip;

    public void SetAutoSkip()
    {
        AutoSkip = !AutoSkip;
        OnAutoSkip?.Invoke(AutoSkip);
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

    public override bool SkipCanExecute()
    {
        return AutoSkip;
    }

    public override bool WaitCondition()
    {
        return Input.GetKeyDown(KeyCode.Z) || (IsEntered && Input.GetMouseButtonDown(0));
    }

    public override bool SkipCondition()
    {
        return Input.GetKeyDown(KeyCode.C) || (IsEntered && Input.GetMouseButtonDown(1));
    }

    public void SetMouseEnter(bool enter)
    {
        IsEntered = enter;
    }
}
