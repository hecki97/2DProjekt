using UnityEngine;
using System.Collections;

public class GameRenderer : MonoBehaviour {
    /*
	private Transform player;
	private float oldPos = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		if (GameManager.instance.gameMode == GameMode.TwoD)
			foreach (Transform child in transform)
				child.gameObject.SetActive (false);

		UpdateGame ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.position.sqrMagnitude != oldPos)
			UpdateGame ();
	}

	void UpdateGame()
	{
		oldPos = player.position.sqrMagnitude;
		float sqrRemainingDistance = (player.position - transform.position).sqrMagnitude;

		if (GameManager.instance.gameMode == GameMode.ThreeD) {
			if (sqrRemainingDistance < 20f) {
				foreach (Transform child in transform)
					child.gameObject.SetActive (true);
			} else {
				foreach (Transform child in transform)
					child.gameObject.SetActive(false);
			}
		} else {
			if (sqrRemainingDistance < 7.5f) {
				foreach (Transform child in transform)
					child.gameObject.SetActive (true);
			}
		}
        
	}
    */
}