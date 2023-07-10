using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleMenu : GameMenuBase
{
    private void Start()
    {
        Disactivate();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (IsActivated)
            {
                DisactivateInGame();
            }
            else
            {
                if (MapManager.Instance.Pause)
                    GameManager.Instance.GameMenu.Current.ChangeMenu(this);
                else
                    ActivateInGame();
            }
        }
    }

    public void DisactivateInGame()
    {
        base.Disactivate();

        MapManager.Instance.SetPause(false);
    }
    public void ActivateInGame()
    {
        base.Activate();

        MapManager.Instance.SetPause(true);
    }
}
