using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;
    public AudioClip mainBGM;
    public AudioClip secretBGM;
    
    public AudioSource playerSource;
    public AudioSource efxSource;
    public AudioSource musicSource;

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
}
