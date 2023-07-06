﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public DialogManager Dialog;
    public GlobalEventObject Event;
    public BackgroundImageManager BackgroundImage;
    public CharactersManager Characters;

    public bool Pause = false;

    public void Start()
    {
        Instance = this;

        Dialog.Init();
        Event.Init();
    }
}