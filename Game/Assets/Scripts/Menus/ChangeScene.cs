using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static string sceneName;

    public void ChangeSceneMethod()
    {
        SceneManager.LoadScene(sceneName);
    }
}
