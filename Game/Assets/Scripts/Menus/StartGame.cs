using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public Animator animatorMenu;
    public Animator animatorFade;
    public Animator animatorShrink;
    public string sceneName;

    public void StartGameMethod()
    {
        ChangeScene.sceneName = sceneName;
        animatorMenu.SetTrigger("Start");
        animatorFade.gameObject.SetActive(true);
        animatorFade.SetTrigger("Fade");
        animatorShrink.SetTrigger("Shrink");
    }
}
