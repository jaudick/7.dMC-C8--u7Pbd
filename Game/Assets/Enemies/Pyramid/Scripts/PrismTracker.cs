using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismTracker : MonoBehaviour
{
    public bool tracking;
    private void Start()
    {
        tracking = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerMovementRigidbody>() != null) tracking = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovementRigidbody>() != null) tracking = false;
    }
}
