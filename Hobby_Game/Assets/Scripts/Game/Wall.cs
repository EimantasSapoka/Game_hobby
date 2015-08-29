using Assets.Scripts.Game.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Wall : MonoBehaviour, IInteractable {

        public Sprite DmgSprite;
        public int Hp=4;
        private SpriteRenderer spriteRenderer;

        public AudioClip ChopSound1;
        public AudioClip ChopSound2;


        // Use this for initialization
        void Awake () {
            spriteRenderer = GetComponent<SpriteRenderer> ();

        }
	
        public void DamageWall(int loss)
        {
            //GameManager.MusicPlayer.RandomizeSfx (chopSound1, chopSound2);
            spriteRenderer.sprite = DmgSprite;
            Hp -= loss;
            if (Hp == 0)
                gameObject.SetActive (false);
        }


        void IInteractable.Interact(Component sender)
        {
            var damager = sender.GetComponent<IWallDamage>();
            if (damager != null)
            {
                DamageWall(damager.GetWallDamage());
            }

        }
    }
}
