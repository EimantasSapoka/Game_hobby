using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    private Transform player;
    public float offset = 1f;


	// Use this for initialization
	void Awake () {
	    player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
	    float boardHeight = GameManager.instance.boardManager.BoardHeight;
	    float boardWidth = GameManager.instance.boardManager.BoardWidth;
                
        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = vertExtent *  Screen.width / Screen.height;

	    var v3 = transform.position;
	    v3.x = Mathf.Clamp(player.position.x, horzExtent, boardWidth-horzExtent-offset);
	    v3.y = Mathf.Clamp(player.position.y, vertExtent, boardHeight-vertExtent-offset);
        transform.position = v3;
	}
}
