using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileKillbox : MonoBehaviour
{
    private CheckPointManager checkPointManager;
    private Shiftable shiftable;
    private bool isFrozen;

    private void Start()
    {
        checkPointManager = FindObjectOfType<CheckPointManager>();
        shiftable = GetComponent<Shiftable>();
        if (shiftable == null) shiftable = GetComponentInParent<Shiftable>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovementRigidbody>() != null && !isFrozen)
        {
            if (checkPointManager!=null) checkPointManager.Respawn();
        }
    }

    private void Update()
    {
        isFrozen = shiftable.localTime == 0;
    }
}
