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
		food = GameManager.instance.playerFoodPoints;
		base.Start ();
	}

	private void OnDisable()
	{
		GameManager.instance.playerFoodPoints = food;
	}


	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.playersTurn)
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
			SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
		}
		CheckIfGameOver ();
		GameManager.instance.playersTurn = false;
	}
	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Exit") {
			Invoke ("Restart", restartLevelDelay);
			enabled = false;
		} else if (other.tag == "Food") {
			food += pointsPerFood;
			foodText.text = "+"  + pointsPerFood + " Food: " + food;
			other.gameObject.SetActive (false);
			SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
		} else if (other.tag == "Soda") {
			food += pointsPerSoda;
			foodText.text = "+"  + pointsPerSoda + " Food: " + food;
			other.gameObject.SetActive(false);
			SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
		}
	}

	protected override void OnCantMove<T>(T component)
	{
		if (component is Wall) {
			Wall hitWall = component as Wall;
			hitWall.DamageWall (wallDamage);
			animator.SetTrigger ("Chop");
		}

	}

	private void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
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
			SoundManager.instance.PlaySinge(gameOverSound);
			SoundManager.instance.musicSource.Stop();
			GameManager.instance.GameOver ();
		}
	}
}
