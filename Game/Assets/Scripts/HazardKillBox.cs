using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardKillBox : MonoBehaviour
{
    private CheckPointManager checkPointManager;
    private HazardShiftable shiftable;
    private bool isFrozen;

    private void Start()
    {
        checkPointManager = FindObjectOfType<CheckPointManager>();
        shiftable = GetComponentInParent<HazardShiftable>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovementRigidbody>()!=null)
        {
            checkPointManager.Respawn();
        }
    }

    private void Update()
    {
        isFrozen = shiftable.localTime == 0;
    }
}
