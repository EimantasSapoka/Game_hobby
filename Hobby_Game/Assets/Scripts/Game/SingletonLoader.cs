﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

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
            GameObject obj = new GameObject("EventSystem");

            //And adds the required components
            obj.AddComponent<EventSystem>();
            obj.AddComponent<StandaloneInputModule>().allowActivationOnMobileDevice = true;
            obj.AddComponent<TouchInputModule>();
        }
        if (!FindObjectOfType<SoundManager>())
        {
            new SoundManager();
        }

    }
}