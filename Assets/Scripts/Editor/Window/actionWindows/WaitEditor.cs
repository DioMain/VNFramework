using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaitEditor : ActionEditorWindowBase<WaitAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox("���������������� ���������� �� ����������� ���������� �������.", MessageType.Info);

        @event.Time = EditorGUILayout.FloatField("����� (���):", @event.Time);
    }
}
