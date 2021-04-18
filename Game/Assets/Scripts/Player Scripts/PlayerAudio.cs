using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] warpSounds;
    public AudioClip wallRun;
    public AudioClip dash;
    public AudioClip slide;
    public AudioClip jump;
    public AudioClip jumpOffEnemy;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWarp(int clip)
    {
        audioSource.PlayOneShot(warpSounds[clip - 1], 0.6f);
    }

    public void PlayWallRun()
    {
        audioSource.PlayOneShot(wallRun);
    }

    public void PlayDash()
    {
        audioSource.PlayOneShot(dash, 1.25f);
    }

    public void PlaySlide()
    {
        audioSource.PlayOneShot(slide, 1f);
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jump, 0.75f);
    }

    public void PlayJumpOffEnemy()
    {
        audioSource.PlayOneShot(jumpOffEnemy, 0.75f);
    }
}
