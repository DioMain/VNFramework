using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ManageVarEditor : ActionEditorWindowBase<ManageVarsAction>
{
    public override void OnDraw()
    {
        GUIStyle text = new GUIStyle(EditorStyles.boldLabel)
        {
            alignment = TextAnchor.MiddleCenter,
            fixedWidth = 50f,
            stretchWidth = false
        };

        EditorGUILayout.HelpBox("Переменная будет созданна если ключ будет не обнаружен", MessageType.Info);

        EditorGUILayout.LabelField("Тип переменной:");
        @event.VType = (ManageVarsAction.VarType)EditorGUILayout.Popup((int)@event.VType, Enum.GetNames(@event.VType.GetType()));

        EditorGUILayout.BeginHorizontal();

        @event.Key = EditorGUILayout.TextField(@event.Key);

        switch (@event.VType)
        {
            case ManageVarsAction.VarType.Bool:
                EditorGUILayout.LabelField("Set", text, GUILayout.Width(50));
                @event.BoolValue = EditorGUILayout.Toggle(@event.BoolValue);
                break;
            case ManageVarsAction.VarType.Int:
                @event.OType = (ManageVarsAction.OperationType)EditorGUILayout.Popup((int)@event.OType, Enum.GetNames(@event.OType.GetType()), GUILayout.Width(75));
                @event.IntValue = EditorGUILayout.IntField(@event.IntValue);
                break;
            case ManageVarsAction.VarType.Float:
                @event.OType = (ManageVarsAction.OperationType)EditorGUILayout.Popup((int)@event.OType, Enum.GetNames(@event.OType.GetType()), GUILayout.Width(75));
                @event.FloatValue = EditorGUILayout.FloatField(@event.FloatValue);
                break;
            case ManageVarsAction.VarType.String:
                EditorGUILayout.LabelField("Set", text, GUILayout.Width(50));
                @event.StringValue = EditorGUILayout.TextField(@event.StringValue);
                break;
        }

        EditorGUILayout.EndHorizontal();
    }
}
