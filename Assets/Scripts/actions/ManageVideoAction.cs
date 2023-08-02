using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ManageVideoAction : ActionBase
{
    public enum ActionType
    {
        Play, Stop
    }

    public bool wait;

    public VideoClip clip;

    public ActionType action;

    public ManageVideoAction() : base("ManageVideo")
    {
        wait = true;
        action = ActionType.Play;
        clip = null;
    }

    public override IEnumerator EventCorotine()
    {
        switch (action)
        {
            case ActionType.Play:

                MapManager.Instance.Video.Play(clip);

                if (wait)
                {
                    yield return new WaitWhile(() => MapManager.Instance.Video.IsPlay);
                }
                    

                break;
            case ActionType.Stop:
                MapManager.Instance.Video.Stop();
                break;
        }
    }

    public override string GetInfo()
    {
        string swait = wait ? ", ждать" : string.Empty;

        if (action == ActionType.Play)
            return $"Видео: {clip.name}{swait}";
        else
            return $"Остановка";
    }

    public override string GetHeader()
    {
        return "Управление видео";
    }
}
