using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGEffect : MonoBehaviour
{
    [SerializeField]
    private RectTransform m_RectTransform;

    private Vector2 prevMousePos = Vector2.zero;
    private Vector2 nowMousePos = Vector2.zero;

    public float Resistance;

    private void Start()
    {
        nowMousePos = Input.mousePosition;
    }

    private void FixedUpdate()
    {
        Vector2 dif = nowMousePos - prevMousePos;

        m_RectTransform.localPosition = (Vector2)m_RectTransform.localPosition - (dif / Resistance);

        prevMousePos = nowMousePos;
        nowMousePos = Input.mousePosition;
    }
}
