using UnityEngine;
using System.Collections;
using Assets.Scripts.Game.Interfaces;

public abstract class MovingObject : MonoBehaviour {


	public float MoveTime = .1f;
	public LayerMask BlockingLayer;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2D;
	private float inverseMoveTime;

	// Use this for initialization
	protected virtual void Awake () {
		boxCollider = GetComponent<BoxCollider2D> ();
		rb2D = GetComponent<Rigidbody2D> ();
		inverseMoveTime = 1f / MoveTime;

	}

	protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		boxCollider.enabled = false;
		hit = Physics2D.Linecast (start, end, BlockingLayer);
		boxCollider.enabled = true;


		if (hit.transform == null) 
		{
			StartCoroutine (SmoothMovement (end));
			return true;
		}
		return false;
	}

	protected virtual bool AttemptMove(int xDir, int yDir)
	{
		RaycastHit2D hit;
		bool canMove = Move (xDir, yDir, out hit);

	    if (hit.transform == null)
	    {
	        return true;
	    }

		IInteractable hitComponent = hit.transform.GetComponent<IInteractable> ();

	    if (!canMove && hitComponent != null)
	    {
	        hitComponent.Interact(this);
            return false;
	    }
        return true;
    }

	protected IEnumerator SmoothMovement(Vector3 end)
	{
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards (rb2D.position, end, inverseMoveTime * Time.deltaTime);
			rb2D.MovePosition (newPosition);
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

}
