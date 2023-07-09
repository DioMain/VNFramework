using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public GameMenuBase Current;
    public GameMenuBase Preview;

    public void SetMenu(GameMenuBase menu)
    {
        Preview = Current;

        Current = menu;
    }
}
