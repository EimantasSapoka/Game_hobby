using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

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
	}
	
}
