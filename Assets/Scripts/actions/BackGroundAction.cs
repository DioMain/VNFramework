using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundAction : ActionBase
{
    public bool WithFade;

    public float FadeTime;

    public Sprite Image;

    public BackGroundAction() : base("BackGround")
    {
        WithFade = false;
        FadeTime = 1;
        Image = null;
    }

    public override IEnumerator EventCorotine()
    {
        if (WithFade)
        {
            MapManager.Instance.BackgroundImage.SetImageWithFade(Image, FadeTime);

            yield return new WaitWhile(() => MapManager.Instance.BackgroundImage.IsFading);
        }
        else
        {
            MapManager.Instance.BackgroundImage.SetImage(Image);

            yield return null;
        }
    }

    public override string GetInfo()
    {
        string fade = WithFade ? $", С затуханием {FadeTime} сек." : "";

        return $"Фото: {Image.name}{fade}";
    }

    public override string GetHeader()
    {
        return "Фон";
    }
}
