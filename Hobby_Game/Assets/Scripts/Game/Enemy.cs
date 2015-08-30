using Assets.Scripts.Game.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public abstract class Enemy : MovingObject, IInteractsWithPlayer {

        public int PlayerDamage;

        protected override void Awake()
        {
            animator = GetComponent<Animator>();
            Target = GameObject.FindGameObjectWithTag("Player").transform;
            audioSource = GetComponent<AudioSource>();
            base.Awake();
        }

        protected void Start()
        {
            FindObjectOfType<GameManager>().AddEnemyToList(this);
        }

        public virtual void InteractWithPlayer(Player player)
        {
            player.DamagePlayer(PlayerDamage);
            TriggerHitAnimation();
        }

        protected void TriggerHitAnimation()
        {
            animator.SetTrigger("Hit");
            audioSource.Play();
        }

        public abstract void MoveEnemy();
        private AudioSource audioSource;
        private Animator animator;
        protected Transform Target;
    }
}
