using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterManagmentEditor : ActionEditorWindowBase<CharacterManagmentAction>
{
    public override void OnDraw()
    {
        @event.operation = (CharacterManagmentAction.Operation)EditorGUILayout.EnumPopup("��������", @event.operation);
        @event.key = EditorGUILayout.TextField("����", @event.key);

        switch (@event.operation)
        {
            case CharacterManagmentAction.Operation.Add:
                @event.sprite = (Sprite)EditorGUILayout.ObjectField("�����������", @event.sprite, typeof(Sprite), true);
                @event.position = EditorGUILayout.Vector2Field("�������", @event.position);
                @event.size = EditorGUILayout.Vector2Field("������", @event.size);

                @event.autoShow = EditorGUILayout.Toggle("���� ���������?", @event.autoShow);
                    
                break;
            case CharacterManagmentAction.Operation.Remove:
                @event.deleteInstance = EditorGUILayout.Toggle("������ ��������?", @event.deleteInstance);
                break;
            case CharacterManagmentAction.Operation.SetState:
                @event.selectionState = (CharacterObject.SelectionState)EditorGUILayout.EnumPopup("���������", @event.selectionState);
                break;
        }
    }
}
