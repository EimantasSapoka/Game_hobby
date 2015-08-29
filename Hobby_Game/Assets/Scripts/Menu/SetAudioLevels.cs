using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Menu
{
    public class SetAudioLevels : MonoBehaviour {

        public AudioMixer MainMixer;					//Used to hold a reference to the AudioMixer mainMixer


        //Call this function and pass in the float parameter musicLvl to set the volume of the AudioMixerGroup Music in mainMixer
        public void SetMusicLevel(float musicLvl)
        {
            MainMixer.SetFloat("musicVol", musicLvl);
        }

        //Call this function and pass in the float parameter sfxLevel to set the volume of the AudioMixerGroup SoundFx in mainMixer
        public void SetSfxLevel(float sfxLevel)
        {
            MainMixer.SetFloat("sfxVol", sfxLevel);
        }
    }
}
