using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    public void UpdateFoodText(int food, int amount)
    {
        var plus = amount > 0 ? "+" : "";
        FoodText.text = string.Format("Food: {0} {1}{2}", food, plus, amount);
    }
}
