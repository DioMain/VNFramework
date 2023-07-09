using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterObject : MonoBehaviour
{
    public enum SelectionState
    {
        Show, Hide, Select, Unselect
    }

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Image _image;

    [SerializeField]
    private SelectionState state = SelectionState.Hide;
    public SelectionState State => state;

    public Sprite OwnSprite => _image.sprite;
    public Vector2 Position => transform.position;
    public Vector2 Size => transform.localScale;

    public void Show()
    {
        _animator.SetTrigger("Show");

        state = SelectionState.Show;
    }
    public void Hide()
    {
        _animator.SetTrigger("Hide");

        state = SelectionState.Hide;
    }
    public void Select() 
    {
        _animator.SetTrigger("Select");

        state = SelectionState.Select;
    }
    public void Unselect()
    {
        _animator.SetTrigger("Unselect");

        state = SelectionState.Unselect;
    }

    public void SetByState(SelectionState state)
    {
        switch (state)
        {
            case SelectionState.Hide:
                Hide();
                break;
            case SelectionState.Show:
                Show();
                break;
            case SelectionState.Select:
                Select();
                break;
            case SelectionState.Unselect: 
                Unselect();
                break;
        }
    }

    public void SetImage(Sprite image) => _image.sprite = image;
}
