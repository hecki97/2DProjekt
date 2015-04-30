using UnityEngine;
using System.Collections;

public class InstantiateBlockScript : MonoBehaviour {

	private bool isLerping = false;
	private float timeStartedLerping = 0;
	private Vector3 endPos = Vector3.zero;

	// Use this for initialization
	void Awake () {
		this.gameObject.transform.position -= new Vector3 (0f, 0f, 10f);
	}

	void Start() {
		//Invoke ("Lerp", GameManager.instance.delay);
		//GameManager.instance.delay += .25f; 
	}

	// Update is called once per frame
	void Update () {
		if (isLerping)
		{
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / .25f;
			
			transform.position = Vector3.Lerp(transform.position, endPos, percentageComplete);
			
			if (percentageComplete >= .8f)
				isLerping = false;
		}
	}

	void Lerp()
	{
		endPos = new Vector3(transform.position.x, transform.position.y, 0f);
		timeStartedLerping = Time.time;
		isLerping = true;
	}
}
