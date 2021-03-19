using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidAudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] pyrSounds;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPyramidSound()
    {
        audioSource.PlayOneShot(pyrSounds[0]);
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
