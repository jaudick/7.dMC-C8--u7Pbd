using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas instance;
    public static bool paused = false;
    public GameObject pauseMenu;
    public GameObject[] notPauseOpen;
    public GameObject pauseOpen;
    //private float previousGameSoundValue = 1;
    public static float previousGameSoundValue = 1;
    public AudioMixer mixer;

    private void Awake()
    {
        instance = this;
        pauseMenu.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                Pause();
            }
            else
            {
                paused = false;
                Unpause();
            }
        }
    }
    public void Pause()
    {
        if (mixer.GetFloat("GameSounds", out float value))
            previousGameSoundValue = value;
        mixer.SetFloat("GameSounds", -80f);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Unpause(bool disbableMenu = true)
    {
        mixer.SetFloat("GameSounds", previousGameSoundValue);
        Time.timeScale = 1;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(!disbableMenu);
        for(int i = 0; i < notPauseOpen.Length; i++)
        {
            notPauseOpen[i].SetActive(false);
        }
        pauseOpen.SetActive(true);

        SaveAudioData();

    }

    IEnumerator CannotTimeShiftBriefly()
    {
        TimeControls.canShift = false;
        yield return new WaitForEndOfFrame();
        TimeControls.canShift = true;
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
