using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MessageAction : ActionBase
{
    public string Text;
    public string CharName;

    public float Speed;

    public bool Wait;
    public bool Clear;
    public bool CloseAfter;

    public MessageAction() : base("Message")
    {
        Text = "";
        CharName = "";

        Speed = 0;
        Wait = true;
        Clear = true;
        CloseAfter = false;
    }

    public override IEnumerator EventCorotine()
    {
        MessageBoxInfo info = new MessageBoxInfo()
        {
            Text = Text,
            Speed = Speed,
            Name = CharName,
            Clear = Clear,
            Wait = Wait
        };

        MapManager.Instance.Dialog.ShowMessageBox(info);

        yield return new WaitWhile(() => MapManager.Instance.Dialog.IsUsing);

        if (CloseAfter)
            MapManager.Instance.Dialog.HideBox();
    }

    public override string GetInfo()
    {
        string onhide = CloseAfter ? ", Çàêğûòü" : "";

        string outputed = Text.Replace("\n", "\n\t ");

        return $"\n\t\"{outputed}\"\nÈÌß: {CharName}, ÆÄÀÒÜ?: {Wait}, Î×ÈÑÒÈÒÜ?: {Clear}{onhide}";
    }

    public override string GetHeader()
    {
        return "Ñîîáùåíèå";
    }
}
