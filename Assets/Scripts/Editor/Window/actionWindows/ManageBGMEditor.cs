using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ManageBGMEditor : ActionEditorWindowBase<ManageBGMAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.LabelField("Тип операции:");
        @event.Type = (ManageBGMAction.ManageType)EditorGUILayout.Popup((int)@event.Type, Enum.GetNames(@event.Type.GetType()));

        switch (@event.Type)
        {
            case ManageBGMAction.ManageType.Set:
                EditorGUILayout.HelpBox("Установка параметров BGM", MessageType.Info);

                @event.Volume = EditorGUILayout.FloatField("Громкость:", @event.Volume);
                @event.Clip = (AudioClip)EditorGUILayout.ObjectField("Музыка:", @event.Clip, typeof(AudioClip), true);
                @event.Autoplay = EditorGUILayout.Toggle("Авто запуск?", @event.Autoplay);
                break;
            case ManageBGMAction.ManageType.Fate:
                EditorGUILayout.HelpBox("Затухание BGM", MessageType.Info);

                @event.FateTime = EditorGUILayout.FloatField("Время (сек):", @event.FateTime);
                @event.WaitFate = EditorGUILayout.Toggle("Ждать завершения?", @event.WaitFate);
                @event.IsPause = EditorGUILayout.Toggle("Ставить на паузу?", @event.IsPause);
                break;
            case ManageBGMAction.ManageType.Stop:
                EditorGUILayout.HelpBox("Останавливает BGM", MessageType.Info);
                break;
            case ManageBGMAction.ManageType.Play:
                EditorGUILayout.HelpBox("Запускает BGM", MessageType.Info);
                break;
            case ManageBGMAction.ManageType.Pause:
                EditorGUILayout.HelpBox("Приостанавливает BGM", MessageType.Info);
                break;
            default:
                EditorGUILayout.HelpBox("UNKNOWN", MessageType.Error);
                break;
        }
    }
}
