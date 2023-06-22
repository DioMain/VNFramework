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
            "<color=[цвет в hex]>[Текст]</color> - для цвета\n" +
            "/p - ожидать нажатия\n" +
            "/. /| /d /D - пауза (0.1s, 0.25s, 0.5s, 1s)\n" +
            "/v(b,i,f,s)[Ключ] - для вывода переменной (bool, int, float, string)", 
            MessageType.Info);

        GUILayout.Label("Текст");
        @event.Text = EditorGUILayout.TextArea(@event.Text, GUILayout.Height(65));

        @event.CharName = EditorGUILayout.TextField("Имя персонажа:", @event.CharName);

        @event.Wait = EditorGUILayout.Toggle("Ждать?", @event.Wait);
        @event.Clear = EditorGUILayout.Toggle("Очистить?", @event.Clear);
        @event.CloseAfter = EditorGUILayout.Toggle("Закрыть окно?", @event.CloseAfter);

        @event.Speed = EditorGUILayout.FloatField("Скорость", @event.Speed);
    }
}
