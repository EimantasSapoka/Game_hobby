using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    private AudioSource audioSource;

    public AudioClip PlayerDamaged;
    public AudioClip PlayerMove;

	// Use this for initialization
	void Awake ()
	{
	    if (Instance == null)
	    {
	        Instance = this;
        }
        else if (Instance != this)
        {
            DestroyObject(this.gameObject);
        }
	    DontDestroyOnLoad(this.gameObject);

	    audioSource = GetComponent<AudioSource>();
	}

    void OnEnable()
    {
        Player.OnPlayerDamaged += () => audioSource.PlayOneShot(PlayerDamaged);
        Player.OnPlayerMoved += () => audioSource.PlayOneShot(PlayerMove);
    }  
}
