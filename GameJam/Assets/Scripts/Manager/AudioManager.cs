using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip LevelOne;
    public AudioClip LevelTwo;

    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private static AudioManager mInstance;
    public static AudioManager Instance { get { return mInstance; } }
    private void Awake()
    {
        if (mInstance != null && mInstance != this)
            Destroy(gameObject);
        else
            mInstance = this;
        DontDestroyOnLoad(gameObject);
        musicSource.loop = true;
    }

    public static void FadeOutMusic()
    {
        mInstance.StartCoroutine(BeginFadeOut(2.0f));
    }

    public static void PlaySfx(AudioClip clip, float volume = 1.0f)
    {
        Instance.sfxSource.clip = clip;
        Instance.sfxSource.volume = volume;
        Instance.sfxSource.Play();
    }

    private static IEnumerator BeginFadeOut(float duration)
    {
        var muteMusicSS = Instance.audioMixer.FindSnapshot("MuteMusic");
        Instance.audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[] { muteMusicSS },
                                            new float[] { 1.0f },
                                            duration);

        yield return new WaitForSeconds(duration);
        // at this point the transition is done.

        var defaultSS = Instance.audioMixer.FindSnapshot("Default");
        Instance.audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[] { defaultSS },
                                     new float[] { 1.0f },
                                     duration);
    }

    public static void SetMasterVolume(float vol)
    {
        if (vol <= -50.0f)
            vol = -80.0f;
        Instance.audioMixer.SetFloat("Master_Volume", vol);
    }

    public static void SetMusicVolume(float vol)
    {
        if (vol <= -50.0f)
            vol = -80.0f;
        Instance.audioMixer.SetFloat("Music_Volume", vol);
    }

    public static void SetSFXVolume(float vol)
    {
        if (vol <= -50.0f)
            vol = -80.0f;
        Instance.audioMixer.SetFloat("SFX_Volume", vol);
    }
}
