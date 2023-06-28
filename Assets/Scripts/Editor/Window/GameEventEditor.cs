using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameEventEditor : EditorWindow, IDisposable
{
    public static GameEventEditor Instance;

    private GameEvent eventPipeLine;
    public GameEvent EventPipeLine => eventPipeLine;

    private int currentId = -1;
    public int CurrentId => currentId;

    private Vector2 scroll;

    public event Action OnClosed;

    public static void Initialize(GameEvent eventPipeLine)
    {
        if (Instance != null)
            Instance.Dispose();

        Instance = GetWindow<GameEventEditor>(true, "Event viewer", true);

        Instance.currentId = -1;
        Instance.eventPipeLine = eventPipeLine;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        scroll = EditorGUILayout.BeginScrollView(scroll);

        GUIStyle header = new GUIStyle(EditorStyles.boldLabel)
        {
            alignment = TextAnchor.MiddleLeft,
            fontSize = 16,
            stretchWidth = false,
        };
        GUIStyle body = new GUIStyle(EditorStyles.boldLabel)
        {
            alignment = TextAnchor.MiddleLeft,
            stretchWidth = true
        };

        for (int i = 0; i < eventPipeLine.Actions.Count; i++)
        {
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            GUILayout.Label($"{eventPipeLine.Actions[i].GetHeader()}:", header);
            GUILayout.Label($"{{{eventPipeLine.Actions[i].GetInfo()}}}", body);

            EditorGUILayout.BeginHorizontal(GUILayout.Width(225));

            if (GUILayout.Button("Изменить", GUILayout.Width(75)))
            {
                currentId = i;
                OpenNeedWindow(eventPipeLine.Actions[i]);
            }

            if (GUILayout.Button("Удалить", GUILayout.Width(75)))
                DeleteEvent(i);

            if (GUILayout.Button("Добавить", GUILayout.Width(75)))
            {
                currentId = i;
                ActionListViewer.Initialize();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Добавить событие"))
        {
            currentId = -1;
            ActionListViewer.Initialize();
        }


        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private void DeleteEvent(int index)
    {
        eventPipeLine.Actions.RemoveAt(index);

        if (eventPipeLine.DispathObject != null)
            EditorUtility.SetDirty(eventPipeLine.DispathObject);
        else
            EditorUtility.SetDirty(eventPipeLine.DispathScriptable);
    }

    private void OpenNeedWindow(ActionBase eventBase)
    {
        switch (eventBase)
        {
            case MessageAction:
                EMassageEditor.Initialize(GetWindow<EMassageEditor>(true, "Massage", true), eventBase as MessageAction);
                break;
            case ManageVarsAction:
                ManageVarEditor.Initialize(GetWindow<ManageVarEditor>(true, "Variable manager", true), eventBase as ManageVarsAction);
                break;
            case ManageBGMAction:
                ManageBGMEditor.Initialize(GetWindow<ManageBGMEditor>(true, "BGM Manager", true), eventBase as ManageBGMAction);
                break;
            case ChoiseAction:
                ChoiseEditor.Initialize(GetWindow<ChoiseEditor>(true, "Choice", true), eventBase as ChoiseAction);
                break;
            case ConditionBranchingAction:
                ConditionBranchingEditor.Initialize(GetWindow<ConditionBranchingEditor>(true, "ConditionBranching", true), eventBase as ConditionBranchingAction);
                break;
            case StopAction:
                StopEvntEditor.Initialize(GetWindow<StopEvntEditor>(true, "Stop", true), eventBase as StopAction);
                break;
            case BackGroundAction:
                BackGroundEditor.Initialize(GetWindow<BackGroundEditor>(true, "BackGround", true), eventBase as BackGroundAction);
                break;
            case WaitAction:
                WaitEditor.Initialize(GetWindow<WaitEditor>(true, "Wait", true), eventBase as WaitAction);
                break;
            case PlaySEAction:
                PlaySEEditor.Initialize(GetWindow<PlaySEEditor>(true, "Play SE", true), eventBase as PlaySEAction);
                break;
            case ManagerBGSAction:
                ManageBGSEditor.Initialize(GetWindow<ManageBGSEditor>(true, "Manage BGS", true), eventBase as ManagerBGSAction);
                break;
            default:
                Debug.LogWarning("Unknown event!");
                break;
        }
    }

    private void OnDisable() => Dispose();

    public void Dispose()
    {
        OnClosed?.Invoke();
        Instance = null;
    }
}
