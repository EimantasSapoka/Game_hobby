using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour
{

    public GameObject GameOverPanel;
    public static GameUI Instance;

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
	
}
