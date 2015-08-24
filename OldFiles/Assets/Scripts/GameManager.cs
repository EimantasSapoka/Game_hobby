using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public BoardManager boardScript;
	public static GameManager instance = null;
    public static PlayMusic MusicPlayer = null;
    public GameObject Player = null;
    public GameObject GameUI = null;
	
	public float levelStartDelay = 2f;
	public float turnDelay = .1f;
	private int level = 1;
	public int playerFoodPoints = 100;
	private Text levelText;
	private GameObject levelImage;
	private bool doingSetup;

	private List<Enemy> enemies;
	private bool enemiesMoving;
	[HideInInspector] public bool playersTurn = true;

	// Use this for initialization
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		enemies = new List<Enemy> ();
		DontDestroyOnLoad (gameObject);
		boardScript = GetComponent<BoardManager> ();
	    MusicPlayer = GameObject.Find("UI").GetComponent<PlayMusic>();

	    Instantiate(GameUI);
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        Instantiate(Player);
		InitGame ();
	}

	private void HideLevelImage()
	{
		levelImage.SetActive (false);
		doingSetup = false;
	}

	public void OnLevelWasLoaded(int index)
	{
	    Debug.Log("LEVEL LOADED CALLED, index: " + index);
		level++;
		InitGame ();
	}

	void InitGame()
	{
		doingSetup = true;
		levelText.text = "Day " + level;
		levelImage.SetActive (true);
		Invoke ("HideLevelImage", this.levelStartDelay);
		enemies.Clear ();
	    var board = GameObject.Find("Board");
	    if (board)
	    {
	        DestroyObject(board);
	    }
		boardScript.SetupScene (level);
	}

	public void GameOver()
	{
		levelText.text = "After " + level + " days, you starved.";
		levelImage.SetActive (true);
		enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (playersTurn || enemiesMoving || doingSetup)
			return;

		StartCoroutine (MoveEnemies ());

	}

	public void AddEnemyToList(Enemy script)
	{
		enemies.Add (script);
	}

	IEnumerator MoveEnemies()
	{
		enemiesMoving = true;
		yield return new WaitForSeconds(turnDelay);

		if (enemies.Count == 0) {
			yield return new WaitForSeconds (turnDelay);
		}

		for (int i = 0; i < enemies.Count; i++) {
			enemies[i].MoveEnemy();
			yield return new WaitForSeconds(enemies[i].moveTime);
		}
		
		playersTurn = true;
		enemiesMoving = false;

	}
}
