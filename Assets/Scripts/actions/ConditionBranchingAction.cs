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

    //public int Index;

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

    private void FindIndex(bool onlyEndIf = false)
    {
        Stack<ConditionBranchingAction> ifStack = new Stack<ConditionBranchingAction>();

        bool isFinded = false;
        for (int i = MainEvent.Index + 1; i < MainEvent.Actions.Count; i++)
        {
            if (MainEvent.Actions[i] is ConditionBranchingAction evn)
            {
                if (evn.Type == ConditionType.If)
                    ifStack.Push(evn);
                else
                {
                    if (ifStack.Count > 0)
                    {
                        if (evn.Type == ConditionType.EndIf)
                            ifStack.Pop();
                    }
                    else
                    {
                        bool part0 = (evn.Type == ConditionType.EndIf || evn.Type == ConditionType.Else
                                                    || evn.Type == ConditionType.ElseIf) && !onlyEndIf;

                        bool part1 = evn.Type == ConditionType.EndIf && onlyEndIf;

                        if (part0 || part1)
                        {
                            MainEvent.SetIndex(i);
                            isFinded = true;
                            break;
                        }
                    }
                }
            }
        }

        if (!isFinded)
            Debug.LogError("Конец условия не найден!");
    }

    public override IEnumerator EventCorotine()
    {
        if (Type == ConditionType.If)
        {
            MainEvent.ConditionEvents.Add(this);

            IsTrue = Check();

            if (!IsTrue)
                FindIndex();
        }
        else if (Type == ConditionType.ElseIf)
        {
            ConditionBranchingAction ifevent = MainEvent.ConditionEvents.Last();

            IsTrue = Check() && ifevent.IsTrue == false;

            if (!IsTrue)
                FindIndex();
            else
                MainEvent.ConditionEvents.Add(this);
        }
        else if (Type == ConditionType.Else)
        {
            ConditionBranchingAction ifevent = MainEvent.ConditionEvents.Last();

            if (ifevent.IsTrue == true)
                FindIndex(true);
        }
        else if (Type == ConditionType.EndIf)
        {
            while (MainEvent.ConditionEvents.Count > 0 && MainEvent.ConditionEvents.Last().Type == ConditionType.ElseIf)
            {
                MainEvent.ConditionEvents.Remove(MainEvent.ConditionEvents.Last());
            }

            if (MainEvent.ConditionEvents.Count > 0)
                MainEvent.ConditionEvents.Remove(MainEvent.ConditionEvents.Last());
            else
                Debug.LogWarning("Не найдено условие!");
        }

        yield return null;
    }

    public override string GetInfo()
    {
        string isVar = CompareVariable ? $", По переменной" : "";
        string isChoise = CompareChoise ? $", Выбор == {ChoiseExpected}" : "";

        return $"ТИП: {Type}{isVar}{isChoise}";
    }

    public override string GetHeader()
    {
        switch (Type)
        {
            case ConditionType.If:
                return $"    Если";
            case ConditionType.ElseIf:
                return $"    Иначе если ";
            case ConditionType.Else:
                return $"    Иначе";
            case ConditionType.EndIf:
                return $"    Конец условия";
            default:
                return "IF NULL";
        }
    }
}
