using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
    }
}
