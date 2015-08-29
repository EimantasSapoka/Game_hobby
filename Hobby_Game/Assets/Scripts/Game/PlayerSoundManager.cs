using UnityEngine;

namespace Assets.Scripts.Game
{
    public class PlayerSoundManager : MonoBehaviour
    {
        public AudioClip PlayerDamaged;
        public AudioClip PlayerMove;
        public AudioClip MoveSound1;
        public AudioClip MoveSound2;
        public AudioClip EatSound1;
        public AudioClip EatSound2;
        public AudioClip DrinkSound1;
        public AudioClip DrinkSound2;
        public AudioClip GameOverSound;

        private AudioSource audioSource;
        private Player player;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            player = GetComponent<Player>();

        }

        void OnEnable()
        {
            player.OnPlayerDamaged += () => audioSource.PlayOneShot(PlayerDamaged);
            player.OnPlayerMoved += () => audioSource.PlayOneShot(PlayerMove);
            player.OnPlayerDeath += () => audioSource.PlayOneShot(GameOverSound);
        }  

    }
}
