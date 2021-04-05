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
        gameCanvas.Unpause();
    }

    public void LeaveGame()
    {
        ChangeScene.sceneName = "MainMenu";
        scene.ChangeSceneMethod();
    }
}
