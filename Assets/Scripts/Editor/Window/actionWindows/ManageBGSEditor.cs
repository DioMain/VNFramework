using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ManageBGSEditor : ActionEditorWindowBase<ManagerBGSAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.LabelField("Тип операции:");
        @event.Operation = (ManagerBGSAction.OperationType)EditorGUILayout.Popup((int)@event.Operation, Enum.GetNames(@event.Operation.GetType()));

        switch (@event.Operation)
        {
            case ManagerBGSAction.OperationType.Set:
                EditorGUILayout.HelpBox("Установка параметров BGS", MessageType.Info);

                @event.Volume = EditorGUILayout.FloatField("Громкость:", @event.Volume);
                @event.Clip = (AudioClip)EditorGUILayout.ObjectField("Музыка:", @event.Clip, typeof(AudioClip), true);
                @event.AutoPlay = EditorGUILayout.Toggle("Авто запуск?", @event.AutoPlay);
                break;
            case ManagerBGSAction.OperationType.Stop:
                EditorGUILayout.HelpBox("Останавливает BGS", MessageType.Info);
                break;
            case ManagerBGSAction.OperationType.Play:
                EditorGUILayout.HelpBox("Запускает BGS", MessageType.Info);
                break;
            default:
                EditorGUILayout.HelpBox("UNKNOWN", MessageType.Error);
                break;
        }
    }
}
