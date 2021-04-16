using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMenu : MonoBehaviour
{
    public static AudioMenu audioMenu;
    public AudioMixer mixer;
    private AudioSource audioSource;
    public AudioClip navigate;
    public AudioClip select;
    public AudioClip back;
    public static bool playOnAwake = false;
    public VolumeSlider[] sliders;

    AudioData data;
    bool changeOnAwake;

    private void Awake()
    {
        changeOnAwake = true;
        audioMenu = this;
        audioSource = GetComponent<AudioSource>();
        if(playOnAwake)
        {
            PlaySelect();
            playOnAwake = false;
        }

        data = AudioDataSaver.LoadAudioData();
    }

    private void Update()
    {
        if (changeOnAwake)
        {
            mixer.SetFloat("GameSounds", data.gameVolume);
            mixer.SetFloat("MasterVolume", data.masterVolume);
            mixer.SetFloat("Music", data.musicVolume);
            mixer.SetFloat("UIVolume", data.uiVolume);
            changeOnAwake = false;
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

    public void ResetAudioData()
    {
        foreach(VolumeSlider v in sliders)
        {
            v.SetDefaultSliderValue();
        }
        AudioData audioData = new AudioData();
        AudioDataSaver.SaveDataToSystem(audioData);

    }
}
