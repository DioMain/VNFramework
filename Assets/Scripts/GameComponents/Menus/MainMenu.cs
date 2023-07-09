using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : GameMenuBase
{
    private void Start()
    {
        Activate();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
