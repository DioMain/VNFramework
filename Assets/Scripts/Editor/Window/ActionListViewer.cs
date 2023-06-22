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

        if (GUILayout.Button("���������")) 
            EMassageEditor.Initialize(GetWindow<EMassageEditor>(true, "Massage", true));

        if (GUILayout.Button("�����"))
            ChoiseEditor.Initialize(GetWindow<ChoiseEditor>(true, "Choise", true));

        if (GUILayout.Button("���������� ��������"))
            ConditionBranchingEditor.Initialize(GetWindow<ConditionBranchingEditor>(true, "ConditionBranching", false));

        if (GUILayout.Button("���������� ���"))
            ManageBGMEditor.Initialize(GetWindow<ManageBGMEditor>(true, "BGM Manage", true));

        if (GUILayout.Button("���������� ����������"))
            ManageVarEditor.Initialize(GetWindow<ManageVarEditor>(true, "Variable manager", true));

        if (GUILayout.Button("��������� ����������"))
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
