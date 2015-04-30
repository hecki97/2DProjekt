using UnityEngine;
using System.Collections;

public class RotatorScript : MonoBehaviour {

    public int speed;
    private int backup_speed;

    void Start()
    {
        SecretEventHandler.OnTrigger += this.SecretEventHandler_OnTrigger;

        if (GameManager.instance.secretModeActive)
            SecretEventHandler_OnTrigger();
    }

    void OnDisable()
    {
        SecretEventHandler.OnTrigger -= this.SecretEventHandler_OnTrigger;
    }

    void SecretEventHandler_OnTrigger()
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
