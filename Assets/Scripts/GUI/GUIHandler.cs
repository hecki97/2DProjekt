using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.Audio;

public class GUIHandler : MonoBehaviour {

    private BlurOptimized blur;

    // Use this for initialization
    void Start()
    {
        blur = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurOptimized>();
        blur.enabled = (Application.loadedLevel != 0) ? false : true;
    }

    public IEnumerator ButtonSwitchGUIPanel(MenuInstance menu, Animator start, Animator target, float time)
    {
        GameManager.instance.menu = menu;
        start.SetTrigger("triggerMenu");
        yield return new WaitForSeconds(time);
        target.SetTrigger("triggerMenu");
    }

    public IEnumerator StartGame(Animator start, int levelIndex)
    {
        GameManager.instance.LoadDefaultPlayerStats();
        GameManager.instance.menu = MenuInstance.PauseMenu;
        start.SetTrigger("triggerMenu");
        yield return new WaitForSeconds(.475f);
        Application.LoadLevel(levelIndex);

    }

    public IEnumerator TriggerPauseScreen(Animator start)
    {
        blur.enabled = !blur.enabled;
        start.SetTrigger("triggerMenu");
        GameManager.instance.isPaused = !GameManager.instance.isPaused;
        if (GameManager.instance.isPaused)
            SoundManager.instance.paused.TransitionTo(.05f);
        else
            SoundManager.instance.unpaused.TransitionTo(.05f);

        yield return new WaitForSeconds(.05f);
        GameManager.instance.menu = MenuInstance.PauseMenu;
    }
}
