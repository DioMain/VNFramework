using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChoiseEditor : ActionEditorWindowBase<ChoiseAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox("���� ���� ������, �� ��� �� ����� �������������� � ������", MessageType.Info);

        for (int i = 0; i < @event.Choises.Length; i++)
        {
            @event.Choises[i] = EditorGUILayout.TextField($"����� {i}", @event.Choises[i]);
        }
    }
}
