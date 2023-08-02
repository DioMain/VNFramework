using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConditionBranchingEditor : ActionEditorWindowBase<ConditionBranchingAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox(
            "��� �������� ������� ��������� ������� ������� " +
            "If �������, � � ����� ����������� ������ ������� EndIf. " +
            "��������� �����������: (IF-ENDIF), (IF-ELSE-ENDIF), (IF-ELSEIF-ELSE-ENDIF), " +
            "(IF-ELSEIF-...-ELSEIF-ENDIF), (IF-ELSEIF-...-ELSEIF-ELSE-ENDIF)",
            MessageType.Info);

        EditorGUILayout.LabelField("��� ��������:");
        @event.Type = (ConditionBranchingAction.ConditionType)EditorGUILayout.Popup((int)@event.Type, Enum.GetNames(@event.Type.GetType()));
        //@event.Type = ConditionBranchingEvent.ConditionType.If;

        switch (@event.Type)
        {
            case ConditionBranchingAction.ConditionType.If:
                EditorGUILayout.HelpBox("������������ �������", MessageType.Info);
                break;
            case ConditionBranchingAction.ConditionType.ElseIf:
                EditorGUILayout.HelpBox("����� � ��������, ����� ������������� ����� If", MessageType.Info);
                break;
            case ConditionBranchingAction.ConditionType.Else:
                EditorGUILayout.HelpBox("�����, ����� ������������� ����� If ��� ElseIf", MessageType.Info);
                break;
            case ConditionBranchingAction.ConditionType.EndIf:
                EditorGUILayout.HelpBox("����������� ���� �������", MessageType.Info);
                break;
        }

        if (@event.Type == ConditionBranchingAction.ConditionType.If || 
            @event.Type == ConditionBranchingAction.ConditionType.ElseIf)
        {
            @event.CompareVariable = EditorGUILayout.Toggle("�� ����������?", @event.CompareVariable, GUILayout.ExpandWidth(true));

            if (@event.CompareVariable)
            {
                EditorGUILayout.LabelField("��� ����������:");
                @event.VariableType = EditorGUILayout.Popup(@event.VariableType, new string[] {"String", "Int", "Float", "Bool"});

                GUIStyle text = new GUIStyle(EditorStyles.boldLabel)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fixedWidth = 50f,
                    stretchWidth = false
                };

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("��� ����������", new GUIStyle(EditorStyles.label)
                {
                    alignment = TextAnchor.MiddleLeft,
                    stretchWidth=true,
                }, GUILayout.MinWidth(0), GUILayout.MaxWidth(500));

                EditorGUILayout.LabelField("��������", new GUIStyle(EditorStyles.label)
                {
                    alignment = TextAnchor.MiddleCenter,
                    stretchWidth=true,
                }, GUILayout.MinWidth(0), GUILayout.MaxWidth(500));

                EditorGUILayout.LabelField("��������", new GUIStyle(EditorStyles.label)
                {
                    alignment = TextAnchor.MiddleRight,
                    stretchWidth=true,
                }, GUILayout.MinWidth(0), GUILayout.MaxWidth(500));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();

                @event.VariableKey = EditorGUILayout.TextField(@event.VariableKey);

                if (@event.VariableType == 1 || @event.VariableType == 2)
                {
                    @event.Operator = (ConditionBranchingAction.ConditionOperator)EditorGUILayout.Popup((int)@event.Operator, new string[] { "==", ">", "<", ">=", "<=" }, GUILayout.Width(75));
                }
                else
                {
                    EditorGUILayout.LabelField("==", text, GUILayout.Width(50));

                    @event.Operator = ConditionBranchingAction.ConditionOperator.Equals;
                }
                    


                switch (@event.VariableType)
                {
                    case 0:
                        @event.VariableStringValue = EditorGUILayout.TextField(@event.VariableStringValue);
                        break;
                    case 1:
                        @event.VariableIntValue = EditorGUILayout.IntField(@event.VariableIntValue);
                        break;
                    case 2:
                        @event.VariableFloatValue = EditorGUILayout.FloatField(@event.VariableFloatValue);
                        break;
                    case 3:
                        @event.VariableBoolValue = EditorGUILayout.Toggle(@event.VariableBoolValue);
                        break;
                }

                EditorGUILayout.EndHorizontal();
            }

            @event.CompareChoise = EditorGUILayout.Toggle("�� ������?", @event.CompareChoise);

            if (@event.CompareChoise)
            {
                @event.ChoiseExpected = EditorGUILayout.IntField("��������� �����:", @event.ChoiseExpected);
            }
        }
    }
}
