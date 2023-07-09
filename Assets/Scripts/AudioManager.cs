using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private AudioSource bgsSource;
    [SerializeField]
    private AudioSource soundSource;


    [SerializeField]
    private bool isFadingBGM = false;
    public bool IsFadingBGM => isFadingBGM;

    public float BGMVolume
    {
        get => bgmSource.volume;
        set => bgmSource.volume = value;
    }
    public float BGSVolume
    {
        get => bgsSource.volume;
        set => bgsSource.volume = value;
    }

    public bool BGMPlaying => bgmSource.isPlaying;
    public bool BGSPlaying => bgsSource.isPlaying;

    public void SetBGM(AudioClip clip, bool autoplay = false)
    {
        StopBGM();

        bgmSource.clip = clip;

        if (autoplay)
            PlayBGM();
    }
    public void PlayBGM() => bgmSource.Play();
    public void StopBGM() => bgmSource.Stop();
    public void PauseBGM() => bgmSource.Pause();
    /// <summary>
    /// Создаёд затухание BGM
    /// </summary>
    /// <param name="time">в секундах</param>
    public void FadeBGM(float time, bool stop = true) => StartCoroutine(FadeBGMCoroutine(time, stop));

    public void SetBGS(AudioClip clip, bool autoplay = false)
    {
        StopBGS();

        bgsSource.clip = clip;

        if (autoplay)
            PlayBGS();
    }
    public void PlayBGS() => bgsSource.Play();
    public void StopBGS() => bgsSource.Stop();

    public void PlaySound(AudioClip clip, float volume = 1)
    {
        if (soundSource.isPlaying)
            soundSource.Stop();

        soundSource.volume = volume;
        soundSource.clip = clip;

        soundSource.Play();
    }

    public void ApplyConfig()
    {
        bgmSource.outputAudioMixerGroup.audioMixer.SetFloat("Vol", GameManager.Instance.GameConfig.BGMVolume < -40 ? -80 : GameManager.Instance.GameConfig.BGMVolume);
        bgsSource.outputAudioMixerGroup.audioMixer.SetFloat("Vol", GameManager.Instance.GameConfig.BGSVolume < -40 ? -80 : GameManager.Instance.GameConfig.BGSVolume);
        soundSource.outputAudioMixerGroup.audioMixer.SetFloat("Vol", GameManager.Instance.GameConfig.SEVolume < -40 ? -80 : GameManager.Instance.GameConfig.SEVolume);
    }

    private IEnumerator FadeBGMCoroutine(float time, bool stop)
    {
        isFadingBGM = true;

        float oldVolume = bgmSource.volume;

        float step = oldVolume / time;

        while (bgmSource.volume > 0)
        {
            bgmSource.volume -= step * Time.deltaTime;

            yield return null;
        }

        if (stop)
            StopBGM();
        else
            PauseBGM();

        bgmSource.volume = oldVolume;

        isFadingBGM = false;
    }
}
