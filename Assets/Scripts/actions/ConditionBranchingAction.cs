using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ConditionBranchingAction : ActionBase
{
    public enum ConditionType
    {
        If, ElseIf, Else, EndIf
    }

    public ConditionType Type;

    public int Index;

    public bool CompareVariable, CompareChoise;

    private bool compareVariableResult, compareChoiseResult;

    public string VariableKey;
    public int VariableType;

    public string VariableStringValue;
    public int VariableIntValue;
    public float VariableFloatValue;
    public bool VariableBoolValue;

    public int ChoiseExpected;

    public bool IsTrue { get; private set; }

    public ConditionBranchingAction() : base("ConditionBranching")
    {
        IsTrue = false;
        Index = 0;
        Type = ConditionType.If;
        CompareVariable = false; CompareChoise = false;
        compareVariableResult = false; compareChoiseResult = false;
        VariableKey = ""; VariableType = 0;
        VariableStringValue = "" ; VariableIntValue = 0; VariableFloatValue = 0; VariableBoolValue = false;
        ChoiseExpected = 0;
    }

    private bool Check()
    {
        if (CompareVariable)
        {
            switch (VariableType)
            {
                case 0:
                    if (!GameManager.Instance.Data.StringValues.ContainsKey(VariableKey))
                        break;

                    compareVariableResult = GameManager.Instance.Data.StringValues[VariableKey] == VariableStringValue;
                    break;
                case 1:
                    if (!GameManager.Instance.Data.IntValues.ContainsKey(VariableKey))
                        break;

                    compareVariableResult = GameManager.Instance.Data.IntValues[VariableKey] == VariableIntValue;
                    break;
                case 2:
                    if (!GameManager.Instance.Data.FloatValues.ContainsKey(VariableKey))
                        break;

                    compareVariableResult = GameManager.Instance.Data.FloatValues[VariableKey] == VariableFloatValue;
                    break;
                case 3:
                    if (!GameManager.Instance.Data.BoolValues.ContainsKey(VariableKey))
                        break;

                    compareVariableResult = GameManager.Instance.Data.BoolValues[VariableKey] == VariableBoolValue;
                    break;
            }
        }

        if (CompareChoise)
        {
            compareChoiseResult = MapManager.Instance.Dialog.ChoiseResult == ChoiseExpected;
        }

        return ((CompareVariable && compareVariableResult) || !CompareVariable) &&
                 ((CompareChoise && compareChoiseResult) || !CompareChoise);
    }

    public override IEnumerator EventCorotine()
    {
        if (Type == ConditionType.If)
        {
            MainEvent.ConditionEvents.Add(this);

            IsTrue = Check();

            if (!IsTrue)
            {
                for (int i = MainEvent.Index + 1; i < MainEvent.Events.Count; i++)
                {
                    if (MainEvent.Events[i] is ConditionBranchingAction evn)
                    {
                        if (evn.Index == Index &&
                            (evn.Type == ConditionType.EndIf
                            || evn.Type == ConditionType.Else
                            || evn.Type == ConditionType.ElseIf))
                        {
                            MainEvent.SetIndex(i);
                            break;
                        }
                    }
                }
            }
        }
        else if (Type == ConditionType.ElseIf)
        {
            ConditionBranchingAction ifevent = MainEvent.ConditionEvents.Last();

            IsTrue = Check() && ifevent.IsTrue == false;

            if (!IsTrue)
            {
                for (int i = MainEvent.Index + 1; i < MainEvent.Events.Count; i++)
                {
                    if (MainEvent.Events[i] is ConditionBranchingAction evn)
                    {
                        if (evn.Index == Index &&
                            (evn.Type == ConditionType.EndIf
                            || evn.Type == ConditionType.Else
                            || evn.Type == ConditionType.ElseIf))
                        {
                            MainEvent.SetIndex(i);
                            break;
                        }
                    }
                }
            }
            else
                MainEvent.ConditionEvents.Add(this);
        }
        else if (Type == ConditionType.Else)
        {
            ConditionBranchingAction ifevent = MainEvent.ConditionEvents.Last();

            if (ifevent.IsTrue == true)
            {
                for (int i = MainEvent.Index + 1; i < MainEvent.Events.Count; i++)
                {
                    if (MainEvent.Events[i] is ConditionBranchingAction evn)
                    {
                        if (evn.Index == Index &&
                            (evn.Type == ConditionType.EndIf))
                        {
                            MainEvent.SetIndex(i);
                            break;
                        }
                    }
                }
            }
        }
        else if (Type == ConditionType.EndIf)
        {
            MainEvent.ConditionEvents.RemoveAll(i => i.Index == Index);
        }

        yield return null;
    }

    public override string GetInfo()
    {
        string isVar = CompareVariable ? ", CompareVariable" : "";
        string isChoise = CompareChoise ? ", CompareChoise" : "";

        return $"TYPE: {Type}, INDEX: {Index}{isVar}{isChoise}";
    }
}
