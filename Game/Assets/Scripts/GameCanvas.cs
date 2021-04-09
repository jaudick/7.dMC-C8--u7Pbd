using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameCanvas : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseMenu;

    public AudioMixer mixer;
    private void Awake()
    {
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
        mixer.SetFloat("GameSounds", -80);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Unpause(bool disbableMenu = true)
    {
        mixer.SetFloat("GameSounds", 0);
        Time.timeScale = 1;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(!disbableMenu);
    }

    IEnumerator CannotTimeShiftBriefly()
    {
        TimeControls.canShift = false;
        yield return new WaitForEndOfFrame();
        TimeControls.canShift = true;
    }
}
