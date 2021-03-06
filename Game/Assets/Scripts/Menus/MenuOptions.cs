using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptions : MonoBehaviour
{
    public GameObject thisMenuOption;
    public GameObject menuOptionToGoTo;
    public GameCanvas gameCanvas;
    public ChangeScene scene;

    public void Change()
    {
        AudioMenu.audioMenu.PlaySelect();
        thisMenuOption.SetActive(false);
        menuOptionToGoTo.SetActive(true);
    }

    public void Hover()
    {
        AudioMenu.audioMenu.PlayNavigate();
    }

    public void Unpause()
    {
        AudioMenu.audioMenu.PlaySelect();
        gameCanvas.Unpause(true);
    }

    public void LeaveGame()
    {
        GameCanvas.instance.SaveAudioData();
        CheckPointManager player = FindObjectOfType<CheckPointManager>();
        player.isInvincible = true;
        gameCanvas.Unpause(false);
        //AudioMenu.audioMenu.PlaySelect();
        AudioMenu.playOnAwake = true;
        ChangeScene.sceneName = "MainMenu";
        scene.ChangeSceneMethod();
    }

    public void SetDefaultAudio()
    {
        AudioMenu.audioMenu.ResetAudioData();
    }

    public void SaveAudioChanges()
    {
        AudioMenu.audioMenu.SaveAudioData();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
