using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public BoardManager BoardManager;
    [HideInInspector] public bool PlayerTurn;
    public int Level = 1;
    
    

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
	    BoardManager = GetComponent<BoardManager>();
	}

    private void OnEnable()
    {
        Player.OnPlayerDeath += () => GameManager.Instance.GameOver();
    }


    private void OnLevelWasLoaded(int levelLoaded)
    {
        if (levelLoaded == 0)
        {
            DestroyObject(gameObject);
        }
        else
        {
           BoardManager.GenerateLevel(Level);
           PlayerTurn = true;  
        }
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
        foreach (var t in enemies)
        {
            t.MoveEnemy();
            yield return null;
        }
        yield return new WaitForSeconds(turnDelay);

        PlayerTurn = true;
        enemiesMoving = false;

    }

    public void GameOver()
    {
        GameUI.Instance.ShowGameOverPanel();
        enabled = false;
        Invoke("LoadMenu", 3.0f);
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    private void LoadMenu()
    {
        Application.LoadLevel(0);
        DestroyObject(gameObject);
    }

    private float turnDelay = .3f;
    private bool enemiesMoving;
    private List<Enemy> enemies;
}
