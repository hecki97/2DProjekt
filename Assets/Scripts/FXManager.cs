using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class FXManager : MonoBehaviour {

	public static FXManager instance = null;

	//ImageEffects
	public BloomOptimized bloom;
	public BlurOptimized blur;	

	//DubstepMode
	public bool bloomIsActive = false;

	// Use this for initialization
	void Awake () {
		if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
			
		InputEventHandler.OnTriggerDubstepMode += this.InputEventHandler_OnTriggerDubstepMode;
	
		if (object.ReferenceEquals(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BloomOptimized>(), null))
        {
            bloom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BloomOptimized>();
            bloom.enabled = bloomIsActive;
        }
		
		if (object.ReferenceEquals(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurOptimized>(), null))
        {
            blur = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurOptimized>();
            blur.enabled = (Application.loadedLevel != 0) ? false : true;;
        }
	}
	
	void OnDisable() {
		InputEventHandler.OnTriggerDubstepMode -= this.InputEventHandler_OnTriggerDubstepMode;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void InputEventHandler_OnTriggerDubstepMode()
    {
        //bloomIsActive = !bloomIsActive;
        bloom.enabled = (bloom.enabled) ? false : true;
    }
}
