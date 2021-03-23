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

    private void Awake()
    {
        meshRenderer = GetComponentInParent<MeshRenderer>();
        checkPointManager = FindObjectOfType<CheckPointManager>();
        if (meshRenderer != null) meshRenderer.material = deactivatedMaterial;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovementRigidbody>()!=null)
        {
            if(checkPointManager.lastCheckpoint!=this)
            {
                audioSource.PlayOneShot(checkpointSound);
            }
            checkPointManager.lastCheckpoint = this;
            activated = true;
            if(meshRenderer!=null) meshRenderer.material = activatedMaterial;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerMovementRigidbody>() != null)
        {
            checkPointManager.isInvincible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovementRigidbody>() != null)
        {
            checkPointManager.isInvincible = false;
        }
    }
}
