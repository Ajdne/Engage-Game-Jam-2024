using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : NonPersistentSingleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
//make 6 individual serializable audioclip fields, for theme, startgame, end, sheep, bump, phasechange
    [SerializeField] private AudioClip theme;
    [SerializeField] private AudioClip startGame;
    [SerializeField] private AudioClip end;
    [SerializeField] private AudioClip sheep;
    [SerializeField] private AudioClip bump;
    [SerializeField] private AudioClip phaseChange;
    [SerializeField] private AudioClip clockSound;

    public void PlayTheme()
    {
        PlayAudio(theme);
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
    //make method playaudio(audioclip a)
    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    


    public void PauseAudio()
    {
        audioSource.Pause();
    }

    public void UnPauseAudio()
    {
        audioSource.UnPause();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void SetPitch(float pitch)
    {
        audioSource.pitch = pitch;
    }

    public void SetLoop(bool loop)
    {
        audioSource.loop = loop;
    }

    private void Awake()
    {
        Initialize();
    }

    int index;
    public void PlayStartGame()
    {
        PlayAudio(startGame);
    }

    public void PlayEnd()
    {
        PlayAudio(end);
    }

    public void PlaySheepSound()
    {
        PlayAudio(sheep);
    }

    public void PlayBumpSound()
    {
        PlayAudio(bump);
    }

    public void PlayPhaseChange()
    {
        PlayAudio(phaseChange);
    }

    //play clock sound
    public void PlayClockSound()
    {
        PlayAudio(clockSound);
    }

    //make start function, bc no sounds are playing at start
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
