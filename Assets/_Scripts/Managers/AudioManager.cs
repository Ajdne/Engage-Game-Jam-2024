using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    public void PlayTheme()
    {
        PlayAudio(audioClips[1]);
    }

    private void PlayAudio(int index)
    {
        audioSource.clip = audioClips[index];
        audioSource.Play();
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

    // make singleton
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    int index;
    public void PlayStartGame()
    {
        PlayAudio(audioClips[1]);
    }

    public void PlayEnd()
    {
        PlayAudio(audioClips[2]);
    }

    public void PlaySheepSound()
    {
        PlayAudio(audioClips[3]);
    }

    public void PlayPhaseChange()
    {
        PlayAudio(audioClips[5]);
    }

    //play bump sound
    public void PlayBumpSound()
    {
        PlayAudio(audioClips[4]);
    }

    //play clock sound
    public void PlayClockSound()
    {
        PlayAudio(audioClips[6]);
    }
}
