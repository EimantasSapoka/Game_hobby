﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public BoardManager boardManager;

    public bool PlayerTurn;

    public int level = 1;
    private float turnDelay = .1f;
    public int playerFoodPoints = 100;
    private bool enemiesMoving;
    private List<Enemy> enemies;

    // Use this for initialization
	void Awake ()
	{
	    if (instance == null)
	    {
            instance = this;
	    }
	    else if (instance != this)
	    {
	        DestroyObject(this.gameObject);
	    }

	    enemies = new List<Enemy>();

	    DontDestroyOnLoad(this.gameObject);
	    boardManager = GetComponent<BoardManager>();
	    OnLevelWasLoaded();
	}

    private void OnLevelWasLoaded()
    {
        Debug.Log("Loading level");
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
       // levelText.text = "After " + level + " days, you starved.";
       // levelImage.SetActive(true);
        enabled = false;

        Invoke("LoadMenu", 3.0f);
    }

    private void LoadMenu()
    {
        UIManager.instance.LoadUiScreen();
        Application.LoadLevel(0);
        DestroyObject(this.gameObject);
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }
}
