using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    public class GameUI : MonoBehaviour
    {

        public GameObject GameOverPanel;
        public GameObject LevelTransitionPanel;
        public Text LevelTransitionText;
        public static GameUI Instance;
        public Text FoodText;


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
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {

            if (Input.GetKey(KeyCode.Escape))
            {
                Application.LoadLevel(Application.loadedLevel);
            }

        }


        private void OnEnable()
        {
            UpdateFoodText(Player.Instance.Food);
            Player.Instance.OnPlayerFoodChanged += PlayerFoodChange;
        }

        private void OnLevelWasLoaded(int level)
        {
            if (level == 0)
            {
                Destroy(gameObject);
            }
        }

        public void ShowGameOverPanel()
        {
            GameOverPanel.SetActive(true);
        }

        public void UpdateFoodText(int food)
        {
            FoodText.text = "Food: " + food;    
        }

        public void PlayerFoodChange(int amount)
        {
            var plus = amount > 0 ? "+" : "";
            FoodText.text = string.Format("{0} {1}{2}", FoodText.text, plus, amount);
        }

        public void ShowLevelTransition(float time, int level)
        {
            StartCoroutine(LevelTransitionCoroutine(time, level));
            
        }

        private IEnumerator LevelTransitionCoroutine(float time, int level)
        {
            LevelTransitionPanel.SetActive(true);
            LevelTransitionText.text = "Day " + level;
            yield return new WaitForSeconds(time);
            LevelTransitionPanel.SetActive(false);
        }
    }
}
