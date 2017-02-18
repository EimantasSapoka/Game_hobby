using Assets.Scripts.Menu;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Game
{
    public class SingletonLoader : MonoBehaviour {

        public GameObject SoundManager;

        void Awake ()
        {
            OnLevelLoaded();
        }

        private void OnLevelLoaded()
        {
            if (!FindObjectOfType<EventSystem>())
            {
                //The following code instantiates a new object called EventSystem
                var obj = new GameObject("EventSystem");

                //And adds the required components
                obj.AddComponent<EventSystem>();
                obj.AddComponent<StandaloneInputModule>().forceModuleActive = true;
                obj.AddComponent<TouchInputModule>();
            }
            if (!FindObjectOfType<SoundManager>())
            {
                Instantiate(SoundManager);
            }

        }
    }
}
