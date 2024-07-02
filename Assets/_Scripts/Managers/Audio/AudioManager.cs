using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets._Scripts.Managers.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void PlayMusic(string name)
        {
            Sound s = Array.Find(musicSounds, x => x.Name == name);
            if (s == null)
            {
                Debug.Log("Sound Not Found");
            }
            else
            {
                musicSource.clip = s.Clip;
                musicSource.Play();
            }
        }
        public void PlaySFX(string name)
        {
            Sound s = Array.Find(sfxSounds, x => x.Name == name);
            if (s == null)
            {
                Debug.Log("SFX Not Found");
            }
            else
            {
                sfxSource.PlayOneShot(s.Clip);
            }
        }
        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }
        public void ToggleSFX()
        {
            sfxSource.mute = !sfxSource.mute;
        }
        public void MusicVolume(float volume)
        {
            musicSource.volume = volume;
        }
        public void SFXVolume(float volume)
        {
            sfxSource.volume = volume;
        }
        private void OnEnable()
        {
            EventManager.SFXEvent += PlaySFX;
            EventManager.SoundEvent += PlayMusic;
        }
        private void OnDisable()
        {
            EventManager.SFXEvent -= PlaySFX;
            EventManager.SoundEvent -= PlayMusic;
        }
    }
}
