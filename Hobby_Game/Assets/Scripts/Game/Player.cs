﻿using System;
using UnityEngine;
using Assets.Scripts.Game.Interfaces;

public class Player : MovingObject, IWallDamage, IInteractable{

	public AudioClip MoveSound1;
	public AudioClip MoveSound2;
	public AudioClip EatSound1;
	public AudioClip EatSound2;
	public AudioClip DrinkSound1;
	public AudioClip DrinkSound2;
	public AudioClip GameOverSound;

    private Player instance;
    public int StartingFood = 100;
	public int Damage = 1;
	public int PointsPerFood = 10;
	public int PointsPerSoda = 20;

	public float RestartLevelDelay = 1f;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerMoved;
    public static event Action OnPlayerDeath;
    public static event Action<int> OnPlayerFoodChanged;


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
	protected override void Start () {
	    if (instance == null)
	    {
            instance = this;
	    }
        else if (instance != this)
        {
            DestroyObject(gameObject);
        }
	    DontDestroyOnLoad(gameObject);

	    food = StartingFood;

		animator = GetComponent<Animator> ();
		base.Start ();
	}

	// Update is called once per frame
	void Update () {
		if (!GameManager.Instance.PlayerTurn)
			return;

	    var horizontal = (int)Input.GetAxisRaw ("Horizontal");
		var vertical = (int)Input.GetAxisRaw ("Vertical");

		if (horizontal != 0)
			vertical = 0;

		if (horizontal != 0 || vertical != 0)
			AttemptMove (horizontal, vertical);
	}

	protected override void AttemptMove(int xDir, int yDir)
	{
	    Food--;
		base.AttemptMove (xDir, yDir);
		RaycastHit2D hit;
		if (Move (xDir, yDir, out hit))
		{
		    LaunchAction(OnPlayerMoved);
		}
		CheckIfGameOver ();
		GameManager.Instance.PlayerTurn = false;
	}
	private void OnTriggerEnter2D (Collider2D other)
	{
	    //
	}

    public void IncreaseFood(int amount)
    {
        Food += amount;
        LaunchAction<int>(OnPlayerFoodChanged, amount);
    }

    public void ReduceFood(int amount)
    {
        Food -= amount;
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
