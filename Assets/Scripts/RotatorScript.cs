using UnityEngine;
using System.Collections;

public class RotatorScript : MonoBehaviour {

    public int speed;

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
	}
}
