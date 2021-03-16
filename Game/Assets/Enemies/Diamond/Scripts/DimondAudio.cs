using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimondAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] dimondSounds;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDimondSound(int clip=1)
    {
        audioSource.PlayOneShot(dimondSounds[clip - 1]);
    }
    
    public void StopAudio()
    {
        audioSource.Stop();
    }
}
