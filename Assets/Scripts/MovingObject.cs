using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
	public float moveTime = 0.1f;
	public LayerMask blockingLayer;

	private BoxCollider boxCollider;
	private Rigidbody rb2D;
	private float inverseMoveTime;

	// Use this for initialization
	protected virtual void Start ()
	{
		boxCollider = GetComponent<BoxCollider> ();
        rb2D = GetComponent<Rigidbody> ();
		inverseMoveTime = 1f / moveTime;
	}
	
    /*
    protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		boxCollider.enabled = false;
		hit = Physics2D.Linecast (start, end, blockingLayer);
		boxCollider.enabled = true;

		if (hit.transform == null) {
			StartCoroutine (SmoothMovement (end));
			return true;
		}
		return false;
	}
    */

    protected bool Move(int xDir, int yDir, out RaycastHit hit)
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(xDir, yDir, 0f);

        boxCollider.enabled = false;
        //hit = Physics.Linecast(start, end, blockingLayer);
        Physics.Raycast(start, Vector3.Normalize(new Vector3(xDir, yDir, 0f)), out hit, (end - start).sqrMagnitude, blockingLayer);
        boxCollider.enabled = true;

        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }

	protected IEnumerator SmoothMovement (Vector3 end)
	{
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards (rb2D.position, end, inverseMoveTime * Time.deltaTime);
			rb2D.MovePosition (newPosition);
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

	protected virtual void AttemptMove <T> (int xDir, int yDir) where T : Component
	{
        RaycastHit hit;
		bool canMove = Move (xDir, yDir, out hit);

		if (hit.transform == null)
			return;

		T hitComponent = hit.transform.GetComponent<T> ();

		if (!canMove && hitComponent != null)
			OnCantMove (hitComponent);
	}

	protected abstract void OnCantMove <T> (T component) where T : Component;
}