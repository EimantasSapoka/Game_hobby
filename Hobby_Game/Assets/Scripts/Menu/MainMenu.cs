using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class MainMenu : MonoBehaviour
    {

        public GameObject FadePanel;

        // Use this for initialization
        void Start()
        {
            uIfadeAnimator = FadePanel.GetComponent<Animator>();
        }

        public void StartButtonOnClick()
        {
            Invoke("LoadFirstLevel", 1.5f);
            uIfadeAnimator.SetTrigger("Fade");
            SoundManager.Instance.FadeOutMusic();
        }

        private void LoadFirstLevel()
        {
            Application.LoadLevel(START_SCENE);
        }

        private Animator uIfadeAnimator;
        private readonly int START_SCENE = 1;
    }
}
