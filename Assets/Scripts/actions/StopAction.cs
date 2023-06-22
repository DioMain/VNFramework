using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAction : ActionBase
{
    public StopAction() : base("Stop")
    {
    }

    public override IEnumerator EventCorotine()
    {
        MainEvent.SetIndex(MainEvent.Actions.Count - 1);

        //MapManager.Instance.EventManager.StopCurrent();

        yield return null;
    }

    public override string GetInfo()
    {
        return "STOP";
    }
}
