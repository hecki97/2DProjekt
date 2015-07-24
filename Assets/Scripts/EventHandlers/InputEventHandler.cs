using UnityEngine;
using System.Collections;

public class InputEventHandler : MonoBehaviour {

	// Event Handler
    public delegate void InputGetKeyEvent();
    public static event InputGetKeyEvent OnTriggerDubstepMode;
    public static event InputGetKeyEvent OnInputEscape;

    public static void TriggerDubstepMode()
    {
        if (object.ReferenceEquals(OnTriggerDubstepMode, null))
            OnTriggerDubstepMode();
    }

    public static void InputEscape()
    {
        if (object.ReferenceEquals(OnInputEscape, null))
            OnInputEscape();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            TriggerDubstepMode();
            
        if (Input.GetKeyDown(KeyCode.Escape))
            InputEscape();   
    }
}
