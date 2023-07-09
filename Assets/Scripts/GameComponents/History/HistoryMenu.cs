using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryMenu : GameMenuBase
{
    [SerializeField]
    private HistoryManager history;

    private void Start()
    {
        Disactivate();
    }

    public override void Activate()
    {
        base.Activate();

        MapManager.Instance.SetPause(true);
    }

    public void DisactiovateInGame()
    {
        base.Disactivate();

        MapManager.Instance.SetPause(false);
    }
}
