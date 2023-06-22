using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class EMassageEditor : ActionEditorWindowBase<MessageAction>
{
    public override void OnDraw()
    {
        EditorGUILayout.HelpBox(
            "<color=[���� � hex]>[�����]</color> - ��� �����\n" +
            "/p - ������� �������\n" +
            "/. /| /d /D - ����� (0.1s, 0.25s, 0.5s, 1s)\n" +
            "/v(b,i,f,s)[����] - ��� ������ ���������� (bool, int, float, string)", 
            MessageType.Info);

        GUILayout.Label("�����");
        @event.Text = EditorGUILayout.TextArea(@event.Text, GUILayout.Height(65));

        @event.CharName = EditorGUILayout.TextField("��� ���������:", @event.CharName);

        @event.Wait = EditorGUILayout.Toggle("�����?", @event.Wait);
        @event.Clear = EditorGUILayout.Toggle("��������?", @event.Clear);
        @event.CloseAfter = EditorGUILayout.Toggle("������� ����?", @event.CloseAfter);

        @event.Speed = EditorGUILayout.FloatField("��������", @event.Speed);
    }
}
