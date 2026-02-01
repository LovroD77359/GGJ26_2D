using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Sounds : MonoBehaviour
{
    private AudioSource audioSource;
    bool looping = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayLoopingSFX(AudioClip clip)
    {
        if (looping && clip == audioSource.clip)
            return;
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.loop = true;
        looping = true;
    }

    public void PlaySFX(AudioClip clip)
    {

        audioSource.loop = false;
        audioSource.clip = clip;
        audioSource.Play();
        looping = false;
    }

    public void StopSFX(AudioClip clip)
    {
        if (clip != audioSource.clip)
            return;
        audioSource.Stop();
        looping = false;
    }

}
