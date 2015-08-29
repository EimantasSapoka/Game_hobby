using System.Collections;
using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class SoundManager : MonoBehaviour
    {

        public static SoundManager Instance;
        private AudioSource audioSource;

        public AudioClip[] SoundTracks;

      

        // Use this for initialization
        void Awake ()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                DestroyObject(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);

            audioSource = GetComponent<AudioSource>();
        }

        private void OnLevelWasLoaded()
        {
            var trackToPlay = SoundTracks[Random.Range(0, SoundTracks.Length)];
            audioSource.clip = trackToPlay;
            if (audioSource.isActiveAndEnabled)
            {
                audioSource.Play();
                StartCoroutine(MusicFadeIn(audioSource.volume));
            }
        }

        private IEnumerator MusicFadeIn(float originalVolume)
        {
            audioSource.volume = 0;
            for (int i = 0; i < 5; i++)
            {
                audioSource.volume += originalVolume*0.2f;
                yield return new WaitForSeconds(.2f);

            }  
        }

        
    }
}
