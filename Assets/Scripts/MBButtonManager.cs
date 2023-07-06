using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MBButtonManager : MonoBehaviour
{
    public Button Menu;
    public Button Skip;
    public Button History;
    public Button Save;

    public MessageBoxWriter Writer;

    private void Awake()
    {
        Writer.OnAutoSkip += Writer_OnAutoSkip;
    }

    private void Writer_OnAutoSkip(bool obj)
    {
        Image image = Skip.GetComponent<Image>();

        if (obj)
            image.color = Color.gray;
        else
            image.color = Color.white;
    }
}
