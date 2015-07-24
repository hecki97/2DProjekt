using UnityEngine;
using System.Collections;

public class RotatorScript : MonoBehaviour {

    public int speed;
    private int backup_speed;

    void Start()
    {
        InputEventHandler.OnTriggerDubstepMode += InputEventHandler_OnTriggerDubstepMode;

        if (FXManager.instance.bloomIsActive)
            InputEventHandler_OnTriggerDubstepMode();
    }

    void OnDisable()
    {
        InputEventHandler.OnTriggerDubstepMode -= InputEventHandler_OnTriggerDubstepMode;
    }

    void InputEventHandler_OnTriggerDubstepMode()
    {
        if (speed != 750)
        {
            backup_speed = speed;
            speed = 750;
        }
        else
            speed = backup_speed;
    }

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0f, 0f, 1f) * Time.deltaTime * speed);
	}
}
