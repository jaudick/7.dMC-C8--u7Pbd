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

    public void SaveAudioData()
    {
        AudioData audioData = new AudioData();
        if (mixer.GetFloat("GameSounds", out float value1)) audioData.gameVolume = value1;
        if (mixer.GetFloat("MasterVolume", out float value2)) audioData.masterVolume = value2;
        if (mixer.GetFloat("UIVolume", out float value3)) audioData.uiVolume = value3;
        if (mixer.GetFloat("Music", out float value4)) audioData.musicVolume = value4;
        AudioDataSaver.SaveDataToSystem(audioData);
    }
}
