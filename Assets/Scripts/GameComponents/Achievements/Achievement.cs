using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "ARMEN/Achievement")]
public class Achievement : ScriptableObject
{
    [InspectorName("Тег")]
    public string Tag;

    [InspectorName("Заголовок")]
    public string Title;
    [InspectorName("Описание"), TextArea]
    public string Description;
    [InspectorName("Условие получения")]
    public string Condition;

    public bool Hiden;

    [InspectorName("Иконка")]
    public Sprite Icon;
}
