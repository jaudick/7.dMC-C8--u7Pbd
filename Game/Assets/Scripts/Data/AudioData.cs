using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioData
{
    public float musicVolume;
    public float uiVolume;
    public float gameVolume;
    public float masterVolume;

    public AudioData()
    {
        musicVolume = 0f;
        uiVolume = 0f;
        gameVolume = 0f;
        masterVolume = 0f;
    }
}
