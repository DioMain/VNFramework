using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StopEvntEditor : ActionEditorWindowBase<StopAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox("Завершает выполнение события", MessageType.Info);
    }
}
