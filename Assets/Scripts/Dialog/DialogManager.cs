using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour, IGameInited
{
    [SerializeField]
    private MessageBoxWriter message;
    [SerializeField]
    private ChoiseManager choise;
    [SerializeField]
    private NameBox nameBox;

    public bool IsWrite => message.IsWrite;
    public bool IsChoise => choise.IsChosing;

    public int ChoiseResult => choise.LastChoiseResult;


    /// <summary>
    /// IsWrite or IsChoise
    /// </summary>
    public bool IsUsing => IsWrite || IsChoise;

    public void Init()
    {
        message.Init();
        choise.Init();
        nameBox.Init();

        HideAll();
    }

    public void ShowMessageBox(string text, bool clear = true, bool wait = true, float speed = 0)
    {
        if (!IsWrite)
        {
            message.IsShow = true;
            nameBox.IsShow = false;

            message.Write(text, clear, wait, speed);
        }
    }
    public void ShowMessageBox(MessageBoxInfo info)
    {
        if (!IsWrite)
        {
            message.IsShow = true;

            nameBox.IsShow = !string.IsNullOrEmpty(info.Name);
            nameBox.Name = info.Name;

            message.Write(info.Text, info.Clear, info.Wait, info.Speed);
        }
    }

    public void ShowChoiseBox(params string[] choises)
    {
        if (!choise.IsChosing)
        {
            choise.IsShow = true;

            choise.InvokeChoise(choises);
        }
    }

    public void HideBox()
    {
        message.IsShow = false;
        nameBox.IsShow = false;
    }
    public void HideChoise()
    {
        choise.IsShow = false;
    }
    public void HideAll()
    {
        message.IsShow = false;
        nameBox.IsShow = false;
        choise.IsShow = false;
    }
}

[Serializable]
public struct MessageBoxInfo
{
    [TextArea]
    [Multiline(4)]
    public string Text;
    public string Name;

    public bool Clear;
    public bool Wait;

    public float Speed;

    public MessageBoxInfo(string text)
    {
        Text = text;
        Name = string.Empty;

        Clear = true;
        Wait = true;

        Speed = 0;
    }
    public MessageBoxInfo(string text, bool clear = true, bool wait = true)
    {
        Text = text;
        Name = string.Empty;

        Clear = clear;
        Wait = wait;

        Speed = 0;
    }
    public MessageBoxInfo(string text, string name, bool clear = true, bool wait = true)
    {
        Text = text;
        Name = name;

        Clear = clear;
        Wait = wait;

        Speed = 0;
    }
}
