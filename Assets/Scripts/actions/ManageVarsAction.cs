using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageVarsAction : ActionBase
{
    public enum OperationType
    {
        Equal, Add
    }
    public enum VarType
    {
        Bool, Int, Float, String
    }

    public OperationType OType;
    public VarType VType;

    public bool BoolValue;
    public int IntValue;
    public float FloatValue;
    public string StringValue;

    public string Key;

    public ManageVarsAction() : base("ManageVars")
    {
        OType = OperationType.Equal;
        VType = VarType.Bool;

        BoolValue = false;
        IntValue = 0;
        FloatValue = 0;
        StringValue = string.Empty;

        Key = string.Empty;
    }

    public override IEnumerator EventCorotine()
    {
        switch (VType)
        {
            case VarType.Bool:
                if (!GameManager.Instance.Data.BoolValues.ContainsKey(Key))
                    GameManager.Instance.Data.BoolValues.Add(Key, false);

                GameManager.Instance.Data.BoolValues[Key] = BoolValue;
                break;
            case VarType.Int:
                if (!GameManager.Instance.Data.IntValues.ContainsKey(Key))
                    GameManager.Instance.Data.IntValues.Add(Key, 0);

                if (OType == OperationType.Equal)
                    GameManager.Instance.Data.IntValues[Key] = IntValue;
                else
                    GameManager.Instance.Data.IntValues[Key] += IntValue;
                break;
            case VarType.Float:
                if (!GameManager.Instance.Data.FloatValues.ContainsKey(Key))
                    GameManager.Instance.Data.FloatValues.Add(Key, 0);

                if (OType == OperationType.Equal)
                    GameManager.Instance.Data.FloatValues[Key] = FloatValue;
                else
                    GameManager.Instance.Data.FloatValues[Key] += FloatValue;
                break;
            case VarType.String:
                if (!GameManager.Instance.Data.StringValues.ContainsKey(Key))
                    GameManager.Instance.Data.StringValues.Add(Key, string.Empty);

                GameManager.Instance.Data.StringValues[Key] = StringValue;
                break;
        }

        yield return null;
    }

    public override string GetInfo()
    {
        switch (VType)
        {
            case VarType.Float:
                if (OType == OperationType.Equal)
                    return $"Float, KEY: {Key} to VALUE: {FloatValue}";
                else
                    return $"Float, KEY: {Key} add VALUE: {FloatValue}";
            case VarType.String:
                return $"String, KEY: {Key} to VALUE: {StringValue}";
            case VarType.Bool:
                return $"Bool, KEY: {Key} to VALUE: {BoolValue}";
            case VarType.Int:
                if (OType == OperationType.Equal)
                    return $"Int, KEY: {Key} to VALUE: {IntValue}";
                else
                    return $"int, KEY: {Key} add VALUE: {IntValue}";
            default:
                return "NULL";
        }
    }
}
