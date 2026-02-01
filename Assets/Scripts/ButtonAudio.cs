using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public AudioClip buttonClickSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = buttonClickSound;
        audioSource.playOnAwake = false;
    }
    public void PlayButtonClickSound()
    {
        audioSource.Play();
    }
}
