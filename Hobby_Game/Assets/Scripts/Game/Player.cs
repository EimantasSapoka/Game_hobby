using System;
using Assets.Scripts.Game.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Player : MovingObject, IWallDamage, IInteractable, IItemConsumer
    {

        public static Player Instance;
        public int StartingFood = 100;
        public int Damage = 1;
        public int PointsPerFood = 10;
        public int PointsPerSoda = 20;

        public float RestartLevelDelay = 1f;

        public event Action OnPlayerDamaged;
        public event Action OnPlayerMoved;
        public event Action OnPlayerDeath;
        public event Action<int> OnPlayerFoodChanged;
        public event Action OnPlayerMoveFinished;


        private int food;
        public int Food
        {
            get { return food; }
            private set
            {
                GameUI.Instance.UpdateFoodText(value);
                food = value;
                CheckIfGameOver();
            } 
        }

    

        // Use this for initialization
        protected override void Awake () {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                DestroyObject(gameObject);
            }
            DontDestroyOnLoad(gameObject);

            food = StartingFood;

            animator = GetComponent<Animator> ();
            base.Awake ();
        }

        // Update is called once per frame
        void Update () {
            if (!GameManager.Instance.PlayerTurn)
                return;

            if (Input.GetKey(KeyCode.Space))
            {
                LaunchAction(OnPlayerMoveFinished);
                return;
            }
            var horizontal = (int)Input.GetAxisRaw ("Horizontal");
            var vertical = (int)Input.GetAxisRaw ("Vertical");

            if (horizontal != 0)
                vertical = 0;

            if (horizontal != 0 || vertical != 0)
                AttemptMove (horizontal, vertical);
        }

        protected override bool AttemptMove(int xDir, int yDir)
        {
            Food = Food - 1;
            base.AttemptMove (xDir, yDir);
            RaycastHit2D hit;
            var moved = Move(xDir, yDir, out hit);
            if (moved)
            {
                LaunchAction(OnPlayerMoved);
            }
            LaunchAction(OnPlayerMoveFinished);
            return moved;
        }
      

        public void IncreaseFood(int amount)
        {
            Food = Food + amount;
            LaunchAction<int>(OnPlayerFoodChanged, amount);
        }

        public void ReduceFood(int amount)
        {
            Food =Food - amount;
            LaunchAction<int>(OnPlayerFoodChanged, amount*-1);
        }

        public void DamagePlayer(int damage)
        {
            animator.SetTrigger("Player_Hit");
            ReduceFood(damage);
            LaunchAction(OnPlayerDamaged);
        }


        private void CheckIfGameOver()
        {
            if (Food <= 0)
            {
                LaunchAction(OnPlayerDeath);
            }
        }

        private void LaunchAction(Action onAction)
        {
            if (onAction != null)
            {
                onAction();
            }
        }

        private void LaunchAction<T>(Action<T> onAction, T value)
        {
            if (onAction != null)
            {
                onAction(value);
            }
        }

        private void OnLevelWasLoaded(int level)
        {
            if (level == 0)
            {
                Instance = null;
                DestroyObject(gameObject);
            }
            else
            {
                transform.position = new Vector3(1, 1, 0);
            }
        }



        int IWallDamage.GetWallDamage()
        {
            animator.SetTrigger("Chop");
            return Damage;
        }

        void IInteractable.Interact(Component sender)
        {
            var playerInteractable = sender.GetComponent<IInteractsWithPlayer>();
            if (playerInteractable != null)
            {
                playerInteractable.InteractWithPlayer(this);
            }
        }


        private Animator animator;

        
    }
}
