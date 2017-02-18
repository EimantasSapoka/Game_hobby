using System.Collections;
using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class SoundManager : MonoBehaviour
    {

        public static SoundManager Instance;

        public float LowPitchRange = 0.95f;
        public float HighPitchRange = 1.05f;
        public bool RandomizeSoundPitch = true;

        public AudioClip[] SoundTracks;
        public AudioClip MainMenuMusic;

      

        // Use this for initialization
        void Awake ()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            musicPlayer = GetComponents<AudioSource>()[0];
            soundPlayer = GetComponents<AudioSource>()[1];
        }

        private void OnLevelWasLoaded(int level)
        {

            if (level == 0)
            {
                musicPlayer.clip = MainMenuMusic;
            }
            else
            {
                var trackToPlay = SoundTracks[Random.Range(0, SoundTracks.Length)];
                musicPlayer.clip = trackToPlay;

            }
            if (musicPlayer.isActiveAndEnabled)
            {
                musicPlayer.Play();                
                StartCoroutine(MusicFadeIn(musicPlayer.volume));
            }
            
        }

        private IEnumerator MusicFadeIn(float originalVolume)
        {

            musicPlayer.volume = 0;
            for (int i = 0; i < 5; i++)
            {
                musicPlayer.volume += originalVolume*0.2f;
                yield return new WaitForSeconds(.2f);
            }  
        }

        public void FadeOutMusic()
        {
            if (musicPlayer.isActiveAndEnabled)
            {
                StartCoroutine(MusicFadeOut(musicPlayer.volume));
            }
        }

        private IEnumerator MusicFadeOut(float originalVolume)
        {
            for (int i = 0; i < 5; i++)
            {
                musicPlayer.volume -= originalVolume/5;
                yield return new WaitForSeconds(.2f);
            }
            musicPlayer.Stop();
            musicPlayer.volume = originalVolume;
        }

        public  void PlayAudioClip(AudioClip audio)
        {
            PlayRandomAudioClip(audio);
        }

        public  void PlayRandomAudioClip(params AudioClip[] audioClips)
        {
            if (RandomizeSoundPitch)
            {
                soundPlayer.pitch = Random.Range(LowPitchRange, HighPitchRange);
            }
            soundPlayer.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        }


        private  AudioSource musicPlayer;
        private  AudioSource soundPlayer;
        
    }
}
