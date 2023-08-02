using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

public class ManageVideoEditor : ActionEditorWindowBase<ManageVideoAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.LabelField("��� ��������");

        @event.action = (ManageVideoAction.ActionType)EditorGUILayout.EnumPopup(@event.action);

        if (@event.action == ManageVideoAction.ActionType.Play)
        {
            @event.wait = EditorGUILayout.Toggle("�����?", @event.wait);
            @event.clip = (VideoClip)EditorGUILayout.ObjectField("�����", @event.clip, typeof(VideoClip), true);
        }
    }
}
