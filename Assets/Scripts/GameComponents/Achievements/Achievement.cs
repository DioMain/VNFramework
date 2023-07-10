using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "ARMEN/Achievement")]
public class Achievement : ScriptableObject
{
    [InspectorName("���")]
    public string Tag;

    [InspectorName("���������")]
    public string Title;
    [InspectorName("��������"), TextArea]
    public string Description;
    [InspectorName("������� ���������")]
    public string Condition;

    public bool Hiden;

    [InspectorName("������")]
    public Sprite Icon;
}
