using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundImageManager : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Image bufferImage;

    [SerializeField]
    private Sprite Black;

    [SerializeField]
    private bool isFading;
    public bool IsFading => isFading;

    private void Start()
    {
        image.enabled = true;
        image.sprite = Black;
        bufferImage.enabled = false;
    }

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetImageWithFade(Sprite sprite, float time)
    {
        StartCoroutine(ImageFade(sprite, time));
    }

    private IEnumerator ImageFade(Sprite sprite, float time)
    {
        isFading = true;

        Color color = Color.white;

        bufferImage.sprite = image.sprite;
        bufferImage.enabled = true;

        bufferImage.color = color;

        image.sprite = sprite;

        float step = 1 / time;

        while (color.a > 0)
        {
            yield return new WaitForFixedUpdate();

            color.a -= step * Time.fixedDeltaTime;

            bufferImage.color = color;
        }

        bufferImage.enabled = false;

        isFading = false;
    }
}
