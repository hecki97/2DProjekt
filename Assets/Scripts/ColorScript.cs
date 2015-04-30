using UnityEngine;
using System.Collections;

public class ColorScript : MonoBehaviour {

	private Color32 colorStart;
	private Color32 colorEnd;
	public float duration;

	void Awake()
	{
		float i = Random.Range(5, 15);
		duration = i/10;

		colorStart = ColorUtil.getRandomColor();
		colorEnd = ColorUtil.getRandomColor();
	}

	void Update() {
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, lerp);
	}
}