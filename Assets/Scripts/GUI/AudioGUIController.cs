using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioGUIController : MonoBehaviour {

    public AudioMixer audioMixer;

    private Animator anim;
    private Animator optionsMenuAnim;
    private Slider musicSlider;
    private Slider sfxSlider;
    private Toggle audioToggle;
    private GUIHandler guiHandler;

    void Start()
    {
        anim = GetComponent<Animator>();
        guiHandler = GameObject.Find("Canvas").GetComponent<GUIHandler>();
        optionsMenuAnim = GameObject.Find("OptionsMenu").GetComponent<Animator>();
        musicSlider = GameObject.Find("SliderMusic").GetComponent<Slider>();
        sfxSlider = GameObject.Find("SliderSfx").GetComponent<Slider>();
        audioToggle = GameObject.Find("AudioToggle").GetComponent<Toggle>();

        musicSlider.value = SoundManager.instance.musicVal;
        sfxSlider.value = SoundManager.instance.sfxVal;
        audioToggle.isOn = !SoundManager.instance.muted;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.menu == MenuInstance.AudioMenu)
        {
            SaveValues();
            StartCoroutine(guiHandler.TriggerPauseScreen(anim));
        }
    }

	public void SetMusicVolume(float musicVol)
    {
        audioMixer.SetFloat("musicVal", musicVol);
    }

    public void SetSfxVolume(float sfxVol)
    {
        audioMixer.SetFloat("sfxVal", sfxVol);
    }

    public void SetSound(bool toggle)
    {
        float val = toggle ? 0 : -80;
        audioMixer.SetFloat("masterVal", val);
    }

    void SaveValues()
    {
        SoundManager.instance.musicVal = musicSlider.value;
        SoundManager.instance.sfxVal = sfxSlider.value;
        SoundManager.instance.muted = !audioToggle.isOn;
    }

    public void ButtonBack()
    {
        SaveValues();
        StartCoroutine(guiHandler.ButtonSwitchGUIPanel(MenuInstance.OptionsMenu, anim, optionsMenuAnim, .5f));
    }
}
