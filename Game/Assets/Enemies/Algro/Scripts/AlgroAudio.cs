using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgroAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] algroSounds;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAlgroSound()
    {
        audioSource.PlayOneShot(algroSounds[0]);
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
