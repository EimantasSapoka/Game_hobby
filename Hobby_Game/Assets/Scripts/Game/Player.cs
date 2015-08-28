using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MovingObject {

	public AudioClip moveSound1;
	public AudioClip moveSound2;
	public AudioClip eatSound1;
	public AudioClip eatSound2;
	public AudioClip drinkSound1;
	public AudioClip drinkSound2;
	public AudioClip gameOverSound;

	public int wallDamage = 1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;

	public float restartLevelDelay = 1f;

	private Animator animator;

	private int food;

	public Text foodText;

	// Use this for initialization
	protected override void Start () {
		animator = GetComponent<Animator> ();
		food = GameManager.Instance.playerFoodPoints;
	    foodText = GameObject.Find("FoodText").GetComponent<Text>();
		base.Start ();
	}

	private void OnDisable()
	{
		GameManager.Instance.playerFoodPoints = food;
	}


	// Update is called once per frame
	void Update () {
		if (!GameManager.Instance.PlayerTurn)
			return;
		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)Input.GetAxisRaw ("Horizontal");
		vertical = (int)Input.GetAxisRaw ("Vertical");

		if (horizontal != 0)
			vertical = 0;

		if (horizontal != 0 || vertical != 0)
			AttemptMove<Wall> (horizontal, vertical);
	}

	protected override void AttemptMove<T> (int xDir, int yDir)
	{
		food --;
		foodText.text = "Food: " + food;
		base.AttemptMove<T> (xDir, yDir);
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

	protected override void OnCantMove<T>(T component)
	{
		if (component is Wall) {
			Wall hitWall = component as Wall;
			hitWall.DamageWall (wallDamage);
			animator.SetTrigger ("Chop");
		}

	}


	public void LoseFood(int loss)
	{
		animator.SetTrigger ("Player_Hit");
		food -= loss;
		foodText.text = "- " + loss + " " + foodText.text;
		CheckIfGameOver ();
	}

	private void CheckIfGameOver()
	{
		if (food <= 0) {
			//GameManager.MusicPlayer.PlaySinge(gameOverSound);
			GameManager.Instance.GameOver ();
		}
	}
}
