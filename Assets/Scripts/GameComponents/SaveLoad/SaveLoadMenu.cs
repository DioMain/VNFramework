using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SaveLoadMenu : GameMenuBase
{
    public static SaveLoadMenu instance;

    [SerializeField]
    private bool mode = false;
    /// <summary>
    /// Обозначает режим данной меню (false - load, true - save)
    /// </summary>
    public bool Mode => mode;

    [SerializeField]
    private TextMeshProUGUI Tittle;

    public UnityEvent SlotUpdate;

    private void Awake()
    {
        instance = this;

        Disactivate();
    }

    public void Init()
    {
        if (mode)
        {
            Tittle.text = "Сохранение";
        }
        else
        {
            Tittle.text = "Загрузка";
        }

        SlotUpdate.Invoke();
    }

    public void ActivateInGame(bool mode)
    {
        if (mode) 
            MapManager.Instance.SetPause(true);

        Activate(mode);
    }

    public void BackInGame()
    {
        if (mode)
        {
            MapManager.Instance.SetPause(false);

            Disactivate();
        }
        else
        {
            ChangeMenuPreview();
        }
    }

    public override void Activate()
    {
        base.Activate();

        mode = false;

        Init();
    }
    public void Activate(bool mode)
    {
        base.Activate();

        this.mode = mode;

        Init();
    }

    public override void Disactivate()
    {
        base.Disactivate();

        container.SetActive(false);
    }
}
