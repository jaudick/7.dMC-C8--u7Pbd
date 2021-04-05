using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    bool paused = false;
    public GameObject pauseMenu;
    private void Awake()
    {
        pauseMenu.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Confined;
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
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 0;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
    }
}
