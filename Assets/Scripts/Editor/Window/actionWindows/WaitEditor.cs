using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaitEditor : ActionEditorWindowBase<WaitAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox("Приастонавливает выполнение на определённый промежуток времени.", MessageType.Info);

        @event.Time = EditorGUILayout.FloatField("Время (сек):", @event.Time);
    }
}
