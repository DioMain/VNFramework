using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagmentAction : ActionBase
{
    public enum Operation
    {
        Add, Remove, SetState
    }

    public Operation operation;

    public string key;

    public Sprite sprite;
    public CharacterObject.SelectionState selectionState;

    public Vector2 position;
    public Vector2 size;

    public bool autoShow;
    public bool deleteInstance;

    public CharacterManagmentAction() : base("CharacterManagment")
    {
        operation = Operation.SetState;
        key = string.Empty;
        sprite = null;
        selectionState = CharacterObject.SelectionState.Select;
        position = Vector2.zero;
        size = Vector2.one;
        autoShow = true;
        deleteInstance = false;
    }

    public override IEnumerator EventCorotine()
    {
        switch (operation)
        {
            case Operation.Add:
                MapManager.Instance.Characters.AddCharacter(key, sprite, position, size, autoShow);
                break;
            case Operation.Remove:
                MapManager.Instance.Characters.RemoveCharacter(key, deleteInstance);
                break;
            case Operation.SetState:
                MapManager.Instance.Characters.SetCharacterState(key, selectionState);
                break;
        }

        yield return null;
    }

    public override string GetInfo()
    {
        switch (operation)
        {
            case Operation.Add:
                return $"��������: ���� = {key}, ���� = {sprite.name}, ������� = {{{position}}}, ������ = {{{size}}}, ������������� = {autoShow}";
            case Operation.Remove:
                return $"�������: ���� = {key}, ������� ����� = {deleteInstance}";
            case Operation.SetState:
                return $"���������� ���������: ���� = {key}, ��������� = {selectionState}";
            default:
                return "NULL";
        }
    }

    public override string GetHeader()
    {
        return "���������� ����������";
    }
}
