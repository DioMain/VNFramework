using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class ActionEditorWindowBase<T> : EditorWindow, IDisposable
    where T : ActionBase, new()
{
    public static ActionEditorWindowBase<T> Instance;

    protected T @event;

    protected bool isEdit;

    private Vector2 scrollPosition;

    public static void Initialize(ActionEditorWindowBase<T> window, T @event = null)
    {
        if (Instance != null)
            Instance.Close();

        Instance = window;

        window.isEdit = @event != null;
        window.Setup(@event);
    }

    private void OnGUI()
    {
        GUIStyle header = new GUIStyle(EditorStyles.boldLabel)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 24,
        };

        EditorGUILayout.BeginVertical();
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        GUILayout.Label(titleContent.text, header);

        EditorGUILayout.BeginVertical(GUI.skin.box);
        OnDraw();
        EditorGUILayout.EndVertical();

        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(rootVisualElement.worldBound.size.x / 2 - 200 / 2);
        if (GUILayout.Button("Accept", GUILayout.Width(200)))
        {
            OnAccept();

            if (GameEventEditor.Instance.EventPipeLine.DispathObject != null) 
                EditorUtility.SetDirty(GameEventEditor.Instance.EventPipeLine.DispathObject);
            else
                EditorUtility.SetDirty(GameEventEditor.Instance.EventPipeLine.DispathScriptable);

            Close();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    public void Setup(T @event)
    {
        this.@event = isEdit ? @event : new T();

        GameEventEditor.Instance.OnClosed += Close;
    }

    public abstract void OnDraw();

    private void OnDisable() => Dispose();

    public virtual void OnAccept()
    {
        if (isEdit)
        {
            GameEventEditor.Instance.EventPipeLine.Events[GameEventEditor.Instance.CurrentId] = @event;
        }
        else
        {
            if (GameEventEditor.Instance.CurrentId > -1)
                GameEventEditor.Instance.EventPipeLine.Events.Insert(GameEventEditor.Instance.CurrentId, @event);
            else
                GameEventEditor.Instance.EventPipeLine.Events.Add(@event);
        }
    }

    public virtual void Dispose()
    {
        Instance = null;
    }
}
