using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;
   
    // AudioClips
    public AudioClip mainBGM;
    public AudioClip dubstepBGM;
    
    // AudioSources
    public AudioSource playerSource;
    public AudioSource efxSource;
    public AudioSource musicSource;

    ///AudioMixerSnapshots
    public AudioMixerSnapshot unpaused;
    public AudioMixerSnapshot paused;
    
    [HideInInspector] public float musicVal = 0;
    [HideInInspector] public float sfxVal = 0;
    [HideInInspector] public bool muted = false;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
        InputEventHandler.OnTriggerDubstepMode += this.InputEventHandler_OnTriggerDubstepMode;
	}
    
    void OnDisable()
    {
        InputEventHandler.OnTriggerDubstepMode -= this.InputEventHandler_OnTriggerDubstepMode;
    }
    
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizePickupSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        playerSource.pitch = randomPitch;
        playerSource.clip = clips[randomIndex];
        playerSource.Play();
    }
    
    private void InputEventHandler_OnTriggerDubstepMode()
    {
        musicSource.clip = (musicSource.clip == mainBGM) ? dubstepBGM : mainBGM;
        musicSource.Play();
    }
}
