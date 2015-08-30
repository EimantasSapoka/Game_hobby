using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Menu;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [HideInInspector] public static BoardManager BoardManager;
        [HideInInspector] public bool PlayerTurn;
        public int Level = 0;
        public bool GamePaused;

    

        // Use this for initialization
        void Awake ()
        {

            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            enemies = new List<Enemy>();
            BoardManager = GameObject.FindObjectOfType<BoardManager>();
            DontDestroyOnLoad(gameObject);

        }

        private void OnEnable()
        {
            Player.Instance.OnPlayerDeath += GameOver;
            Player.Instance.OnPlayerMoveFinished += () => PlayerTurn = false;
        }


        private void OnLevelWasLoaded(int levelLoaded)
        {
            if (levelLoaded == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                Level++;
                print("showing level transition");
                GameUI.Instance.ShowLevelTransition(1.0f, Level);
                enemies.Clear();
                BoardManager.GenerateLevel(Level);
                PlayerTurn = true;
                GamePaused = false;
                Player.Instance.enabled = true;
            }
        }
	
        // Update is called once per frame
        void Update () {
            if (PlayerTurn || enemiesMoving || GamePaused)
            {
                return;
            }
            StartCoroutine(MoveEnemies());
 
        }

        protected IEnumerator MoveEnemies()
        {
            enemiesMoving = true;
            yield return new WaitForSeconds(turnDelay);
            foreach (var t in enemies)
            {
                t.MoveEnemy();
            }
            yield return new WaitForSeconds(turnDelay);

            enemiesMoving = false;
            PlayerTurn = true;

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
            SoundManager.Instance.FadeOutMusic();
            Application.LoadLevel(0);
        }

        private float turnDelay = .2f;
        private bool enemiesMoving;
        private List<Enemy> enemies;

        public void LoadNextLevel()
        {
            GamePaused = true;
            Player.Instance.enabled = false;
            Invoke("LoadLevel", 1.0f);

        }

        public void LoadLevel()
        {
            Application.LoadLevel(Application.loadedLevel);
        }


    }
}
