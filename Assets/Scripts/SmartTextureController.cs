using UnityEngine;
using System.Collections;

public class SmartTextureController : MonoBehaviour {

    public LayerMask blockingLayer;
    public enum Direction {North, South, West, East}
    public Direction dir;

    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;

	// Use this for initialization
	void Awake () {
        boxCollider = GetComponent<BoxCollider2D>();

        dir = Direction.North;
        CheckDir();
	}

    void CheckDir()
    {
        bool _bool = false;

        switch (dir)
        {
            case Direction.North:
                _bool = CheckIfHit(0f, .75f, out hit);
                if (_bool && hit.transform.tag == "Wall")
                {
                    this.transform.FindChild("Front").gameObject.SetActive(false);
                    hit.transform.FindChild("Back").gameObject.SetActive(false);
                }
                dir = Direction.South;
                break;
            case Direction.South:
                _bool = CheckIfHit(0f, -.75f, out hit);
                if (_bool && hit.transform.tag == "Wall")
                {
                    this.transform.FindChild("Back").gameObject.SetActive(false);
                    hit.transform.FindChild("Front").gameObject.SetActive(false);
                }
                dir = Direction.East;
                break;
            case Direction.East:
                _bool = CheckIfHit(.75f, 0f, out hit);
                if (_bool && hit.transform.tag == "Wall")
                {
                    this.transform.FindChild("Right").gameObject.SetActive(false);
                    hit.transform.FindChild("Left").gameObject.SetActive(false);

                    //this.transform.FindChild("Left").gameObject.SetActive(false);
                    //hit.transform.FindChild("Right").gameObject.SetActive(false);
                }
                dir = Direction.West;
                break;
            case Direction.West:
                _bool = CheckIfHit(-.75f, 0f, out hit);
                if (_bool && hit.transform.tag == "Wall")
                {
                    this.transform.FindChild("Left").gameObject.SetActive(false);
                    hit.transform.FindChild("Right").gameObject.SetActive(false);

                    //this.transform.FindChild("Right").gameObject.SetActive(false);
                    //hit.transform.FindChild("Left").gameObject.SetActive(false);
                }
                break;
        }

        if (dir != Direction.West)
            CheckDir();

    }

    bool CheckIfHit(float xDir, float yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if (hit.transform == null)
            return false;

        return true;
    }

	// Update is called once per frame
	void Update () {
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, .75f, 0));
		Debug.DrawLine(transform.position, transform.position + new Vector3(0, -.75f, 0));
        Debug.DrawLine(transform.position, transform.position + new Vector3(.75f, 0, 0));
        Debug.DrawLine(transform.position, transform.position + new Vector3(-.75f, 0, 0));
	}
}
