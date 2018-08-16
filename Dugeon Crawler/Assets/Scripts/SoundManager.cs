using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance = null;
    private AudioSource backgroundAudio;
    private AudioSource sfxAudio;

    public AudioClip backgroundCreepy;
    public AudioClip shieldHit;
    public AudioClip playerHit;
    public AudioClip playerDeath;
    public AudioClip gateOpen;
    public AudioClip gateClose;
    public AudioClip swordMiss;
    public AudioClip swordHit;
    public AudioClip lavaDeath;

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                sfxAudio = source;
            }
            else
            {
                backgroundAudio = source;
            }
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        sfxAudio.PlayOneShot(clip);
    }

    public void ChangeBackgroundMusic(AudioClip clip)
    {
        backgroundAudio.Stop();
        backgroundAudio.clip = clip;
    }
}
