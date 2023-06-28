using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ActionListViewer : EditorWindow, IDisposable
{
    public static ActionListViewer Instance;

    private Vector2 scroll;

    public static void Initialize()
    {
        if (Instance != null)
            Instance.Close();

        Instance = GetWindow<ActionListViewer>(true, "Event list", true);

        GameEventEditor.Instance.OnClosed += Instance.Close;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        scroll = EditorGUILayout.BeginScrollView(scroll);

        if (GUILayout.Button("Сообщение")) 
            EMassageEditor.Initialize(GetWindow<EMassageEditor>(true, "Massage", true));

        if (GUILayout.Button("Выбор"))
            ChoiseEditor.Initialize(GetWindow<ChoiseEditor>(true, "Choise", true));

        if (GUILayout.Button("Управление условием"))
            ConditionBranchingEditor.Initialize(GetWindow<ConditionBranchingEditor>(true, "ConditionBranching", false));

        if (GUILayout.Button("Управление BGM"))
            ManageBGMEditor.Initialize(GetWindow<ManageBGMEditor>(true, "Manage BGM", true));

        if (GUILayout.Button("Управление BGS"))
            ManageBGSEditor.Initialize(GetWindow<ManageBGSEditor>(true, "Manage BGS", true));

        if (GUILayout.Button("Запуск SE"))
            PlaySEEditor.Initialize(GetWindow<PlaySEEditor>(true, "Play SE", true));

        if (GUILayout.Button("Управление переменной"))
            ManageVarEditor.Initialize(GetWindow<ManageVarEditor>(true, "Variable manager", true));

        if (GUILayout.Button("Управление задним фоном"))
            BackGroundEditor.Initialize(GetWindow<BackGroundEditor>(true, "BackGround", true));

        if (GUILayout.Button("Ждать"))
            WaitEditor.Initialize(GetWindow<WaitEditor>(true, "Wait", true));

        if (GUILayout.Button("Завершить выполнение"))
            StopEvntEditor.Initialize(GetWindow<StopEvntEditor>(true, "Stop", true));

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private void OnDisable() => Dispose();

    public void Dispose()
    {
        Instance = null;
    }
}
