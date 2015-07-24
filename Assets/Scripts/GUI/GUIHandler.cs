using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class GUIHandler : MonoBehaviour {
    
    public IEnumerator ButtonSwitchGUIPanel(MenuInstance menu, Animator start, Animator target, float time)
    {
        GameManager.instance.menu = menu;
        start.SetTrigger("triggerMenu");
        yield return new WaitForSeconds(time);
        target.SetTrigger("triggerMenu");
    }

    public IEnumerator StartGame(Animator start, int levelIndex)
    {
        PlayerStatsManager.instance.LoadPlayerStatsFromXML();
        GameManager.instance.menu = MenuInstance.PauseMenu;
        start.SetTrigger("triggerMenu");
        yield return new WaitForSeconds(.475f);
        Application.LoadLevel(levelIndex);

    }

    public IEnumerator TriggerPauseScreen(Animator start)
    {
        FXManager.instance.blur.enabled = !FXManager.instance.blur.enabled;
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
