using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.Audio;

public class PauseMenuGUIController : MonoBehaviour {

    private GUIHandler guiHandler;
    private Animator anim;
    private Animator optionsMenuAnim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        guiHandler = GameObject.Find("Canvas").GetComponent<GUIHandler>();
        optionsMenuAnim = GameObject.Find("OptionsMenu").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.menu == MenuInstance.PauseMenu)
            StartCoroutine(guiHandler.TriggerPauseScreen(anim));
    }

    //Only for testing!
    public void ButtonRestart()
    {
        StartCoroutine(guiHandler.TriggerPauseScreen(anim));
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ButtonResume()
    {
        StartCoroutine(guiHandler.TriggerPauseScreen(anim));
    }

    public void ButtonOptionsMenu()
    {
        StartCoroutine(guiHandler.ButtonSwitchGUIPanel(MenuInstance.OptionsMenu, anim, optionsMenuAnim, .5f));
    }

    public void ButtonLoadLevel(int index)
    {
        StartCoroutine(guiHandler.TriggerPauseScreen(anim));
        Application.LoadLevel(index);
    }
}
