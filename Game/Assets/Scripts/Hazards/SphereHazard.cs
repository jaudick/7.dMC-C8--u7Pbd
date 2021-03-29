using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereHazard : MonoBehaviour
{
    private CheckPointManager checkPointManager;

    private void Awake()
    {
        checkPointManager = FindObjectOfType<CheckPointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovementRigidbody>() != null)
        {
            checkPointManager.Respawn();
        }
    }
}
