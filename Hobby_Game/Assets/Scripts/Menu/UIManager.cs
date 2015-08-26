using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {     

    private readonly int START_SCENE = 1;
    private GameObject MainMenu;

    public static UIManager instance;

    private Animator UIfadeAnimator;
    private GameObject fadePanel;

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
	    MainMenu = GameObject.Find("Main UI Screen");
	    fadePanel = GameObject.Find("Fade");

	    UIfadeAnimator = fadePanel.GetComponent<Animator>();

	}

    public void StartButtonOnClick()
    {
        Invoke("LoadFirstLevel", 1.0f);
        UIfadeAnimator.SetTrigger("Fade");
    }

    private void LoadFirstLevel()
    {
        MainMenu.SetActive(false);
        Application.LoadLevel(START_SCENE);
    }

    public void LoadUiScreen()
    {
        MainMenu.SetActive(true);
    }

	
	
}
