using UnityEngine;

namespace Assets.Scripts.Game
{
    public class CameraScript : MonoBehaviour
    {

        private Transform player;


        // Use this for initialization
        void Awake () {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
	
        // Update is called once per frame
        void Update ()
        {
            float boardHeight = GameManager.BoardManager.BoardHeight;
            float boardWidth = GameManager.BoardManager.BoardWidth;
                
            var vertExtent = Camera.main.orthographicSize;
            
            if (vertExtent > boardHeight/2)
            {
                Camera.main.orthographicSize = boardHeight;
                vertExtent = Camera.main.orthographicSize;
            }
            
            var horzExtent = (vertExtent *  Screen.width) / Screen.height;
            if (horzExtent > boardWidth/2)
            {
                Camera.main.orthographicSize = (boardWidth/2 * Screen.height) / Screen.width;
                vertExtent = Camera.main.orthographicSize;
                horzExtent = (vertExtent * Screen.width) / Screen.height;
            }
            var v3 = transform.position;
            v3.x = Mathf.Clamp(player.position.x, horzExtent, boardWidth-horzExtent);
            v3.y = Mathf.Clamp(player.position.y, vertExtent, boardHeight-vertExtent);
            transform.position = v3;
        }
    }
}
