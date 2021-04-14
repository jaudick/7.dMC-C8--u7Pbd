using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string mixerVariable;
    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        if(mixer.GetFloat(mixerVariable, out float value))
        {
            if (GameCanvas.paused && (name == "Game"))
            {
                slider.value = Mathf.Pow(10, GameCanvas.previousGameSoundValue / 20f);
            }
            else
            {
                slider.value = Mathf.Pow(10, value / 20f);
            }
        }
    }
    public void SetVolumeLevel(float sliderValue)
    {
        mixer.SetFloat(mixerVariable, Mathf.Log10(sliderValue) * 20);
        if (name == "Game")
        { 
            if (mixer.GetFloat("GameSounds", out float value))
            {
                GameCanvas.previousGameSoundValue = value;
            }
        }
    }
}
