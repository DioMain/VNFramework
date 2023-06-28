using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ManageBGSEditor : ActionEditorWindowBase<ManagerBGSAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.LabelField("��� ��������:");
        @event.Operation = (ManagerBGSAction.OperationType)EditorGUILayout.Popup((int)@event.Operation, Enum.GetNames(@event.Operation.GetType()));

        switch (@event.Operation)
        {
            case ManagerBGSAction.OperationType.Set:
                EditorGUILayout.HelpBox("��������� ���������� BGS", MessageType.Info);

                @event.Volume = EditorGUILayout.FloatField("���������:", @event.Volume);
                @event.Clip = (AudioClip)EditorGUILayout.ObjectField("������:", @event.Clip, typeof(AudioClip), true);
                @event.AutoPlay = EditorGUILayout.Toggle("���� ������?", @event.AutoPlay);
                break;
            case ManagerBGSAction.OperationType.Stop:
                EditorGUILayout.HelpBox("������������� BGS", MessageType.Info);
                break;
            case ManagerBGSAction.OperationType.Play:
                EditorGUILayout.HelpBox("��������� BGS", MessageType.Info);
                break;
            default:
                EditorGUILayout.HelpBox("UNKNOWN", MessageType.Error);
                break;
        }
    }
}
