﻿using Assets.Scripts.Game.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Enemy : MovingObject, IInteractsWithPlayer {

        public int PlayerDamage;
       

        protected override void Awake () {
            GameManager.Instance.AddEnemyToList (this);
            animator = GetComponent<Animator> ();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            audioSource = GetComponent<AudioSource>();
            base.Awake ();
        }

        protected override void AttemptMove(int xDir, int yDir)
        {
            if (skipMove)
            {
                skipMove = false;
                return;
            }

            base.AttemptMove(xDir,yDir);
            skipMove = true;
        }

        public void MoveEnemy()
        {
            int xDir = 0;
            int yDir = 0;

            if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
                yDir = target.position.y > transform.position.y ? 1 : -1;
            else 
                xDir = target.position.x > transform.position.x ? 1 : -1;
            AttemptMove (xDir, yDir);

        }

        void IInteractsWithPlayer.InteractWithPlayer(Player player)
        {
            player.DamagePlayer(PlayerDamage);
            animator.SetTrigger ("Hit");
            audioSource.Play();
        }

        private AudioSource audioSource;
        private Animator animator;
        private Transform target;
        private bool skipMove;
    }
}
