using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip active;
    public AudioClip inactive;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayActive()
    {
        audioSource.clip = active;
        audioSource.Stop();
        audioSource.Play();
    }

    public void PlayInactive()
    {
        audioSource.clip = inactive;
        audioSource.Stop();
        audioSource.Play();
    }

}
