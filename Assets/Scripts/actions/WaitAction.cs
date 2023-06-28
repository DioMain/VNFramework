using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAction : ActionBase
{
    public float Time;

    public WaitAction() : base("Wait")
    {
        Time = 1;
    }

    public override IEnumerator EventCorotine()
    {
        yield return new WaitForSeconds(Time);
    }

    public override string GetInfo()
    {
        return $"¬рем€: {Time} сек";
    }

    public override string GetHeader()
    {
        return "∆дать";
    }
}
