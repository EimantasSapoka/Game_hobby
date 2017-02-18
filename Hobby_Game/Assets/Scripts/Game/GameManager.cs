using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Menu;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
                enabled = false;
                gameObject.SetActive(false);
                return;
            }
            
            enemies = new List<Enemy>();
            BoardManager = FindObjectOfType<BoardManager>();
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnEnable()
        {
            Player.Instance.OnPlayerDeath += GameOver;
            Player.Instance.OnPlayerMoveFinished += () => PlayerTurn = false;
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "Menu")
            {
                Destroy(this);
                return;
            }
            Level++;
            GameUI.Instance.ShowLevelTransition(1.0f, Level);
            enemies.Clear();
            BoardManager.GenerateLevel(Level);
            PlayerTurn = true;
            GamePaused = false;
            Player.Instance.enabled = true;
            
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
            SceneManager.LoadScene("Menu");
        }

        private float turnDelay = .1f;
        private bool enemiesMoving;
        private List<Enemy> enemies;

        public void LoadNextLevel()
        {
            GamePaused = true;
            Player.Instance.enabled = false;
            Invoke("LoadLevel", 1.0f);
        }

    }
}
