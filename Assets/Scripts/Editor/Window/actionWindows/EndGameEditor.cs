using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EndGameEditor : ActionEditorWindowBase<EndAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox("Завершает игру", MessageType.Info);
    }
}
