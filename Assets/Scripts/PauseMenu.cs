using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PauseMenu : MonoBehaviour
{
	private bool isPaused = false;
    private BlurOptimized blur;
	private Animator anim;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();

        blur = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurOptimized>();
        blur.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			TriggerPauseScreen ();
            blur.enabled = !blur.enabled;
		}
	}

	public void ButtonResume ()
	{
		TriggerPauseScreen ();
	}

	public void ButtonExit ()
	{
		Application.Quit ();
	}

    //Only for testing!
    public void ButtonRestart()
    {
        anim = null;
        Application.LoadLevel(Application.loadedLevel);
    }

	void TriggerPauseScreen ()
	{
		isPaused = !isPaused;
		anim.SetBool ("isPaused", isPaused);
		anim.SetTrigger ("triggerPauseMenu");

		/*
		if (isPaused) {
			Time.timeScale = 0;
		} else
			Time.timeScale = 1;
		*/
	}
}
