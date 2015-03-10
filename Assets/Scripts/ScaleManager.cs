using UnityEngine;
using System.Collections;

public class ScaleManager : MonoBehaviour {

    public Vector3 scale2D = new Vector3(1f, 1f, 1f);
    public Vector3 scale3D = new Vector3(1f, 1f, 1f);

    public Vector3 offset2D;
    public Vector3 offset3D;

	// Use this for initialization
	void Start () {
	    if (GameManager.instance.gameMode == GameMode.TwoD)
        {
            transform.localScale = scale2D;
            transform.position += offset2D;
        }
        else
        {
            transform.localScale = scale3D;
            transform.position += offset3D;
        }
	}
}
