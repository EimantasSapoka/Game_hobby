using UnityEngine;
using System.Collections;
using Assets.Scripts.Game.Interfaces;

public class Wall : MonoBehaviour, IInteractable {

	public Sprite dmgSprite;
	public int hp=4;
	private SpriteRenderer spriteRenderer;

	public AudioClip chopSound1;
	public AudioClip chopSound2;


	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();

	}
	
	public void DamageWall(int loss)
	{
		//GameManager.MusicPlayer.RandomizeSfx (chopSound1, chopSound2);
		spriteRenderer.sprite = dmgSprite;
		hp -= loss;
		if (hp == 0)
			gameObject.SetActive (false);
	}


    public void Interact(Component sender)
    {
        var damager = sender.GetComponent<IWallDamage>();
        if (damager != null)
        {
            DamageWall(damager.GetWallDamage());
        }

    }
}
