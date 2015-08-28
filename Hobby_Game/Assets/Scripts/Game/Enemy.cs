using Assets.Scripts.Game.Interfaces;
using UnityEngine;

public class Enemy : MovingObject, IInteractsWithPlayer {

	public int PlayerDamage;

	public AudioClip EnemyAttackSound1;
	public AudioClip EnemyAttackSound2;

	private Animator animator;
	private Transform target;
	private bool skipMove;

	protected override void Start () {
		GameManager.Instance.AddEnemyToList (this);
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		base.Start ();
	}

	protected override void AttemptMove(int xDir, int yDir)
	{
		if (skipMove)
		{
			skipMove = false;
			return;
		}
		base.AttemptMove(xDir,yDir);
		skipMove = true;
	}

	public void MoveEnemy()
	{
		int xDir = 0;
		int yDir = 0;

		if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
			yDir = target.position.y > transform.position.y ? 1 : -1;
		else 
			xDir = target.position.x > transform.position.x ? 1 : -1;
		AttemptMove (xDir, yDir);

	}

    void IInteractsWithPlayer.InteractWithPlayer(Player player)
    {
        player.LoseFood(PlayerDamage);
        animator.SetTrigger ("Hit");
        //GameManager.MusicPlayer.RandomizeSfx(enemyAttackSound1, enemyAttackSound2);
    }
}
