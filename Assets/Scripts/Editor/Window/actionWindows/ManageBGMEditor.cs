using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ManageBGMEditor : ActionEditorWindowBase<ManageBGMAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.LabelField("��� ��������:");
        @event.Type = (ManageBGMAction.ManageType)EditorGUILayout.Popup((int)@event.Type, Enum.GetNames(@event.Type.GetType()));

        switch (@event.Type)
        {
            case ManageBGMAction.ManageType.Set:
                EditorGUILayout.HelpBox("��������� ���������� BGM", MessageType.Info);

                @event.Volume = EditorGUILayout.FloatField("���������:", @event.Volume);
                @event.Clip = (AudioClip)EditorGUILayout.ObjectField("������:", @event.Clip, typeof(AudioClip), true);
                @event.Autoplay = EditorGUILayout.Toggle("���� ������?", @event.Autoplay);
                break;
            case ManageBGMAction.ManageType.Fade:
                EditorGUILayout.HelpBox("��������� BGM", MessageType.Info);

                @event.FadeDirection = EditorGUILayout.Popup(@event.FadeDirection ? 1 : 0, new string[] { "�� ����������", "�� ���������" }) != 0;

                @event.FadeTime = EditorGUILayout.FloatField("����� (���):", @event.FadeTime);
                @event.WaitFade = EditorGUILayout.Toggle("����� ����������?", @event.WaitFade);

                if (!@event.FadeDirection) 
                    @event.IsPause = EditorGUILayout.Toggle("������� �� �����?", @event.IsPause);

                break;
            case ManageBGMAction.ManageType.Stop:
                EditorGUILayout.HelpBox("������������� BGM", MessageType.Info);
                break;
            case ManageBGMAction.ManageType.Play:
                EditorGUILayout.HelpBox("��������� BGM", MessageType.Info);
                break;
            case ManageBGMAction.ManageType.Pause:
                EditorGUILayout.HelpBox("���������������� BGM", MessageType.Info);
                break;
            default:
                EditorGUILayout.HelpBox("UNKNOWN", MessageType.Error);
                break;
        }
    }
}
