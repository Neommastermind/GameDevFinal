using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance = null;
    private AudioSource backgroundAudio;

    public AudioClip background;
    public AudioClip heartbeat;
    public AudioClip shieldHit;
    public AudioClip playerHit;
    public AudioClip playerDeath;
    public AudioClip gateOpen;
    public AudioClip gateClose;

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

        backgroundAudio = GetComponent<AudioSource>();

        /*AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                soundEffectAudio = source;
            }
        }*/
    }

    public void PlayOneShot(AudioClip clip)
    {
        backgroundAudio.PlayOneShot(clip);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
