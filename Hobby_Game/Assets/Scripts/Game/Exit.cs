using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Exit : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Player>() != null)
            {
                GameManager.Instance.LoadNextLevel();
            }

   
        }
    }
}
