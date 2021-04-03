using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBeam : MonoBehaviour
{
    private Animator animator;
    private bool loading;
    private void Awake()
    {
        animator = ((ChangeScene)FindObjectOfType(typeof(ChangeScene), true)).GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovementRigidbody>() != null && !loading)
        {
            loading = true;
            GetComponent<AudioSource>().Play();
            ChangeScene.sceneName = "MainMenu";
            animator.gameObject.SetActive(true);
            animator.SetTrigger("Fade");
        }
    }
}
