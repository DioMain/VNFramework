using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySEAction : ActionBase
{
    public AudioClip Clip;

    public float Volume;

    public PlaySEAction() : base("PlaySE")
    {
        Clip = null;
        Volume = 1.0f;
    }

    public override IEnumerator EventCorotine()
    {
        GameManager.Instance.Audio.PlaySound(Clip, Volume);

        yield return null;
    }

    public override string GetInfo()
    {
        return $"ÀÓÄÈÎ: {Clip.name}, ÃĞÎÌÊÎÑÒÜ: {Volume}";
    }

    public override string GetHeader()
    {
        return "Çàïóñê SE";
    }
}
