using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuGUIController : MonoBehaviour {

    private Animator anim;
    private Animator optionsMenuAnim;
    private GUIHandler guiHandler;

    void Awake()
    {
        anim = GetComponent<Animator>();
        guiHandler = GameObject.Find("Canvas").GetComponent<GUIHandler>();
        optionsMenuAnim = GameObject.Find("OptionsMenu").GetComponent<Animator>();
        
        anim.SetTrigger("triggerSkipFadeIn");
    }

    public void ButtonOptionsMenu()
    {
        StartCoroutine(guiHandler.ButtonSwitchGUIPanel(MenuInstance.OptionsMenu, anim, optionsMenuAnim, .5f));
    }

    public void ButtonLoadLevel(int index)
    {
        StartCoroutine(guiHandler.StartGame(anim, index));
    }

    public void ButtonExit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else   
            Application.Quit();
        #endif
    }
}
