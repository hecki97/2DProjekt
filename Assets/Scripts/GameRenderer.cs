using UnityEngine;
using System.Collections;

public class GameRenderer : MonoBehaviour {

	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float sqrRemainingDistance = (player.position - transform.position).sqrMagnitude;

		if (sqrRemainingDistance < 7.5f) {
			foreach (Transform child in transform)
				child.gameObject.SetActive (true);
		} else {
			foreach (Transform child in transform)
				child.gameObject.SetActive(false);
		}
	}
}