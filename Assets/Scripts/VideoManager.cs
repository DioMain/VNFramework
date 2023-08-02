using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer player;

    [SerializeField]
    private SpriteRenderer videoRenderer;

    private bool isPlay = false;
    public bool IsPlay => isPlay;

    private void Start()
    {
        Stop();
    }

    private void FixedUpdate()
    {
        if (player.length - player.time <= 0.1d)
            Stop();
    }

    public void Play(VideoClip clip)
    {
        player.Stop();

        player.clip = clip;

        player.Play();

        videoRenderer.enabled = true;

        isPlay = true;
    }

    public void Stop()
    {
        player.Stop();

        isPlay = false;

        videoRenderer.enabled = false;
    }

    public void HideRenderer() => videoRenderer.enabled = false;
}
