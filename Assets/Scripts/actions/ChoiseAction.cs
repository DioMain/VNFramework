using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiseAction : ActionBase
{
    public string[] Choises;

    public ChoiseAction() : base("ChoiseEvent")
    {
        Choises = new string[5];
    }

    public override IEnumerator EventCorotine()
    {
        List<string> list = new();

        foreach (string choice in Choises)
        {
            if (!string.IsNullOrEmpty(choice)) 
                list.Add(choice);
        }

        MapManager.Instance.Dialog.ShowChoiseBox(list.ToArray());

        yield return new WaitWhile(() => MapManager.Instance.Dialog.IsChoise);

        MapManager.Instance.Dialog.HideChoise();
    }

    public override string GetInfo()
    {
        string str = "";

        int index = 0;
        foreach (string choice in Choises)
        {
            if (!string.IsNullOrEmpty(choice))
                str += $"CHOICE {index}: {choice}; ";

            index++;
        }

        return str;
    }
}
