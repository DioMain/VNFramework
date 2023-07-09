using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class GameMenuBase : MonoBehaviour
{
    [SerializeField]
    protected bool isActivated = false;
    public bool IsActivated => isActivated;

    [SerializeField]
    protected GameObject container;

    public event Action<bool> Activated;

    public virtual void Activate()
    {
        isActivated = true;
        container.SetActive(true);
        Activated?.Invoke(isActivated);
        GameManager.Instance.GameMenu.SetMenu(this);
    }
    public virtual void Disactivate()
    {
        isActivated = false;
        container.SetActive(false);
        Activated?.Invoke(isActivated);
    }

    public void ChangeMenu(GameMenuBase menu)
    {
        menu.Activate();

        Disactivate();
    }
    public void ChangeMenuPreview()
    {
        if (GameManager.Instance.GameMenu.Preview == null)
            return;

        GameManager.Instance.GameMenu.Preview.Activate();

        Disactivate();
    }
}