using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaySEEditor : ActionEditorWindowBase<PlaySEAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox("�������������� �������� ������.", MessageType.Info);

        @event.Volume = EditorGUILayout.FloatField("���������:", @event.Volume);
        @event.Clip = (AudioClip)EditorGUILayout.ObjectField("�����:", @event.Clip, typeof(AudioClip), true);
    }
}
