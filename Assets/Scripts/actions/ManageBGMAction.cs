using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBGMAction : ActionBase
{
    public enum ManageType
    {
        Set, Play, Stop, Pause, Fade
    }

    public ManageType Type;

    #region FOR SET

    public float Volume;
    public AudioClip Clip;
    public bool Autoplay;

    #endregion

    #region FOR FADE

    public float FadeTime;
    public bool IsPause;
    public bool WaitFade;
    public bool FadeDirection;

    #endregion

    public ManageBGMAction() : base("ManageBGM")
    {
        Type = ManageType.Set;

        Volume = 1;
        Clip = null;
        Autoplay = true;

        FadeTime = 1;
        IsPause = false;
        WaitFade = true;

        FadeDirection = false;
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
            case ManageType.Fade:
                GameManager.Instance.Audio.FadeBGM(FadeTime, !IsPause, FadeDirection);

                if (WaitFade)
                    yield return new WaitWhile(() => GameManager.Instance.Audio.IsFadingBGM);
                break;
        }

        yield return null;
    }

    public override string GetInfo()
    {
        return Type switch
        {
            ManageType.Set => $"ÓÑÒÀÍÎÂÈÒÜ: {{ÃĞÎÌÊÎÑÒÜ: {Volume}, ÀÓÄÈÎ: {Clip.name}, ÀÂÒÎÇÀÏÓÑÊ: {Autoplay}}}",
            ManageType.Play => $"ÍÀ×ÀÒÜ",
            ManageType.Stop => $"ÎÑÒÀÍÎÂÈÒÜ",
            ManageType.Pause => $"ÏÀÓÇÀ",
            ManageType.Fade => $"ÇÀÒÓÕÀÍÈÅ: {{ÂĞÅÌß ÇÀÒÓÕÀÍÈß: {FadeTime}s, ÏÀÓÇÀ?: {IsPause}, ÆÄÀÒÜ?: {WaitFade}}}",
            _ => "NULL!",
        };
    }

    public override string GetHeader()
    {
        return "Óïğàâëåíèå BGM";
    }
}
