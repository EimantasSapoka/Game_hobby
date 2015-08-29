using UnityEngine;

namespace Assets.Scripts.Game
{
    public class CameraScript : MonoBehaviour
    {

        private Transform player;
        public float Offset = 1f;


        // Use this for initialization
        void Awake () {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
	
        // Update is called once per frame
        void LateUpdate ()
        {
            float boardHeight = GameManager.Instance.BoardManager.BoardHeight;
            float boardWidth = GameManager.Instance.BoardManager.BoardWidth;
                
            var vertExtent = Camera.main.orthographicSize;
            var horzExtent = vertExtent *  Screen.width / Screen.height;

            var v3 = transform.position;
            v3.x = Mathf.Clamp(player.position.x, horzExtent, boardWidth-horzExtent-Offset);
            v3.y = Mathf.Clamp(player.position.y, vertExtent, boardHeight-vertExtent-Offset);
            transform.position = v3;
        }
    }
}
