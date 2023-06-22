using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiseObject : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private TextMeshProUGUI textMesh;
    public TextMeshProUGUI TextMesh => textMesh;

    [SerializeField]
    private bool isChoised = false;
    public bool IsChoised
    {
        get => isChoised;
        set
        {
            isChoised = value;
            m_Animator.SetBool("Choised", value);
        }
    }
}
