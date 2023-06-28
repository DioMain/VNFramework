using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackGroundEditor : ActionEditorWindowBase<BackGroundAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox("�������� ������ ���.", MessageType.Info);

        @event.Image = EditorGUILayout.ObjectField("����� ���:", @event.Image, typeof(Sprite), true) as Sprite;

        @event.WithFade = EditorGUILayout.Toggle("� ����������?", @event.WithFade);

        if (@event.WithFade)
            @event.FadeTime = EditorGUILayout.FloatField("����� ���������(���):", @event.FadeTime);
    }
}
