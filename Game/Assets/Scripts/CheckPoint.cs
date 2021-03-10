using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool activated = false;
    private CheckPointManager checkPointManager;

    private void Awake()
    {
        checkPointManager = FindObjectOfType<CheckPointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovementRigidbody>()!=null)
        {
            checkPointManager.lastCheckpoint = this;
            activated = true;
        }
    }
}
