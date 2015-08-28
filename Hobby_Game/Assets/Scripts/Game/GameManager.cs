using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public BoardManager boardManager;

    public bool PlayerTurn;
    public int playerFoodPoints = 100;

    public int level = 1;
    
    

    // Use this for initialization
	void Awake ()
	{
	    if (Instance == null)
	    {
            Instance = this;
	    }
	    else if (Instance != this)
	    {
	        DestroyObject(gameObject);
	    }

	    enemies = new List<Enemy>();

	    DontDestroyOnLoad(gameObject);
	    boardManager = GetComponent<BoardManager>();
        OnLevelWasLoaded();
	}

    private void OnLevelWasLoaded()
    {
        boardManager.GenerateLevel(level);
        PlayerTurn = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerTurn || enemiesMoving)
        {
            return;
        }
	    StartCoroutine(MoveEnemies());
 
	}

    protected IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        PlayerTurn = true;
        enemiesMoving = false;

    }

    public void GameOver()
    {
        UIManager.instance.ShowGameOverPanel();
        enabled = false;
        Invoke("LoadMenu", 3.0f);
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }

     private void LoadMenu()
    {
        UIManager.instance.LoadUiScreen();
        Application.LoadLevel(0);
        DestroyObject(this.gameObject);
    }

    private float turnDelay = .1f;
    private bool enemiesMoving;
    private List<Enemy> enemies;
}
