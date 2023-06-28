using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaySEEditor : ActionEditorWindowBase<PlaySEAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox("Воспроизводить звуковой эффект.", MessageType.Info);

        @event.Volume = EditorGUILayout.FloatField("Громкость:", @event.Volume);
        @event.Clip = (AudioClip)EditorGUILayout.ObjectField("Аудио:", @event.Clip, typeof(AudioClip), true);
    }
}
