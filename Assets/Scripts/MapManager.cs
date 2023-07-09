using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    [Header("Общие ссылки:")]
    public DialogManager Dialog;
    public GlobalEventObject Event;
    public BackgroundImageManager BackgroundImage;
    public CharactersManager Characters;
    public HistoryManager History;

    [Header("События паузы:")]
    [Space]
    public UnityEvent OnPauseTrue;
    [Space]
    public UnityEvent OnPauseFalse;

    [SerializeField]
    private bool pause = false;
    public bool Pause => pause;

    public bool IngnoreLoading;

    public void Start()
    {
        Instance = this;

        Dialog.Init();
        Event.Init();

        SetPause(false);
    }

    public void SetPause(bool active)
    {
        pause = active;

        if (pause)
            OnPauseTrue.Invoke();
        else
            OnPauseFalse.Invoke();
    }
}