using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    private Transform player;


	// Use this for initialization
	void Awake () {
	    player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
	    float x;
	    float y;

	    float boardHeight = GameManager.instance.boardManager.BoardHeight;
	    float boardWidth = GameManager.instance.boardManager.BoardWidth;
                
        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        y = player.position.y < vertExtent ? vertExtent : player.position.y;

	    var playerX = player.position.x;
        var playerY = player.position.y;

	    if (playerX < horzExtent)
	    {
	        x = horzExtent;
        }
        else if (playerX > boardWidth - horzExtent)
        {
            x = boardWidth - horzExtent;
        }
        else
        {
            x = playerX;
        }

	    if (playerY < vertExtent)
	    {
	        y = vertExtent;
	    } else if (playerY > boardHeight - vertExtent)
	    {
	        y = boardHeight - vertExtent;
	    }
	    else
	    {
            y = playerY;
	    }
	    
	    this.transform.position = new Vector3(x, y, -10);

	}
}
