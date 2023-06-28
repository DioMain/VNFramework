using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackGroundEditor : ActionEditorWindowBase<BackGroundAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox("Измениет задний фон.", MessageType.Info);

        @event.Image = EditorGUILayout.ObjectField("Новый фон:", @event.Image, typeof(Sprite), true) as Sprite;

        @event.WithFade = EditorGUILayout.Toggle("С затуханием?", @event.WithFade);

        if (@event.WithFade)
            @event.FadeTime = EditorGUILayout.FloatField("Время затухания(сек):", @event.FadeTime);
    }
}
