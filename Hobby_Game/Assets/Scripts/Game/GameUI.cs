using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    public class GameUI : MonoBehaviour
    {

        public GameObject GameOverPanel;
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
                DestroyObject(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            Player.Instance.OnPlayerFoodChanged += PlayerFoodChange;
        }

        private void OnLevelWasLoaded(int level)
        {
            if (level == 0)
            {
                DestroyObject(gameObject);
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
    }
}
