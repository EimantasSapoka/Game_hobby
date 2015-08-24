using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
	    DontDestroyOnLoad(this.gameObject);
	}
	
}
