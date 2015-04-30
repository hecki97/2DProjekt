using UnityEngine;
using System.Collections;

public class SecretEventHandler : MonoBehaviour {

    // Event Handler
    public delegate void OnTriggerSecretEvent();
    public static event OnTriggerSecretEvent OnTrigger;

    public static void Trigger()
    {
        if (OnTrigger != null)
            OnTrigger();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            Trigger();
    }
}
