using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    public static AudioMenu audioMenu; 
    private AudioSource audioSource;
    public AudioClip navigate;
    public AudioClip select;
    public AudioClip back;
    public static bool playOnAwake = false;

    private void Awake()
    {
        audioMenu = this;
        audioSource = GetComponent<AudioSource>();
        if(playOnAwake)
        {
            PlaySelect();
            playOnAwake = false;
        }
    }
    public void PlaySelect()
    {
        audioSource.PlayOneShot(select, 0.5f);
    }

    public void PlayNavigate()
    {
        audioSource.PlayOneShot(navigate);
    }

    public void PlayBack()
    {
        audioSource.PlayOneShot(back);
    }
}
