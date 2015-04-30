using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsGUIController : GUIHandler {

    private Animator anim;
    private Animator prevMenuAnim;
    private Animator audioMenuAnim;
    private Button buttonDifficulty;
    private Text buttonDifficultyText;
    private GUIHandler guiHandler;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        guiHandler = GameObject.Find("Canvas").GetComponent<GUIHandler>();
        audioMenuAnim = GameObject.Find("AudioMenu").GetComponent<Animator>();
        buttonDifficulty = GameObject.Find("ButtonDifficulty").GetComponent<Button>();
        buttonDifficultyText = GameObject.Find("ButtonDifficulty/Text").GetComponent<Text>();

        if (Application.loadedLevel == 0)
            prevMenuAnim = GameObject.Find("MainMenu").GetComponent<Animator>();
        else
        {
            prevMenuAnim = GameObject.Find("PauseMenu").GetComponent<Animator>();
            buttonDifficulty.interactable = false;
        }

        buttonDifficultyText.text = GameManager.instance.difficulty.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.menu == MenuInstance.OptionsMenu)
            StartCoroutine(guiHandler.TriggerPauseScreen(anim));
    }

    public void ButtonDifficulty()
    {
        if (Application.loadedLevel == 0)
        {
            switch (GameManager.instance.difficulty)
            {
                case Difficulty.Easy:
                    GameManager.instance.difficulty = Difficulty.Normal;
                    break;
                case Difficulty.Normal:
                    GameManager.instance.difficulty = Difficulty.Hard;
                    break;
                case Difficulty.Hard:
                    GameManager.instance.difficulty = Difficulty.Easy;
                    break;
            }
            buttonDifficultyText.text = GameManager.instance.difficulty.ToString();
        }   
    }

    public void ButtonAudioMenu()
    {
        StartCoroutine(ButtonSwitchGUIPanel(MenuInstance.AudioMenu, anim, audioMenuAnim, .5f));
    }

    public void ButtonBack()
    {
        StartCoroutine(ButtonSwitchGUIPanel(MenuInstance.PauseMenu, anim, prevMenuAnim, .5f));
    }
}
