using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

	public Text Text;
	private float count;
	
	IEnumerator Start ()
	{
		while (true) {
			if (Time.timeScale == 1) {
				yield return new WaitForSeconds (0.1f);
				count = (1 / Time.deltaTime);
				Text.text = "FPS: " + (Mathf.Round (count));
			} else {
				Text.text = "Pause";
			}
			yield return new WaitForSeconds (0.5f);
		}
	}
}
