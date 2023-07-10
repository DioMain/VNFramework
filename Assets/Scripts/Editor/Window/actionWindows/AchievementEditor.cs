using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AchievementEditor : ActionEditorWindowBase<AchievementAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox("����������� ����������", MessageType.Info);

        @event.AchievementTag = EditorGUILayout.TextField("���", @event.AchievementTag);
    }
}
