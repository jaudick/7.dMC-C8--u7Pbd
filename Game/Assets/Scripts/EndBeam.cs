using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBeam : MonoBehaviour
{
    public static float timer;

    private Animator animator;
    private bool loading;
    private void Awake()
    {
        animator = ((ChangeScene)FindObjectOfType(typeof(ChangeScene), true)).GetComponent<Animator>();
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovementRigidbody>() != null && !loading)
        {
            loading = true;
            GetComponent<AudioSource>().Play();
            ChangeScene.sceneName = "MainMenu";

            int level = GetLevelInt();
            SaveData.SaveDataToSystem(level, timer);

            animator.gameObject.SetActive(true);
            animator.SetTrigger("Fade");
            
        }
    }

    private int GetLevelInt()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Tutorial" || sceneName == "Tutorial2")
        {
            Debug.Log("<color=yellow>Saving Tutorial</color>");
            return 0;
        }

        if (sceneName == "Level1")
        {
            Debug.Log("<color=yellow>Saving 1</color>");
            return 1;
        }

        if (sceneName == "Level2")
        {
            Debug.Log("<color=yellow>Saving 2</color>");
            return 2;
        }

        if (sceneName == "Level3")
        {
            Debug.Log("<color=yellow>Saving 3</color>");
            return 3;
        }

        if (sceneName == "Level4" || sceneName == "Level4 Test")
        {
            Debug.Log("<color=yellow>Saving 4</color>");
            return 4;
        }

        if (sceneName == "Level5")
        {
            Debug.Log("<color=yellow>Saving 5</color>");
            return 5;
        }

        if (sceneName == "Level6")
        {
            Debug.Log("<color=yellow>Saving 6</color>");
            return 6;
        }

        if (sceneName == "Level7")
        {
            Debug.Log("<color=yellow>Saving 8</color>");
            return 7;
        }

        if (sceneName == "Level8")
        {
            Debug.Log("<color=yellow>Saving 8</color>");
            return 8;
        }

        if (sceneName == "Level9")
        {
            Debug.Log("<color=yellow>Saving 9</color>");
            return 9;
        }

        if (sceneName == "Level10")
        {
            Debug.Log("<color=yellow>Saving 10</color>");
            return 10;
        }

        return 0;
    }
}
