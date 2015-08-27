using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {     

    public GameObject GameOverPanel;
    public GameObject FadePanel;
    public GameObject MainMenu;
    public GameObject GameUI;

    public static UIManager instance;

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
	    DontDestroyOnLoad(this.gameObject);
	    UIfadeAnimator = FadePanel.GetComponent<Animator>();
	}

    public void StartButtonOnClick()
    {
        Invoke("LoadFirstLevel", 1.0f);
        UIfadeAnimator.SetTrigger("Fade");
    }

    public void LoadUiScreen()
    {
        GameOverPanel.SetActive(false);
        MainMenu.SetActive(true);
    }


    public void ShowGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }

    private void LoadFirstLevel()
    {
        MainMenu.SetActive(false);
        Application.LoadLevel(START_SCENE);
        GameUI.SetActive(true);
    }

    private Animator UIfadeAnimator;
    private readonly int START_SCENE = 1;
}
