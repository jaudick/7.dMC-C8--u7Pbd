using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool activated = false;
    private CheckPointManager checkPointManager;
    [SerializeField] private Material activatedMaterial;
    [SerializeField] private Material deactivatedMaterial;
    private MeshRenderer meshRenderer;
    private AudioSource audioSource;
    [SerializeField] private AudioClip checkpointSound;
    private bool waitForIntro = true;

    private void Awake()
    {
        meshRenderer = GetComponentInParent<MeshRenderer>();
        checkPointManager = FindObjectOfType<CheckPointManager>();
        if (meshRenderer != null) meshRenderer.material = deactivatedMaterial;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(WaitForIntro());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovementRigidbody>()!=null)
        {
            checkPointManager.isInvincible = true;
            if (checkPointManager.lastCheckpoint!=this && !waitForIntro)
            {
                audioSource.PlayOneShot(checkpointSound);
            }
            checkPointManager.lastCheckpoint = this;
            for(int i = 0; i < checkPointManager.checkPoints.Length; i++)
            { 
                if(checkPointManager.checkPoints[i] == this)
                {
                    checkPointManager.currentCheckpointNum = i;
                    break;
                }
            }
            activated = true;
            if(meshRenderer!=null) meshRenderer.material = activatedMaterial;
        }
    }

    private IEnumerator WaitForIntro()
    {
        yield return new WaitForEndOfFrame();
        waitForIntro = false;
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerMovementRigidbody>() != null)
        {
            checkPointManager.isInvincible = true;
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovementRigidbody>() != null)
        {
            checkPointManager.isInvincible = false;
        }
    }
}
