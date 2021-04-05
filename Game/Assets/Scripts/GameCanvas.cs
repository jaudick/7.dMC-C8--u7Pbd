using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public static bool paused = false;
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

    public void Unpause(bool disbableMenu = true)
    {
        Time.timeScale = 1;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(!disbableMenu);
    }
}
