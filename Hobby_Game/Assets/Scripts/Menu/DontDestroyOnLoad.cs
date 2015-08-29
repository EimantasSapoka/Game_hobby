using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class DontDestroyOnLoad : MonoBehaviour {

        // Use this for initialization
        void Awake ()
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }
}
