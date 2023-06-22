using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GlobalEventObject))]
public class GlobalEventEditor : Editor
{
    private GlobalEventObject globalEvent;

    private void OnEnable()
    {
        globalEvent = (GlobalEventObject)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Открыть редактор"))
        {
            GameEventEditor.Initialize(globalEvent.Event);
        }
    }
}
