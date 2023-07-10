using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AchievementEditor : ActionEditorWindowBase<AchievementAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox("Засчитывает достижение", MessageType.Info);

        @event.AchievementTag = EditorGUILayout.TextField("Тег", @event.AchievementTag);
    }
}
