using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChoiseEditor : ActionEditorWindowBase<ChoiseAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox("Если поле пустое, то оно не будет использоваться в выборе", MessageType.Info);

        for (int i = 0; i < @event.Choises.Length; i++)
        {
            @event.Choises[i] = EditorGUILayout.TextField($"Выбор {i}", @event.Choises[i]);
        }
    }
}
