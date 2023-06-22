using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventObject : MonoBehaviour, IGameInited
{
    [SerializeField]
    private GlobalEvent @event = new GlobalEvent();
    public GlobalEvent Event => @event;

    public void Init()
    {
        Event.DispathObject = this;

        Event.Run();
    }
}
