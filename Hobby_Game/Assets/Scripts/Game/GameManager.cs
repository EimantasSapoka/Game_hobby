using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public BoardManager boardManager;

    public int level = 1;

	// Use this for initialization
	void Awake ()
	{
	    if (instance == null)
	    {
            instance = this;
	    }
	    else if (instance != this)
	    {
	        DestroyObject(this.gameObject);
	    }

	    DontDestroyOnLoad(this.gameObject);
	    boardManager = GetComponent<BoardManager>();
	    boardManager.GenerateLevel(level);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
