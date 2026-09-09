using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Audio Source")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip flySound;
    public AudioClip gameoverSound;
    public AudioClip deathSound;
    public AudioClip highScoreSound;
    
    public AudioClip bgmSound;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGM(bgmSound);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (musicSource == null)
        {
            Debug.LogError("[AudioManager] musicSource AudioSource component is NOT assigned in Inspector!");
            return;
        }

        if (clip == null)
        {
            Debug.LogError("[AudioManager] The AudioClip passed to PlayBGM is null!");
            return;
        }

        // If this exact clip is already playing, don't restart it
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void playSFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
