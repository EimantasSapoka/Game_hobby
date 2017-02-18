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


          /*  float boardHeight = GameManager.BoardManager.BoardHeight;
            float boardWidth = GameManager.BoardManager.BoardWidth;

            var vertExtent = Camera.main.orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height;

            var v3 = transform.position;
            v3.x = Mathf.Clamp(player.position.x, horzExtent, boardWidth - horzExtent - Offset);
            v3.y = Mathf.Clamp(player.position.y, vertExtent, boardHeight - vertExtent - Offset);
            transform.position = v3;*/
        }


        private void OnEnable()
        {
            UpdateFoodText(Player.Instance.Food);
            Player.Instance.OnPlayerFoodChanged += PlayerFoodChange;
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
