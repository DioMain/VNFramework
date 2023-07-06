using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterManagmentEditor : ActionEditorWindowBase<CharacterManagmentAction>
{
    public override void OnDraw()
    {
        @event.operation = (CharacterManagmentAction.Operation)EditorGUILayout.EnumPopup("Операция", @event.operation);
        @event.key = EditorGUILayout.TextField("Ключ", @event.key);

        switch (@event.operation)
        {
            case CharacterManagmentAction.Operation.Add:
                @event.sprite = (Sprite)EditorGUILayout.ObjectField("Изображение", @event.sprite, typeof(Sprite), true);
                @event.position = EditorGUILayout.Vector2Field("Позиция", @event.position);
                @event.size = EditorGUILayout.Vector2Field("Размер", @event.size);

                @event.autoShow = EditorGUILayout.Toggle("Авто появление?", @event.autoShow);
                    
                break;
            case CharacterManagmentAction.Operation.Remove:
                @event.deleteInstance = EditorGUILayout.Toggle("Резкое удаление?", @event.deleteInstance);
                break;
            case CharacterManagmentAction.Operation.SetState:
                @event.selectionState = (CharacterObject.SelectionState)EditorGUILayout.EnumPopup("Состояние", @event.selectionState);
                break;
        }
    }
}
