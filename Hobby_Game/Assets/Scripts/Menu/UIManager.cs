using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {     

    private readonly int START_SCENE = 1;
    private GameObject MainMenu;

    private Animator UIfadeAnimator;

    // Use this for initialization
	void Awake ()
	{
	    DontDestroyOnLoad(this.gameObject);
	    MainMenu = GameObject.Find("Main UI Screen");
	    UIfadeAnimator = GameObject.Find("Fade").GetComponent<Animator>();

	}

    public void StartButtonOnClick()
    {
        Debug.Log("starting the game scene");
        Invoke("LoadFirstLevel", 1.0f);
        UIfadeAnimator.SetTrigger("Fade");
    }

    private void LoadFirstLevel()
    {
        MainMenu.SetActive(false);
        Application.LoadLevel(START_SCENE);
    }

	
	
}
