using UnityEngine;
using Assets.Scripts.Game.Interfaces;
using UnityEngine.UI;

public class Player : MovingObject, IWallDamage, IInteractable{

	public AudioClip MoveSound1;
	public AudioClip MoveSound2;
	public AudioClip EatSound1;
	public AudioClip EatSound2;
	public AudioClip DrinkSound1;
	public AudioClip DrinkSound2;
	public AudioClip GameOverSound;

    public Player Instance;
    public int StartingFood = 100;
	public int Damage = 1;
	public int PointsPerFood = 10;
	public int PointsPerSoda = 20;

	public float RestartLevelDelay = 1f;

    private int food;
    public int Food
    {
        get { return food; }
        private set
        {
            GameUI.Instance.UpdateFoodText(Food);
            food = value;
            CheckIfGameOver();
        } 
    }

    // Use this for initialization
	protected override void Start () {
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
	    Food --;
		base.AttemptMove (xDir, yDir);
		RaycastHit2D hit;
		if (Move (xDir, yDir, out hit)) {
			//GameManager.MusicPlayer.RandomizeSfx(moveSound1, moveSound2);
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
        GameUI.Instance.UpdateFoodText(Food, amount);
    }

    public void ReduceFood(int amount)
    {
        Food -= amount;
        GameUI.Instance.UpdateFoodText(Food, amount*-1);
    }

    public void DamagePlayer(int damage)
    {
        animator.SetTrigger("Player_Hit");
        ReduceFood(damage);
    }


	private void CheckIfGameOver()
	{
		if (Food <= 0) {
			//GameManager.MusicPlayer.PlaySinge(gameOverSound);
			GameManager.Instance.GameOver ();
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
