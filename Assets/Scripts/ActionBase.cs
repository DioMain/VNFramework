using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ActionBase : ICustomEvent
{
    public GameEvent MainEvent;
    public int ActionIndex;

    public string Name;

    public ActionBase(string name)
    {
        Name = name;
        MainEvent = null;
        ActionIndex = -1;
    }

    public abstract IEnumerator EventCorotine();

    public abstract string GetInfo();

    public virtual string GetHeader()
    {
        return Name;
    }
}

public interface ICustomEvent
{
    public IEnumerator EventCorotine();

    public string GetInfo();
}
