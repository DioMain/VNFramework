using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBGMAction : ActionBase
{
    public enum ManageType
    {
        Set, Play, Stop, Pause, Fate
    }

    public ManageType Type;

    #region FOR SET

    public float Volume;
    public AudioClip Clip;
    public bool Autoplay;

    #endregion

    #region FOR FATE

    public float FateTime;
    public bool IsPause;
    public bool WaitFate;

    #endregion

    public ManageBGMAction() : base("ManageBGM")
    {
        Type = ManageType.Set;

        Volume = 1;
        Clip = null;
        Autoplay = true;

        FateTime = 1;
        IsPause = false;
        WaitFate = true;
    }

    public override IEnumerator EventCorotine()
    {
        switch (Type)
        {
            case ManageType.Set:
                GameManager.Instance.Audio.BGMVolume = Volume;
                GameManager.Instance.Audio.SetBGM(Clip, Autoplay);
                break;
            case ManageType.Play:
                GameManager.Instance.Audio.PlayBGM();
                break;
            case ManageType.Stop:
                GameManager.Instance.Audio.StopBGM();
                break;
            case ManageType.Pause:
                GameManager.Instance.Audio.PauseBGM();
                break;
            case ManageType.Fate:
                GameManager.Instance.Audio.FadeBGM(FateTime, !IsPause);

                if (WaitFate)
                    yield return new WaitWhile(() => GameManager.Instance.Audio.IsFadingBGM);
                break;
        }

        yield return null;
    }

    public override string GetInfo()
    {
        return Type switch
        {
            ManageType.Set => $"SET: {{VOL: {Volume}, CLIP: {Clip.name}, AUTOPLAY: {Autoplay}}}",
            ManageType.Play => $"PLAY",
            ManageType.Stop => $"STOP",
            ManageType.Pause => $"PAUSE",
            ManageType.Fate => $"FATE: {{FATETIME: {FateTime}s, ISPAUSE: {IsPause}, WAIT: {WaitFate}}}",
            _ => "NULL!",
        };
    }
}
