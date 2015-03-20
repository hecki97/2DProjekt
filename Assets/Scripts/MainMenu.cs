using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void Start() {
		Time.timeScale = 0;
	}

	public void ButtonLoadLevel(int index) {
		Application.LoadLevel (index);
	}

	public void ButtonExit() {
		Application.Quit ();
	}
}