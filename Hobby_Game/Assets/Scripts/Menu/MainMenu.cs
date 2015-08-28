using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject FadePanel;

    // Use this for initialization
    void Start()
    {
        UIfadeAnimator = FadePanel.GetComponent<Animator>();
        LoadFirstLevel();
    }

    public void StartButtonOnClick()
    {
        Invoke("LoadFirstLevel", 1.0f);
        UIfadeAnimator.SetTrigger("Fade");
    }

    private void LoadFirstLevel()
    {
        Application.LoadLevel(START_SCENE);
    }

    private Animator UIfadeAnimator;
    private readonly int START_SCENE = 1;
}
